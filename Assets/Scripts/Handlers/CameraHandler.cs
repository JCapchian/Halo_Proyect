//using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class CameraHandler : MonoBehaviour
{
    [Space(2f)]
    [Header("Components")]
    [SerializeField] Transform playerBody;
    [SerializeField] Camera viewCamera;
    public Camera ViewCamera { get => viewCamera; }
    [SerializeField] Transform currentGunModel;

    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    [Space(2f)]
    [Header("Camera Stats")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplier;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime;
    float cameraCap;
    [SerializeField][Range(0.1f, 1f)] float sensitivity;

    [Space(2f)]
    [Header("Camera Gun Stats")]
    [SerializeField] private float recoilInX;
    [SerializeField] private float recoilInY;
    [SerializeField] private float recoilInZ;
    [SerializeField] private float snappiness;
    [SerializeField] private float returnToCenterSpeed;
    Vector2 axis;

    private Vector3 currentRotation;
    private Vector3 targetRotation;


    #region Sets && Gets

    public void Initialize(PlayerController playerController)
    {
        playerController.InputManager.onCameraMovement += GetAxis;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
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

    // public void GunMovement()
    // {
    //     var mouseX = axis.x * swayMultiplier;
    //     var mouseY = axis.y * swayMultiplier;

    //     Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
    //     Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

    //     Quaternion targetRotation = rotationX * rotationY;

    //     if (currentGunModel)
    //         currentGunModel.transform.localRotation = Quaternion.Slerp(currentGunModel.transform.localRotation, targetRotation, smooth * Time.deltaTime);
    // }

    #endregion
}