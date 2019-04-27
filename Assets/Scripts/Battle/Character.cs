using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private string _attackType;

        public string Name => _name;
        public int Damage => _damage;

        [SerializeField] private  Image _progressBar;
        [SerializeField] private  Image _avatar;
        private  Text _charInfo;

        [SerializeField] private float _shakeTime = 1;
        [SerializeField] private float _deadShakeTime = 1;
        [SerializeField] private float _shakeSpeed = 30;
        [SerializeField] private float _shakeAmp = 0.5f;
        private float _shakeEndTime = 0;

        [CanBeNull] private Action<string> _deadCallback;
        [CanBeNull] private Action _deadAnimEndCallback;
        [SerializeField] private Animator _attackAnimator;
        [SerializeField] private Animator _avatarAnimator;

        [SerializeField] private DamageDealer _damageDealer;

        public void SetDeadCallback(Action<string> callback)
        {
            _deadCallback += callback;
        }

        public void SetDeadAnimEndCallback(Action callback)
        {
            _deadAnimEndCallback += callback;
        }

        void Start()
        {
            UpdateProgressBar();
            _charInfo = GetComponentInChildren<Text>();
            _charInfo.text = _name;
            _damageDealer = _attackAnimator.gameObject.GetComponent<DamageDealer>();
        }
        
        public void DealDamage(int damage)
        {
            if (_health == 0)
                return;
            
            Debug.LogFormat(_name + "is damaged!");
            _health -= damage;
            AnimateDamage();
            UpdateProgressBar();
            
        }


        private void AnimateDamage(){
            if (IsDead())
            {
                _health = 0;
                StartCoroutine(Shake(_deadShakeTime));
                _deadCallback(_name);
            }
            else
            {         
                StartCoroutine(Shake(_shakeTime));
            }
        }


        public void Attack(int prescaler){
            _attackAnimator.SetTrigger(_attackType);
            _damageDealer.setDamage(prescaler * _damage);
            return;
        }

        public bool IsDead()
        {
            return _health <= 0;
        }

        private void UpdateProgressBar()
        {
            _progressBar.fillAmount = _health / (float) _maxHealth;
        }


        private  IEnumerator Shake(float shakeTime)
        {
            if (_avatarAnimator != null){
            _avatarAnimator.SetTrigger("Damaged");
            }
            _shakeEndTime = Time.time + shakeTime;
            Vector2 initPos = _avatar.rectTransform.anchoredPosition;
            int timer = 0;
            while (_shakeEndTime > Time.time)
            {
                timer++;
                Color c = _avatar.color;
                //fade out
                if (IsDead())
                {
                    float a = (_shakeEndTime - Time.time) / _deadShakeTime;
                    c.a = a;
                    _avatar.color = c;
                }
                else
                {
                    float gb =  Mathf.Sin(timer * Time.deltaTime);
                    c.g = gb;
                    c.b = gb;
                    _avatar.color = c;
                }

                Vector2 pos = initPos;
                pos.x += Mathf.Sin(Time.time * _shakeSpeed) * _shakeAmp * Screen.width;
                _avatar.rectTransform.anchoredPosition = pos;
                yield return 1;
            }
            _avatar.rectTransform.anchoredPosition = initPos;
            _avatar.color = Color.white;

            if (IsDead())
            {
                gameObject.SetActive(false);
                if (_deadCallback != null)
                {
                    _deadAnimEndCallback();
                }
                else
                {
                    Debug.Log("_deadCallback is null!");
                }
            }
        }

        private void OnEnable()
        {
            _avatar.color = Color.white;
            _health = _maxHealth;
            UpdateProgressBar();
        }
    }
}
