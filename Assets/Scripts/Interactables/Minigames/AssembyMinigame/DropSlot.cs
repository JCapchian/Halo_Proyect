using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    AudioManager audioManager;

    [Header("Components")]
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image frame;

    [Header("Effects")]
    [SerializeField] AudioStruc dropSound;
    [SerializeField] Color blockedColor;
    [SerializeField] AudioStruc blockedSound;

    [Header("Minigame")]
    [SerializeField] AssembleMinigame mainMinigame;
    [SerializeField] DragDrop currentObject;
    [SerializeField] DragDrop desiredObject;

    bool blocked;
    public bool Blocked { get => blocked; }

    void Start()
    {
        audioManager = AudioManager.Instance;
    }

    /// <summary>Cuando se suelta un objeto encima</summary>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        // Si no esta bloqueado realiza el chequeo
        if (Blocked)
            return;

        // Pregunta si se coloco un objeto
        if (eventData.pointerDrag != null)
        {
            Check();
            // Almaceno el nuevo objeto
            DragDrop newObject = eventData.pointerDrag.GetComponent<DragDrop>();
            // Muevo y declaro su nuevo padre
            newObject.transform.SetParent(transform);
            newObject.MoveBack();

            //newObject.PreviousSlot.Check();

            // Muevo el anterior
            currentObject.transform.SetParent(newObject.PreviousSlot.transform);
            currentObject.MoveBack();

            currentObject.PreviousSlot = newObject.PreviousSlot;
            newObject.PreviousSlot.currentObject = currentObject;

            newObject.PreviousSlot.Check();

            newObject.PreviousSlot = this;

            currentObject = newObject;
            //currentObject.PreviousSlot.Check();

            // Pregunta si es el deseado
            if (currentObject == desiredObject)
            {
                BlockSlot();
                return;
            }

            audioManager.PlayOneShot(dropSound);
        }
        currentObject.MoveBack();
    }
    public void Check()
    {
        if (desiredObject == currentObject)
            BlockSlot();
    }

    void BlockSlot()
    {
        currentObject.BlockObject();
        blocked = true;
        mainMinigame.CheckGame();

        // Effects
        audioManager.PlayOneShot(blockedSound);
        frame.color = blockedColor;
    }
}
