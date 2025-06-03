using System.Threading.Tasks;
using UnityEngine;

public class HaloController : MonoBehaviour
{
    GameController gameController;
    [Header("Handlers")]
    [SerializeField] EffectsHandler effectsHandler;
    public EffectsHandler EffectsHandler { get => effectsHandler; }


    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        effectsHandler.Initialize(this);
    }

    public async Task ShowRoom()
    {
        await effectsHandler.StopGlowing();
        PlayerController.Instance.InputManager.EnableControls();
        gameObject.SetActive(false);
    }

    #region Execution Functions

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {

    }

    #endregion
}
