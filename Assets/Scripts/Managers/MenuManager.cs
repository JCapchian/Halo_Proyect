using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameController gameController;

    [SerializeField] public List<PopUpScriptable> PopUps;
    [SerializeField] public List<BasePopUp> scenePopUps;
    [SerializeField] Transform popUpContainer;

    [SerializeField] Button startButton;

    [Header("Popup")]
    // [SerializeField] GameObject firstPopup;
    // [SerializeField] AudioStruc firstPopupDialog;
    // [SerializeField] GameObject finalPopup;
    [SerializeField] AudioStruc finalPopupDialog;
    [SerializeField] GameObject backgroundPopUp;

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

        ShowPopUp(0);
        scenePopUps[0].hideButton.onClick.AddListener(FirstButton);
        scenePopUps[0].hideButton.onClick.AddListener(HideBackground);
        //gameController.PlayerController.InputManager.EnableControls();

        // Managers Functions
        gameController.RoomManager.ActiveMinigames();
        gameController.EffectManager.SwitchLightsBright();
    }

    public void ShowPopUp(int indexPopUp)
    {
        backgroundPopUp.SetActive(true);
        // Creo el nuevo Pop Up
        Debug.Log(indexPopUp);
        var newPopUpObject = Instantiate(PopUps[indexPopUp].PopUpPrefab, popUpContainer);
        Debug.Log("Creado");
        newPopUpObject.GetComponent<BasePopUp>().Initialize(PopUps[indexPopUp]);
        newPopUpObject.GetComponent<BasePopUp>().hideButton.onClick.AddListener(HideBackground);
        scenePopUps.Add(newPopUpObject.GetComponent<BasePopUp>());


        // Side Effects
        gameController.EffectManager.BlurDepth();
        gameController.PlayerController.InputManager.DisableControls();
    }
    public void HideBackground()
    {
        backgroundPopUp.SetActive(false);
    }
    #region Buttons Region
    public void FirstButton()
    {
        gameController.PlayerController.InputManager.EnableControls();
        gameController.AudioManager.StopAudioClip(AudioType.Dialog);
        gameController.EffectManager.NormalDepth();
    }

    public void FinalButton()
    {
        Debug.Log("Final Button");
        //finalPopup.SetActive(false);
        gameController.PlayerController.InputManager.DisableControls();
        gameController.PlayerController.CameraHandler.ClearInteractables();
        gameController.AudioManager.StopAudioClip(AudioType.Dialog);
        gameController.SceneController.StartTransition();
    }
    #endregion
}
