using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] static AudioSource bgmAudioSource;
        [SerializeField] static AudioSource seAudioSource;
        public AudioClip SE1;
        public AudioClip SE2;
        public AudioClip SE3;
        public AudioClip SE4;
        [SerializeField] static AudioClip SE5;
        public AudioClip BGM1;

        public static Dictionary<string, AudioClip> Sound;

        public float BgmVolume
        {
            get
            { return bgmAudioSource.volume; }
            set
            { bgmAudioSource.volume = Mathf.Clamp01(value); }
        }

        public float SeVolume
        {
            get
            { return seAudioSource.volume; }
            set
            { seAudioSource.volume = Mathf.Clamp01(value); }
        }

        void Start()
        {
            Sound = new Dictionary<string, AudioClip>();

            Sound.Add("SE1", SE1);
            Sound.Add("BGM1", BGM1);

            GameObject soundManager = CheckOtherSoundManager();
            bool checkResult = soundManager != null && soundManager != gameObject;
            if (checkResult)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

    }

        GameObject CheckOtherSoundManager()
        {

            return GameObject.FindGameObjectWithTag("SoundManager");
        }


       
    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if (clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

    public void PlaySe(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }
            seAudioSource.PlayOneShot(clip);
        }
    }
