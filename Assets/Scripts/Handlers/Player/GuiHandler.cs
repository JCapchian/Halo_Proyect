using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GuiHandler : MonoBehaviour
{
    GameController gameController;
    //EffectManager effectManager;
    PlayerController playerController;


    [Header("Show Effects")]
    [SerializeField] float showDuration;
    [SerializeField] Image showBackground;
    [SerializeField] float maxShow;

    public void Initialize(PlayerController _playerController)
    {
        playerController = _playerController;

        //effectManager = playerController.GameController.EffectManager;
    }

    public async Task StartShowRoom()
    {
        await ShowEffect();
        await ShowRoom();
    }

    async Task ShowEffect()
    {
        var currentTime = 0f;
        var tempColor = showBackground.color;
        while (currentTime < showDuration)
        {
            float t = currentTime / showDuration;

            t = t * t * (3f - 2f * t);

            tempColor.a = Mathf.Lerp(showBackground.color.a, maxShow, t);
            showBackground.color = tempColor;

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        tempColor.a = maxShow;
        showBackground.color = tempColor;

    }

    async Task ShowRoom()
    {
        var currentTime = 0f;
        var tempColor = showBackground.color;
        while (currentTime < showDuration)
        {
            float t = currentTime / showDuration;

            t = t * t * (3f - 2f * t);

            tempColor.a = Mathf.Lerp(showBackground.color.a, 0, t);
            showBackground.color = tempColor;

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        tempColor.a = 0;
        showBackground.color = tempColor;

    }


}
