using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FindAssembleMinigame : BaseMinigame
{
    [Header("FindAssemble Minigame")]
    [SerializeField] GameObject countObject;
    [SerializeField] TMP_Text pagesCount;
    int pagesAmount;
    [SerializeField] AudioStruc introDialog;
    [SerializeField] List<NotePickUp> notes;
    [SerializeField] GameObject assembleScreen;
    [SerializeField] List<DropSlot> slots;
    [SerializeField] Button doneButton;

    public override void StartGame()
    {
        base.StartGame();
        doneButton.onClick.AddListener(gameController.RoomManager.DoneMinigames);
        ShowAssembleScreen();
    }

    public void CheckNotes()
    {
        foreach (var note in notes)
        {
            if (note.picked == false)
                return;
        }
        StartGame();
    }

    public void IncreaseCount()
    {
        pagesAmount++;
        pagesCount.text = pagesAmount + " / 4";
    }

    public void ShowCount()
    {
        countObject.SetActive(true);
    }

    void ShowAssembleScreen()
    {
        countObject.SetActive(false);
        assembleScreen.SetActive(true);
        gameController.PlayerController.InputManager.DisableControls();
        gameController.EffectManager.BlurDepth();
    }

    public override void CheckGame()
    {
        base.CheckGame();
        foreach (var slot in slots)
        {
            if (slot.Blocked == false)
                return;
        }
        EndGame();
    }
    public override void EndGame()
    {
        audioManager.PlayOneShot(winGameClip);
        doneButton.interactable = true;
    }
}
