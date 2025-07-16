using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    protected GameController gameController;
    protected AudioManager audioManager;
    protected PlayerController playerController;

    protected bool blocked;

    [Header("Effects")]
    [SerializeField] protected AudioStruc interactSound;
    [SerializeField] protected AudioStruc cantInteractSound;


    void Start()
    {
        gameController = GameController.Instance;

        playerController = gameController.PlayerController;
        audioManager = gameController.AudioManager;
    }

    public virtual void ActiveInteractable()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnPointed() { }
    public virtual void Interact() { }
    public virtual void NotInteract() { }
    public virtual void OnNotPointed() { }

    public virtual void DisableInteractable()
    {
        blocked = true;
    }
}
