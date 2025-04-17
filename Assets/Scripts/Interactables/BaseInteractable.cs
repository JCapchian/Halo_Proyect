using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField] AudioStruc interactSound;

    public virtual void OnPointed() { }
    public virtual void Interact() { }
    public virtual void NotInteract() { }
    public virtual void OnNotPointed() { }
}
