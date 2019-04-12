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

        public void SetDeadCallback(Action<string> callback)
        {
            _deadCallback += callback;
        }

        void Start()
        {
            UpdateProgressBar();
            _charInfo = GetComponentInChildren<Text>();
            _charInfo.text = _name;
        }
        
        public void DealDamage(int damage)
        {
            if (_health == 0)
                return;
            
            Debug.LogFormat(_name + "is damaged!");
            _health -= damage;
            if (_health < 0)
            {
                _health = 0;
                
                //play dead animation
                _shakeEndTime = Time.time + _deadShakeTime;
            }
            else
            {         
                _shakeEndTime = Time.time + _shakeTime;
            }
            UpdateProgressBar();
            StartCoroutine(Shake());
        }

        public bool IsDead()
        {
            return _health <= 0;
        }

        private void UpdateProgressBar()
        {
            _progressBar.fillAmount = _health / (float) _maxHealth;
        }


        private  IEnumerator Shake()
        {
            Vector3 initPos = transform.position;
            while (_shakeEndTime > Time.time)
            {
                //fade out
                if (IsDead())
                {
                    float a = (_shakeEndTime - Time.time) / _shakeTime;
                    Color c = _avatar.color;
                    c.a = a;
                    _avatar.color = c;
                }

                Vector3 pos = initPos;
                pos.x += Mathf.Sin(Time.time * _shakeSpeed) * _shakeAmp;
                transform.position = pos;
                yield return 1;
            }
            transform.position = initPos;

            if (IsDead())
            {
                gameObject.SetActive(false);
                if (_deadCallback != null)
                {
                    _deadCallback(_name);
                }
                else
                {
                    Debug.Log("_deadCallback is null!");
                }
            }
        }

        private void OnEnable()
        {
            Color c = _avatar.color;
            c.a = 1;
            _avatar.color = c;
            _health = _maxHealth;
            UpdateProgressBar();
        }
    }
}
