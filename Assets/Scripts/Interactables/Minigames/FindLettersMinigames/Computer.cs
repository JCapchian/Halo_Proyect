using UnityEngine;

public class Computer : BaseInteractable
{
    public override void Interact()
    {
        gameController.RoomManager.FindLetters.StartGame();
        base.Interact();
    }
}
