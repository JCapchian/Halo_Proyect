using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    protected AudioManager audioManager;
    protected PlayerController playerController;

    protected bool blocked;

    [Header("Effects")]
    [SerializeField] protected AudioStruc interactSound;
    [SerializeField] protected AudioStruc cantInteractSound;


    protected virtual void Start()
    {
        audioManager = AudioManager.Instance;

        playerController = PlayerController.Instance;
    }

    public virtual void OnPointed() { }
    public virtual void Interact()
    {



    }
    public virtual void NotInteract() { }
    public virtual void OnNotPointed() { }

    public virtual void DisableInteractable()
    {
        blocked = true;
    }
}
