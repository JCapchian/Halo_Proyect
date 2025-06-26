using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameController gameController;

    [Header("Room #1")]
    [SerializeField] BaseMinigame[] minigamesRoom;

    [Header("Room #2")]
    [SerializeField] BaseMinigame findLetters;
    public BaseMinigame FindLetters { get => findLetters; }

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        foreach (var game in minigamesRoom)
        {
            game.Initialize(gameController);
        }
    }

    public void FindMinigame()
    {
        findLetters = FindAnyObjectByType<BaseMinigame>();
    }

    public void ActiveMinigames()
    {
        foreach (var minigame in minigamesRoom)
        {
            minigame.ActiveMiniGame();
        }
    }

    public void CheckGames()
    {
        foreach (var games in minigamesRoom)
        {
            if (!games.done)
                return;
        }
        DoneMinigames();
    }

    public void DoneMinigames()
    {
        //gameController.EffectManager.CancelDepth();
        gameController.EffectManager.BlurDepth();
        gameController.PlayerController.InputManager.DisableControls();
        gameController.MenuManager.ShowFinalMessage();
    }
}
