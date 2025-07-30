using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameController gameController;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource dialogSource;

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
            case AudioType.Dialog:
                dialogSource.PlayOneShot(audioStruc.Clip);
                break;
        }
    }
    public void PlaySound(AudioStruc audioStruc)
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
            case AudioType.Dialog:
                dialogSource.clip = audioStruc.Clip;
                dialogSource.Play();
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

    public void StopAudioClip(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Music:
                musicSource.clip = null;
                musicSource.Stop();
                break;
            case AudioType.Sound:
                soundSource.clip = null;
                soundSource.Stop();
                break;
            case AudioType.Dialog:
                dialogSource.clip = null;
                dialogSource.Stop();
                break;
        }

    }
}
