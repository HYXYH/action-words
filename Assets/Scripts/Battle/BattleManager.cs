using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


namespace Battle
{
    public class BattleManager : MonoBehaviour, IBattleManager
    {
        [SerializeField] private Animator _bubbleAnimator;
        [SerializeField] private Text _bubbleText;

        [SerializeField] private PentaPuzzleManager _boardGame;
        [SerializeField] private Character _player;
        [SerializeField] private Character _enemy;
        


        [SerializeField] private float _damageTime  = 3;
        private float _nextDamageTime = 5;

        private bool _isBattleStarted = false;
        private bool _enemyDead;


        [CanBeNull] private Action<bool> _endBattleCallback;
       

        private void OnEnable()
        {
            _boardGame.SetWordActivationCallback(OnWordActivation);
            _player.SetDeadCallback(OnCharacterDead);
            _player.SetDeadAnimEndCallback(OnDeadAnimationEnd);
            _enemy.SetDeadCallback(OnCharacterDead);
            _enemy.SetDeadAnimEndCallback(OnDeadAnimationEnd);

        }

        private void Update()
        {
            // Enemy deals damage to player
            if (Time.time > _nextDamageTime && _isBattleStarted)
            {
                _nextDamageTime = Time.time + _damageTime;
                _enemy.Attack(1);
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
        

        private void OnWordActivation(string word, SpellEffect effect)
        {
            _bubbleText.text = word.ToUpper() + "!";
            _bubbleAnimator.SetTrigger("Show");
            _player.Attack(word.Length);
        }

        private void OnCharacterDead(string deadName)
        {
            Debug.Log(deadName + " is dead!");
            _isBattleStarted = false;
            if (_endBattleCallback != null)
            {
                _boardGame.EndBoardGame();
                _enemyDead = !(deadName.Equals(_player.Name));
            }
            else
            {
                Debug.Log("_endBattleCallback is null!");
            }
        }

        private void OnDeadAnimationEnd(){
            _endBattleCallback(_enemyDead);
            gameObject.SetActive(false);
        }
    }
}
