using System;
using Enums;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class SoundManager : MonoGenericSingleton<SoundManager>
    {
        [SerializeField]

        public AudioSource soundMusic;
        public AudioSource soundEffect;
        public Sounds[] sound;
        

        public void Play(SoundTypes soundType)
        {
            SetVolume(soundType);
        
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                soundEffect.PlayOneShot(clip);
            }
            else
            {
                GameLogManager.CustomLog("Clip not found for sound type: " + soundType);
            }
        }

        public void PlayMusic(SoundTypes soundType)
        {
            SetVolume(soundType);
        
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                soundMusic.PlayOneShot(clip);
            }
            else
            {
                GameLogManager.CustomLog("Clip not found for sound type: " + soundType);
            }
        }
        
        private void SetVolume(SoundTypes soundType)
        {
            Sounds sounds = Array.Find(sound, item => item.soundType == soundType);
        
            // If it is muted then don't play any sound.
            if (sounds.b_IsMute)
            {
                soundEffect.mute = true;
            }
            else
            {
                soundEffect.mute = false;
                soundEffect.volume = sounds.volume;
            }

        }

        private AudioClip GetSoundClip(SoundTypes soundType)
        {
            Sounds sounds = Array.Find(sound, item => item.soundType == soundType);
        
            if (sounds != null)
                return sounds.soundClip;
            return null;

        }
    }

    [Serializable]
    public class Sounds
    {
   
        public SoundTypes soundType;
        public AudioClip soundClip;

        public bool b_IsMute = false;
        [Range(0f, 1f)] public float volume;
    }
}