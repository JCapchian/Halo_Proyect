using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameController gameController;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;
    }

    public void PlayOneShot(AudioStruc audioStruc)
    {
        switch (audioStruc.Type)
        {
            case AudioType.Music:
                musicSource.PlayOneShot(audioStruc.Clip);
                break;
            case AudioType.Sound:
                soundSource.PlayOneShot(audioStruc.Clip);
                break;
        }
    }

    public void PlayMusic(AudioStruc audioStruc)
    {
        switch (audioStruc.Type)
        {
            case AudioType.Music:
                musicSource.clip = audioStruc.Clip;
                musicSource.Play();
                break;
            case AudioType.Sound:
                soundSource.clip = audioStruc.Clip;
                soundSource.Play();
                break;
        }

    }

    public void StopAudioClip(AudioStruc audioStruc)
    {
        switch (audioStruc.Type)
        {
            case AudioType.Music:
                musicSource.clip = null;
                musicSource.Stop();
                break;
            case AudioType.Sound:
                soundSource.clip = null;
                soundSource.Stop();
                break;
        }

    }
}
