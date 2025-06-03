using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleMinigame : BaseMinigame
{
    [Header("Assemble Minigame")]
    [Space(20f)]
    [SerializeField] List<DropSlot> slots;
    [SerializeField] Button doneButton;
    public override void ActiveMiniGame()
    {
        base.ActiveMiniGame();
    }


    #region Game Functions
    public override void StartGame()
    {
        base.StartGame();
        gameObject.SetActive(true);
    }

    public override void CheckGame()
    {
        foreach (var item in slots)
        {
            if (!item.Blocked)
                return;
        }
        EndGame();

        base.CheckGame();
    }

    public override void EndGame()
    {
        audioManager.PlayOneShot(winGameClip);
        doneButton.interactable = true;

        base.EndGame();
    }

    public override void CloseMinigame()
    {
        gameObject.SetActive(false);
        base.CloseMinigame();
    }

    #endregion
}
