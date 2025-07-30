using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Components")]
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image image;
    public RectTransform RectTransform { get => rectTransform; }
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AudioStruc pickAudio;

    [SerializeField] public DropSlot PreviousSlot;

    public void MoveBack()
    {
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void BlockObject()
    {
        image.raycastTarget = false;
    }

    #region Pointers Functions

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameController.Instance.AudioManager.StopAudioClip(AudioType.Dialog);
        GameController.Instance.AudioManager.StopAudioClip(pickAudio.Type);
        GameController.Instance.AudioManager.PlaySound(pickAudio);
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    #endregion
}
