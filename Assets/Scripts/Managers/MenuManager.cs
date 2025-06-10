using System.Threading.Tasks;
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

        startButton.onClick.AddListener(StartFunction);
    }

    public void StartFunction()
    {
        StartExperience();
    }

    public async Task StartExperience()
    {
        startButton.gameObject.SetActive(false);
        // Player Functions
        gameController.PlayerController.GuiHandler.StartShowRoom();

        gameController.EffectManager.BlurDepth();
        // Halo Functions
        await gameController.HaloController.HaloEffectsHandler.StopGlowing();

        ShowFirstMessage();
        //gameController.PlayerController.InputManager.EnableControls();

        // Managers Functions
        gameController.RoomManager.ActiveMinigames();
        gameController.EffectManager.SwitchLightsBright();

    }

    public void ShowFinalMessage()
    {
        finalPopup.SetActive(true);
        gameController.PlayerController.InputManager.DisableControls();
    }

    public void ShowFirstMessage()
    {
        firstPopup.SetActive(true);
    }

    public void FirstButton()
    {
        firstPopup.SetActive(false);
        gameController.PlayerController.InputManager.EnableControls();
        gameController.EffectManager.NormalDepth();
    }
}
