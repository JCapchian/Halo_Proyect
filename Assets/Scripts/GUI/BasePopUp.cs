using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BasePopUp : MonoBehaviour
{
    GameController gameController;
    [SerializeField] PopUpScriptable data;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI titleComponent;
    [SerializeField] TextMeshProUGUI descriptionComponent;
    [SerializeField] TextMeshProUGUI buttonTextComponent;
    [SerializeField] public Button hideButton;
    [Space(5f)]
    [Header("Audio")]
    [SerializeField] AudioStruc showClip;
    [SerializeField] AudioStruc hideClip;

    public void Initialize(PopUpScriptable newData)
    {
        gameController = GameController.Instance;
        data = newData;

        showClip = data.ShowAudio;
        hideClip = data.HideAudio;

        titleComponent.text = data.Title;
        descriptionComponent.text = data.Description;
        buttonTextComponent.text = data.ButtonText;
        Show();
    }

    public void Show()
    {
        gameController.AudioManager.StopAudioClip(AudioType.Dialog);
        gameController.AudioManager.PlaySound(showClip);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameController.AudioManager.StopAudioClip(AudioType.Dialog);
        gameController.AudioManager.PlaySound(hideClip);
        gameObject.SetActive(false);
    }

}
