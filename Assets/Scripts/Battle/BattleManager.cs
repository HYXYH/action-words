using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour, IBattleManager
    {
        [SerializeField] private BoardGame _boardGame;
        [SerializeField] private Character _player;
        [SerializeField] private Character _enemy;

        [SerializeField] private float _damageTime  = 3;
        private float _nextDamageTime = 5;

        private bool _isBattleStarted = false;
        
        [CanBeNull] private Action<bool> _endBattleCallback;
       

        private void OnEnable()
        {
            _boardGame.SetWordActivationCallback(OnWordActivation);
            _player.SetDeadCallback(OnCharacterDead);
            _enemy.SetDeadCallback(OnCharacterDead);
        }

        private void Update()
        {
            // Enemy deals damage to player
            if (Time.time > _nextDamageTime && _isBattleStarted)
            {
                _nextDamageTime = Time.time + _damageTime;
                int damage = _enemy.Attack();
                _player.DealDamage(damage);
                if (_player.IsDead())
                {
                    _boardGame.EndBoardGame();
                    _isBattleStarted = false;
                }
            }
        }

        public void StartBattle()
        {
            gameObject.SetActive(true);
            _nextDamageTime = Time.time + _damageTime;
            _isBattleStarted = true;
            _player.gameObject.SetActive(true);
            _enemy.gameObject.SetActive(true);
            _boardGame.StartBoardGame();
        }
        
        public void SetBattleEndCallback(Action<bool> callback)
        {
            _endBattleCallback += callback;
        }
        

        private void OnWordActivation(string word, List<Thaum> thaums)
        {
            int damage = _player.Attack();
            damage *= word.Length;
            _enemy.DealDamage(damage);
            if (_enemy.IsDead())
            {
                _boardGame.EndBoardGame();
                _isBattleStarted = false;
            }
        }

        private void OnCharacterDead(string deadName)
        {
            Debug.Log(deadName + " is dead!");
            _isBattleStarted = false;
            if (_endBattleCallback != null)
            {
                _boardGame.EndBoardGame();
                _boardGame.gameObject.SetActive(false);
                bool enemyDead = !(deadName.Equals(_player.Name));
                _endBattleCallback(enemyDead);
            }
            else
            {
                Debug.Log("_endBattleCallback is null!");
            }
            gameObject.SetActive(false);
        }
    }
}
