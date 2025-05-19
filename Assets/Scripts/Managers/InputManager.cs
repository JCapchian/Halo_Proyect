using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;

    public delegate void OnMovement(Vector2 axis);
    public OnMovement onMovement;
    public delegate void OnCameraMovement(Vector2 axis);
    public OnCameraMovement onCameraMovement;
    public delegate void OnInteraction();
    OnInteraction onInteraction;

    public void DisableControls()
    {
        playerControls.Disable();
    }

    public void EnableControls()
    {
        playerControls.Enable();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Player.Move.performed += i => onMovement?.Invoke(i.ReadValue<Vector2>());
            playerControls.Player.Look.performed += i => onCameraMovement?.Invoke(i.ReadValue<Vector2>());

            playerControls.Player.Interact.performed += i => onInteraction?.Invoke();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}