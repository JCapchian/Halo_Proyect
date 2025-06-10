using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [Header("Controllers")]
    [SerializeField] PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    [SerializeField] HaloController haloController;
    public HaloController HaloController { get => haloController; }

    [Space(20f)]

    [Header("Managers")]
    [SerializeField] AudioManager audioManager;
    public AudioManager AudioManager { get => audioManager; }
    [SerializeField] RoomManager roomManager;
    public RoomManager RoomManager { get => roomManager; }
    [SerializeField] MenuManager menuManager;
    public MenuManager MenuManager { get => menuManager; }
    [SerializeField] EffectManager effectManager;
    public EffectManager EffectManager { get => effectManager; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        // Controllers
        playerController.Initialize(this);
        haloController.Initialize(this);

        PlayerController.InputManager.DisableControls();

        // Managers
        audioManager.Initialize(this);
        effectManager.Initialize(this);
        menuManager.Initialize(this);
        roomManager.Initialize(this);

        effectManager.StartMusic();
    }

}
