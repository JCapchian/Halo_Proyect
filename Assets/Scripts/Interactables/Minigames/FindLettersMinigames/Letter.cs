using UnityEngine;

public class Letter : BaseInteractable
{
    [Header("Components")]
    [SerializeField] FindLettersMinigame minigame;
    [SerializeField] int letterPosition;

    public override void Interact()
    {
        // if (blocked)
        //     return;

        audioManager.PlayOneShot(interactSound);

        gameController.PlayerController.GuiHandler.AddLetter(letterPosition);
        minigame.ActiveMiniGame();

        gameObject.SetActive(false);

        base.Interact();
    }

    // public override void OnPointed()
    // {
    //     Debug.Log(gameObject);
    //     base.OnPointed();
    // }

}