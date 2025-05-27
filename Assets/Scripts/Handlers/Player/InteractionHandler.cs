using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    PlayerController playerController;
    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;
    }
}
