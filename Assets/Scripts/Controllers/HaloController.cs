using System.Threading.Tasks;
using UnityEngine;

public class HaloController : MonoBehaviour
{
    GameController gameController;
    [Header("Handlers")]
    [SerializeField] HaloEffectsHandler haloEffectsHandler;
    public HaloEffectsHandler HaloEffectsHandler { get => haloEffectsHandler; }


    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;

        haloEffectsHandler.Initialize(this);
    }

    public async Task ShowRoom()
    {
        await haloEffectsHandler.StopGlowing();
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
