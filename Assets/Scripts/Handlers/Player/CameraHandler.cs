//using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class CameraHandler : MonoBehaviour
{
    PlayerController playerController;
    [Space(5f)]
    [Header("Components")]
    [SerializeField] Transform playerBody;
    [SerializeField] Camera viewCamera;
    public Camera ViewCamera { get => viewCamera; }
    Ray playerRay;
    RaycastHit hitInfo;
    [SerializeField] int distanciaRayo;
    [SerializeField] BaseInteractable pickUpObject;

    [Space(5f)]

    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    [Space(5f)]
    [Header("Camera Stats")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplier;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime;
    float cameraCap;
    [SerializeField][Range(0.1f, 1f)] float sensitivity;

    Vector2 axis;

    private Vector3 currentRotation;


    #region Sets && Gets

    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;

        playerController.InputManager.onCameraMovement += GetAxis;
    }

    public void GetAxis(Vector2 _axis)
    {
        axis = _axis;
    }

    #endregion

    #region Camera Movement

    public void HandleRotation()
    {
        Vector2 targetMouseDelta = axis;
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * sensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -50f, 50f);

        currentRotation = Vector3.right * cameraCap;

        ViewCamera.transform.localEulerAngles = currentRotation;

        playerBody.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    public void HandleRayCast()
    {
        playerRay = new Ray(viewCamera.transform.position, viewCamera.transform.forward);

        if (Physics.Raycast(playerRay, out hitInfo, distanciaRayo))
        {
            if (hitInfo.collider.GetComponent<BaseInteractable>() == null)
            {
                if (pickUpObject != null)
                {
                    playerController.InputManager.onInteraction -= pickUpObject.Interact;
                    pickUpObject.OnNotPointed();
                    //guiManager.HidePickupsGui();
                    pickUpObject = null;
                }
                return;
            }

            if (pickUpObject != null)
                if (pickUpObject != hitInfo.collider.GetComponent<BaseInteractable>())
                {
                    playerController.InputManager.onInteraction -= pickUpObject.Interact;
                    pickUpObject.OnNotPointed();
                    //guiManager.HidePickupsGui();
                    pickUpObject = null;
                }

            pickUpObject = hitInfo.collider.GetComponent<BaseInteractable>();
            if (playerController.InputManager.onInteraction == null)
                playerController.InputManager.onInteraction += pickUpObject.Interact;

            pickUpObject.OnPointed();
            //guiManager.ShowInteractGui(pickUpObject);
        }
        else
        {
            if (pickUpObject != null)
            {
                playerController.InputManager.onInteraction -= pickUpObject.Interact;
                pickUpObject.OnNotPointed();
                //guiManager.HidePickupsGui();
                pickUpObject = null;
            }
        }
    }

    #endregion
}