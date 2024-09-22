using UnityEngine;

namespace Audio
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxAudioSource;
        
        [SerializeField] private AudioClip buttonClickAudioClip;
        [SerializeField] private AudioClip levelCompletedAudioClip;
        [SerializeField] private AudioClip levelFailedAudioClip;

        public void PlayButtonClick()
        {
            sfxAudioSource.PlayOneShot(buttonClickAudioClip);
        }

        public void PlayLevelCompleted()
        {
            sfxAudioSource.PlayOneShot(levelCompletedAudioClip);
        }

        public void PlayLevelFailed()
        {
            sfxAudioSource.PlayOneShot(levelFailedAudioClip);
        }
    }
}