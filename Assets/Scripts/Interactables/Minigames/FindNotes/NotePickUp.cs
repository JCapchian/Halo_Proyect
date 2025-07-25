using UnityEngine;

public class NotePickUp : BaseInteractable
{
    [Header("Components")]
    [SerializeField] FindAssembleMinigame minigame;
    public bool picked = false;

    public override void Interact()
    {
        audioManager.PlayOneShot(interactSound);
        picked = true;
        minigame.CheckNotes();

        gameObject.SetActive(false);

        DisableInteractable();
    }
}
