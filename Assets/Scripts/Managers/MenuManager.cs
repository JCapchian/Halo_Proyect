using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button startButton;

    [Space(20f)]
    [Header("Other Entities")]
    [SerializeField] HaloController haloController;

    void Awake()
    {
        startButton.onClick.AddListener(StartExperience);
    }

    void StartExperience()
    {
        haloController.ShowRoom();
    }
}
