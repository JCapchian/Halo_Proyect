using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PlayerEffectsHandler : MonoBehaviour
{
    PlayerController playerController;

    Camera playerViewCamera;


    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    void ControlBlur()
    {

    }
}
