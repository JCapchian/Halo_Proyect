using UnityEngine;

public class Computer : BaseInteractable
{
    public override void Interact()
    {
        audioManager.PlayOneShot(interactSound);
        gameController.RoomManager.FindLetters.StartGame();
    }
}
