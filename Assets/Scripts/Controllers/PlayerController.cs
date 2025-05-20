using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        Initialize();
    }

    void Initialize()
    {
        movementHandler.Initialize(this);
        cameraHandler.Initialize(this);
        interactionHandler.Initialize(this);

    }

    public void Start()
    {

        //inputManager.DisableControls();
    }
    #region Execution Functions

    private void Update()
    {
        movementHandler.GroundChecker();
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