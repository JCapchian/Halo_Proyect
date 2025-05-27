using UnityEngine;

public class BaseMinigame : MonoBehaviour
{
    protected AudioManager audioManager;
    protected PlayerController playerController;

    [Header("Components")]
    [SerializeField] BaseInteractable interactObject;

    [Header("Effects")]
    [SerializeField] protected AudioStruc winGameClip;

    void Start()
    {
        audioManager = AudioManager.Instance;

        playerController = PlayerController.Instance;
    }
    #region Main Functions

    public virtual void StartGame() { }

    public virtual void CheckGame() { }

    public virtual void EndGame()
    {
        interactObject.DisableInteractable();
    }

    public virtual void CloseMinigame()
    {
        playerController.InputManager.EnableControls();
        interactObject.DisableInteractable();
    }

    #endregion
}
