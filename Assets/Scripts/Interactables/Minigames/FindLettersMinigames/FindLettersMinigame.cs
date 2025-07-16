using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FindLettersMinigame : BaseMinigame
{
    [Header("Encontrar letras Minigame")]
    [Space(5f)]
    [Header("Palabras")]
    [SerializeField] List<string> words;
    [SerializeField] string actualWord;
    int wordIndex;
    [Space(20f)]
    [Header("Screens")]
    [SerializeField] GameObject findLetterScreen;
    [SerializeField] GameObject textScreen;
    [SerializeField] GameObject firstLetter;

    [Space(20f)]
    [Header("Components")]
    [SerializeField] TMP_InputField textField;
    [Space(20f)]
    [Header("Otros")]
    [SerializeField] AudioStruc errorClip;
    [SerializeField] GameObject rewardingMessage;
    [SerializeField] bool findAllLetters;
    [SerializeField] bool active;

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

            active = true;
        }

        // Si encontró todas las letras
        if (!findAllLetters)
            return;

        // Muevo la cámara del jugador
        gameController.PlayerController.InputManager.DisableControls();
        ActiveInput();
    }

    public override void CheckGame()
    {
        // Pregunto si tiene la palabra actual
        if (textField.text != actualWord)
        {
            gameController.AudioManager.PlayOneShot(errorClip);
            ActiveInput();
            return;
        }

        ClearInput();
        Debug.Log(wordIndex);
        Debug.Log(words.Count);

        // Pregunto si aun queda palabras
        if (words.Count - 1 <= wordIndex)
        {
            CloseMinigame();
            return;
        }

        rewardingMessage.SetActive(true);
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
        gameController.MenuManager.ShowFinalMessage();
        gameController.PlayerController.GuiHandler.ClearLetter();
    }

    void ShowNextScreen()
    {
    }
    void ShowFinalScreen()
    {

    }









}
