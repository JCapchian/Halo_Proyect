using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FindLettersMinigame : BaseMinigame
{
    [Header("Encontrar letras Minigame")]
    [Space(5f)]
    [Header("Screens")]
    [SerializeField] GameObject findLetterScreen;
    [SerializeField] GameObject textScreen;
    [Space(20f)]
    [Header("Components")]
    [SerializeField] TextField textField;
    [Header("Otros")]
    [SerializeField] bool findAllLetters;

    public override void ActiveMiniGame()
    {
        if (!gameController.PlayerController.GuiHandler.CheckLetters())
            return;

        textScreen.SetActive(true);
        findAllLetters = true;

        base.ActiveMiniGame();
    }

    public override void StartGame()
    {
        if (!findAllLetters)
        {
            Debug.Log("No");
            return;
        }

        // Quito control al player
        textField.enabledSelf = true;
        base.StartGame();
    }

    public override void CheckGame()
    {
        if (textField.text == "esfuerzo")
            done = true;
        base.CheckGame();
    }









}
