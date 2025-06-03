using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameController gameController;

    [Header("Buttons")]
    [SerializeField] Button startButton;

    [Header("Popup")]
    [SerializeField] GameObject firstPopup;
    [SerializeField] GameObject finalPopup;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        startButton.onClick.AddListener(StartExperience);
    }

    void StartExperience()
    {
        // Halo Functions
        gameController.HaloController.EffectsHandler.StopGlowing();

        // Player Functions
        gameController.PlayerController.GuiHandler.StartShowRoom();
        gameController.PlayerController.InputManager.EnableControls();

        // Managers Functions
        gameController.RoomManager.ActiveMinigames();
        gameController.EffectManager.SwitchLightsBright();

        firstPopup.SetActive(false);
    }

    public void ShowFinalMessage()
    {
        finalPopup.SetActive(true);
    }
}
