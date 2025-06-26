using UnityEngine;

public class Letter : ObjectMinigames
{
    [Header("Components")]
    [SerializeField] FindLettersMinigame minigame;
    [SerializeField] int letterPosition;

    public override void Interact()
    {
        if (blocked)
            return;

        audioManager.PlayOneShot(interactSound);

        gameController.PlayerController.GuiHandler.AddLetter(letterPosition);
        gameController.RoomManager.FindLetters.ActiveMiniGame();
        base.Interact();
    }

    // public override void OnPointed()
    // {
    //     Debug.Log(gameObject);
    //     base.OnPointed();
    // }

}