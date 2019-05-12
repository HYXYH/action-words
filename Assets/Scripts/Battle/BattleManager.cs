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
        [SerializeField] private CanvasGroup _boardCanvas;
        [SerializeField] private Character _player;
        [SerializeField] private Character _enemy;

        [SerializeField] private Text _playerActivatedWords;
        [SerializeField] private Text _enemyActivatedWords;

        [SerializeField] private TurnLabel[] _turnLabels;

        
        private bool _isPlayerTurn = true;
        private bool _isBossFirstAttack = false;
        private bool _enemyDead;

        [SerializeField] private float _firstBossAttackDelay = 3f;
        [SerializeField] private float _firstBossAttackTime;


        [CanBeNull] private Action<bool> _endBattleCallback;
       

        private void OnEnable()
        {
            _boardGame.SetWordActivationCallback(OnWordActivation);
            _boardGame.SetNextScrollCallback(OnSkipScroll);
            _player.SetDeadCallback(OnCharacterDead);
            _player.SetDeadAnimEndCallback(OnDeadAnimationEnd);
            _player.SetEndTurnCallback(OnEndTurn);
            _player.GetDamageDealer().SetTarget(_enemy);
            _enemy.SetDeadCallback(OnCharacterDead);
            _enemy.SetDeadAnimEndCallback(OnDeadAnimationEnd);
            _enemy.SetEndTurnCallback(OnEndTurn);
            
            foreach(var turnLabel in _turnLabels)
            {
                turnLabel.setBossIcon(_enemy.GetTurnIcon());
            }
        }

        private void Update()
        {
            // The shit is needed if battle starts with boss turn (for tutorial) 
            if (_isBossFirstAttack &&  _firstBossAttackTime < Time.time){
                _isBossFirstAttack = false;
                enemyAction();
            }
        }

        public void StartBattle(bool isPlayerTurn, string bossName)
        {
            _isPlayerTurn = isPlayerTurn;
            _boardCanvas.blocksRaycasts = _isPlayerTurn;
            _isBossFirstAttack = !_isPlayerTurn;
            if (_isBossFirstAttack){
                _firstBossAttackTime = Time.time + _firstBossAttackDelay;
            }
            foreach(var turnLabel in _turnLabels){
                turnLabel.setTurn(_isPlayerTurn);
            }

            _enemy = gameObject.transform.Find(bossName)?.GetComponent<Character>();
            if (_enemy == null){
                Debug.LogError("Bad boss name");
            }
            _enemyActivatedWords = _enemy.transform.Find("ActivatedWords").GetComponent<Text>();

            gameObject.SetActive(true);
            _player.gameObject.SetActive(true);
            _enemy.gameObject.SetActive(true);
            _boardGame.gameObject.SetActive(true);
            _boardGame.StartBoardGame();
        }
        
        public void SetBattleEndCallback(Action<bool> callback)
        {
            _endBattleCallback += callback;
        }
        
        // todo: move to character, AI should depend on boss.
        private void enemyAction(){
            if (_enemy.IsDead())
            {
                return;
            }
             List<string> words = _boardGame.GetSelectableWords();
                if (words.Count > 0)
                    StartCoroutine(_boardGame.EmulateWordActivation(words[0]));
                else{
                    _boardGame.NextScroll();
                    words = _boardGame.GetSelectableWords();
                    StartCoroutine(_boardGame.EmulateWordActivation(words[0]));
                    }
        }


        private void OnWordActivation(string word, SpellEffect effect)
        {
            if (_isPlayerTurn){

                _player.Attack(word.Length);
                _bubbleText.text = word.ToUpper() + "!";
                _bubbleAnimator.SetTrigger("Show");
                _player.Attack(word.Length);
                _playerActivatedWords.text = _playerActivatedWords.text + word + "\n";
            }
            else {
                _enemy.Attack(word.Length);
                _enemyActivatedWords.text = _enemyActivatedWords.text + word + "\n";
            }
        }

        private void OnCharacterDead(string deadName)
        {
            Debug.Log(deadName + " is dead!");
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
            
            foreach(var turnLabel in _turnLabels){
                turnLabel.setTurn(_isPlayerTurn);
            }

            // Enemy deals damage to player
            if (!_isPlayerTurn)
            {
                Debug.Log("Boss turn");
                enemyAction();
            }
            else{
                Debug.Log("Player turn");
            }
        }

        private void OnSkipScroll(){
            _playerActivatedWords.text = "";
            _enemyActivatedWords.text = "";
        }
    }
}
