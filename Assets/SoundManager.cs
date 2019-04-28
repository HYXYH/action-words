using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SoundManager : MonoBehaviour
    {
        public  AudioClip FireSound;
        public AudioClip FireShortSound;
        public AudioClip DeathSound;
        public AudioClip PainSound;


        private AudioSource _source;

        void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlaySound(string attackType)
        {
            if (attackType.Equals("Fireball"))
            {
                //_source.PlayOneShot(FireSound);
                _source.PlayOneShot(FireShortSound);
            }
            else if (attackType.Equals("Death"))
            {
                _source.PlayOneShot(DeathSound);
            }
            else if (attackType.Equals("Pain"))
            {
                _source.PlayOneShot(PainSound);
            }

                
        }
    }
}