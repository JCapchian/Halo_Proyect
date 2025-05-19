using UnityEngine;

public class HaloController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] EffectsHandler effectsHandler;
    public EffectsHandler EffectsHandler { get => effectsHandler; }

    public void ShowRoom()
    {
        effectsHandler.StopGlowing();
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
