using System;
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
    [SerializeField] GameObject flashlightObject;
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

    bool stop;

    Vector2 axis;

    private Vector3 currentRotation;


    #region Sets && Gets

    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;

        playerController.InputManager.onCameraMovement += GetAxis;

        DisableFlashlight();
    }

    public void GetAxis(Vector2 _axis)
    {
        axis = _axis;
    }

    #endregion

    #region Camera Movement

    public void HandleRotation()
    {
        if (stop)
            return;
        Vector2 targetMouseDelta = axis;
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * sensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

        currentRotation = Vector3.right * cameraCap;

        ViewCamera.transform.localEulerAngles = currentRotation;

        playerBody.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    public void StopMovement()
    {
        playerController.InputManager.onCameraMovement -= GetAxis;
        axis = Vector2.zero;
    }

    public void ResumeMovement()
    {
        playerController.InputManager.onCameraMovement += GetAxis;
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
            {
                playerController.InputManager.onInteraction += pickUpObject.Interact;
            }


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

    public void ClearInteractables()
    {
        if (pickUpObject != null)
        {
            playerController.InputManager.onInteraction -= pickUpObject.Interact;
            pickUpObject.OnNotPointed();
            pickUpObject = null;
        }
    }

    public void IncreasePickupRange(int newValue)
    {
        distanciaRayo = newValue;
    }

    #endregion

    #region Flashlight

    public void EnableFlashlight()
    {
        flashlightObject.SetActive(true);
    }

    public void DisableFlashlight()
    {
        flashlightObject.SetActive(false);
    }

    #endregion
}