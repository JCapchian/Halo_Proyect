using UnityEngine;

public class BaseMinigame : MonoBehaviour
{
    GameController gameController;

    PlayerController playerController;

    protected AudioManager audioManager;
    [Header("Components")]
    [SerializeField] BaseInteractable interactObject;

    [Header("Effects")]
    [SerializeField] protected AudioStruc winGameClip;

    public bool done;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        audioManager = gameController.AudioManager;

        playerController = gameController.PlayerController;
    }

    #region Operation Functions

    public virtual void ActiveMiniGame()
    {
        interactObject.gameObject.SetActive(true);
    }

    #endregion
    #region Game Functions

    public virtual void StartGame()
    {
        gameController.EffectManager.BlurDepth();
    }

    public virtual void CheckGame() { }

    public virtual void EndGame()
    {
        interactObject.DisableInteractable();
        done = true;
    }

    public virtual void CloseMinigame()
    {
        gameController.EffectManager.NormalDepth();
        playerController.InputManager.EnableControls();
        interactObject.DisableInteractable();
        gameController.RoomManager.CheckGames();
    }

    #endregion
}
