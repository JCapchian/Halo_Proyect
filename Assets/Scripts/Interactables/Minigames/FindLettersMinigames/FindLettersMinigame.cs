using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FindLettersMinigame : BaseMinigame
{
    [Header("Encontrar letras Minigame")]
    [SerializeField] Computer computer;
    [Space(5f)]
    [Header("Palabras")]
    [SerializeField] List<string> words;
    [SerializeField] string actualWord;
    int wordIndex;
    [SerializeField] List<GameObject> letters;
    [Space(20f)]
    [Header("Screens")]
    [SerializeField] GameObject findLetterScreen;
    [SerializeField] GameObject textScreen;
    [SerializeField] GameObject firstLetter;
    [SerializeField] GameObject rewardingMessage;

    [Space(20f)]
    [Header("Components")]
    [SerializeField] TMP_InputField textField;

    [Space(20f)]
    [Header("Audio")]

    [SerializeField] AudioStruc errorClip;
    [SerializeField] AudioStruc rewardingClip;
    bool findAllLetters;
    bool active;

    #region Game Functions
    // Cada vez que interactua con una letra
    public override void ActiveMiniGame()
    {
        // Pregunta si encontró todas las letras
        if (!gameController.PlayerController.GuiHandler.CheckLetters())
        {
            findLetterScreen.SetActive(false);
            return;
        }

        // Encontro todas las letras
        findAllLetters = true;
        textScreen.SetActive(true);
    }

    public override void StartGame()
    {
        // Al Interactuar la primera vez
        if (!active)
        {
            actualWord = words[0];
            wordIndex = 0;
            textField.characterLimit = 100;

            findLetterScreen.SetActive(true);
            firstLetter.SetActive(true);
            ActiveLetters();

            active = true;
        }

        // Si encontró todas las letras
        if (!findAllLetters)
            return;

        // Muevo la cámara del jugador
        gameController.PlayerController.InputManager.DisableControls();
        computer.DisableCollider();
        ActiveInput();
    }

    public override void CheckGame()
    {
        // Pregunto si tiene la palabra actual
        if (textField.text.ToLower() != actualWord)
        {
            gameController.AudioManager.PlayOneShot(errorClip);
            ActiveInput();
            return;
        }

        ClearInput();

        // Pregunto si aun queda palabras
        if (words.Count - 1 <= wordIndex)
        {
            CloseMinigame();
            return;
        }

        // Rewarding function

        gameController.AudioManager.StopAudioClip(AudioType.Dialog);

        gameController.MenuManager.ShowPopUp(2);
        gameController.MenuManager.scenePopUps[1].hideButton.onClick.AddListener(ActiveInput);
        gameController.MenuManager.scenePopUps[1].hideButton.onClick.AddListener(gameController.MenuManager.HideBackground);
        gameController.EffectManager.NormalDepth();
        wordIndex++;
        actualWord = words[wordIndex];

        ActiveInput();
    }

    public void ActiveInput()
    {
        textField.text = " ";
        textField.ActivateInputField();

        textField.onFocusSelectAll = true;
    }
    public void ClearInput()
    {
        textField.text = " ";
    }

    public override void CloseMinigame()
    {
        Debug.Log("PEPE");
        gameController.MenuManager.ShowPopUp(1);
        gameController.MenuManager.scenePopUps[2].hideButton.onClick.AddListener(gameController.MenuManager.FinalButton);
        gameController.MenuManager.scenePopUps[2].hideButton.onClick.AddListener(gameController.MenuManager.HideBackground);
        gameController.PlayerController.GuiHandler.ClearLetter();
    }
    #endregion

    void ActiveLetters()
    {
        foreach (var letter in letters)
        {
            letter.SetActive(true);
        }
    }
}
