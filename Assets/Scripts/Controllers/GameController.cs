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
    [SerializeField] SceneController sceneController;
    public SceneController SceneController { get => sceneController; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this);
        // Controllers
        playerController.Initialize(this);
        DontDestroyOnLoad(playerController);
        haloController.Initialize(this);
        DontDestroyOnLoad(haloController);

        PlayerController.InputManager.DisableControls();

        // Managers
        audioManager.Initialize(this);
        effectManager.Initialize(this);
        roomManager.Initialize(this);
        sceneController.Initialize(this);
        FindMenuManager();

        effectManager.StartRoom1Music();
    }

    public void FindMenuManager()
    {
        menuManager = FindAnyObjectByType<MenuManager>();
        menuManager.Initialize(this);
    }
}