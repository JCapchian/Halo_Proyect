using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    GameController gameController;

    [SerializeField] InputManager inputManager;
    public InputManager InputManager { get => inputManager; }
    [SerializeField] GuiHandler guiHandler;
    public GuiHandler GuiHandler { get => guiHandler; }
    [SerializeField] CameraHandler cameraHandler;
    public CameraHandler CameraHandler { get => cameraHandler; }
    [SerializeField] InteractionHandler interactionHandler;
    public InteractionHandler InteractionHandler { get => interactionHandler; }
    [SerializeField] MovementHandler movementHandler;
    public MovementHandler MovementHandler { get => movementHandler; }

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        movementHandler.Initialize(this);
        cameraHandler.Initialize(this);
        guiHandler.Initialize(this);
        interactionHandler.Initialize(this);
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