using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameController gameController;

    [SerializeField] Button startButton;

    [Header("Popup")]
    [SerializeField] GameObject firstPopup;
    [SerializeField] GameObject finalPopup;

    [Header("Others")]
    [SerializeField] Transform spawnSceneTransform;

    public Transform SpawnSceneTransform { get => spawnSceneTransform; }

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;
    }

    public void StartFunction()
    {
        StartExperience();
    }

    async Task StartExperience()
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
        gameController.EffectManager.BlurDepth();
        gameController.PlayerController.InputManager.DisableControls();
    }

    public void ShowFirstMessage()
    {
        firstPopup.SetActive(true);
    }
    #region Buttons Region
    public void FirstButton()
    {
        firstPopup.SetActive(false);
        gameController.PlayerController.InputManager.EnableControls();
        gameController.EffectManager.NormalDepth();
    }

    public void FinalButton()
    {
        finalPopup.SetActive(false);
        gameController.PlayerController.InputManager.DisableControls();
        gameController.PlayerController.CameraHandler.ClearInteractables();
        gameController.PlayerController.CameraHandler.IncreasePickupRange(0);
        gameController.SceneController.StartTransition();
    }
    #endregion
}
