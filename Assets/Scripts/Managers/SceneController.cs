using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameController gameController;
    [SerializeField] List<SceneScriptableObject> database;
    //int actualSceneIndex;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;
        //actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void LoadNextScene()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        switch (database[SceneManager.GetActiveScene().buildIndex + 1].loadMode)
        {
            case LoadMode.Normal:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case LoadMode.Async:
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
        gameController.EffectManager.NormalDepth();
    }

    public async Task StartTransition()
    {
        await gameController.PlayerController.GuiHandler.FadeInEffect();
        gameController.EffectManager.NormalDepth();
        LoadNextScene();
        SceneManager.sceneLoaded += EnterNewScene;
    }

    void EnterNewScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cargando Escena");
        gameController.PlayerController.CameraHandler.ClearInteractables();
        //Efectos Escena
        gameController.RoomManager.FindMinigame();
        gameController.FindMenuManager();
        gameController.AudioManager.PlayMusic(database[SceneManager.GetActiveScene().buildIndex].musicScene);

        //Efectos Jugador
        gameController.PlayerController.InputManager.DisableControls();
        gameController.EffectManager.NormalDepth();
        gameController.PlayerController.ResetPlayerPosition();
        gameController.PlayerController.GuiHandler.FadeOutEffect();

        //Efectos específicos de cada escena
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameController.PlayerController.CameraHandler.IncreasePickupRange(100);
            gameController.PlayerController.PlayerEffectsHandler.TurnOnFlashLight();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameController.PlayerController.PlayerEffectsHandler.TurnOffFlashLight();
            gameController.EffectManager.EnableFilmGrain();
            gameController.EffectManager.EnableColorAdjustment();
            gameController.PlayerController.CameraHandler.DisableFlashlight();
        }

        //Muestro el popup Inicial
        gameController.MenuManager.ShowPopUp(0);
        gameController.MenuManager.scenePopUps[0].hideButton.onClick.AddListener(gameController.MenuManager.FirstButton);
        gameController.MenuManager.scenePopUps[0].hideButton.onClick.AddListener(gameController.MenuManager.HideBackground);
        gameController.PlayerController.CameraHandler.ClearInteractables();

        SceneManager.sceneLoaded -= EnterNewScene;
    }
}
