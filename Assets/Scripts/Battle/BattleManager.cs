using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Battle
{
    public class BattleManager : MonoBehaviour, IBattleManager
    {
        [SerializeField] private PentaPuzzleManager _boardGame;
        [SerializeField] private CanvasGroup _boardCanvas;
        [SerializeField] private Character _player;
        [SerializeField] private Character _enemy;

        private bool _isPlayerTurn = true;
        private bool _isBattleStarted = false;
        private bool _enemyDead;

        [CanBeNull] private Action<bool> _endBattleCallback;
       

        private void OnEnable()
        {
            _boardGame.SetWordActivationCallback(OnWordActivation);
            _player.SetDeadCallback(OnCharacterDead);
            _player.SetDeadAnimEndCallback(OnDeadAnimationEnd);
            _player.SetEndTurnCallback(OnEndTurn);
            _enemy.SetDeadCallback(OnCharacterDead);
            _enemy.SetDeadAnimEndCallback(OnDeadAnimationEnd);
            _enemy.SetEndTurnCallback(OnEndTurn);
        }

        private void Update()
        {
        }

        public void StartBattle(bool isPlayerTurn)
        {
            _isPlayerTurn = isPlayerTurn;
            _boardCanvas.blocksRaycasts = _isPlayerTurn;

            gameObject.SetActive(true);
            _isBattleStarted = true;
            _player.gameObject.SetActive(true);
            _enemy.gameObject.SetActive(true);
            _boardGame.StartBoardGame();
        }
        
        public void SetBattleEndCallback(Action<bool> callback)
        {
            _endBattleCallback += callback;
        }
        

        private void OnWordActivation(string word)
        {
            if (_isPlayerTurn){
                _player.Attack(word.Length);
            }
            else {
                _enemy.Attack(word.Length);
            }
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


        private void OnEndTurn(){
            _isPlayerTurn = !_isPlayerTurn;
            _boardCanvas.blocksRaycasts = _isPlayerTurn;
            
            // Enemy deals damage to player
            // if (!_isPlayerTurn)
            // {
            //     _enemy.Attack(1);
            // }
        }
    }
}
