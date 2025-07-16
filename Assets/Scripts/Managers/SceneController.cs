using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameController gameController;
    int actualSceneIndex;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(actualSceneIndex + 1);
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        //Efectos Escena
        gameController.RoomManager.FindMinigame();
        gameController.FindMenuManager();
        gameController.EffectManager.StartRoom2Music();

        //Efectos Jugador
        gameController.EffectManager.NormalDepth();
        gameController.PlayerController.ResetPlayerPosition();
        gameController.PlayerController.GuiHandler.FadeOutEffect();

        gameController.PlayerController.CameraHandler.IncreasePickupRange(100);
        gameController.PlayerController.PlayerEffectsHandler.TurnOnFlashLight();

        SceneManager.sceneLoaded -= EnterNewScene;
    }
}
