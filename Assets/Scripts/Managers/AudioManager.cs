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

    public void StopAudioClip(AudioStruc audioStruc)
    {
        switch (audioStruc.Type)
        {
            case AudioType.Music:
                musicSource.Stop();
                break;
            case AudioType.Sound:
                soundSource.Stop();
                break;
        }

    }
}
