using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    PlayerController playerController;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;

    public void Initialize(PlayerController _playerController)
    {
        if (Instance == null)
        {
            Instance = this;
        }
        playerController = _playerController;
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
