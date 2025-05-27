using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] InputManager inputManager;
    public InputManager InputManager { get => inputManager; }
    [SerializeField] CameraHandler cameraHandler;
    public CameraHandler CameraHandler { get => cameraHandler; }
    [SerializeField] InteractionHandler interactionHandler;
    public InteractionHandler InteractionHandler { get => interactionHandler; }
    [SerializeField] MovementHandler movementHandler;
    public MovementHandler MovementHandler { get => movementHandler; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Initialize();
    }

    void Initialize()
    {
        movementHandler.Initialize(this);
        cameraHandler.Initialize(this);
        interactionHandler.Initialize(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Start()
    {

    }
    #region Execution Functions

    private void Update()
    {
        movementHandler.GroundChecker();
        cameraHandler.HandleRayCast();
    }

    private void FixedUpdate()
    {
        movementHandler.CurrentState.OnUpdate(movementHandler);
    }

    private void LateUpdate()
    {
        cameraHandler.HandleRotation();
    }
    #endregion
}