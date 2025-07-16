using UnityEngine;

public class ObjectMinigames : BaseInteractable
{
    [Header("Components")]
    [SerializeField] protected BaseMinigame minigame;

    public override void Interact()
    {
        if (blocked)
            return;

        audioManager.PlayOneShot(interactSound);

        playerController.InputManager.DisableControls();
        minigame.StartGame();

        base.Interact();
    }
}
