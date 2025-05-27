using System.Threading.Tasks;
using UnityEngine;

public class HaloController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] EffectsHandler effectsHandler;
    public EffectsHandler EffectsHandler { get => effectsHandler; }

    public void ShowRoom()
    {
        effectsHandler.StopGlowing();
        Task.WaitAny(effectsHandler.FinishGlowDown());
        PlayerController.Instance.InputManager.EnableControls();
        Cursor.lockState = CursorLockMode.Locked;
    }


    #region Execution Functions

    void Awake()
    {
        effectsHandler.Initialize(this);
    }

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
