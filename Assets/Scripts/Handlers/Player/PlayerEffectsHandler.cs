using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PlayerEffectsHandler : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField] GameObject playerFlashlight;


    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;
    }

    public void TurnOnFlashLight()
    {
        playerFlashlight.SetActive(true);
    }

    public void TurnOffFlashLight()
    {
        playerFlashlight.SetActive(false);
    }
}
