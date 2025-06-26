using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GuiHandler : MonoBehaviour
{
    GameController gameController;
    //EffectManager effectManager;
    PlayerController playerController;
    [Header("Gameplay")]
    [SerializeField] List<GameObject> letters;

    [Space(20f)]
    [Header("Effects")]
    [Header("Show Effects")]
    [SerializeField] float showDuration;
    [SerializeField] Image showBackground;
    [SerializeField] float maxShow;

    [Space(5f)]
    [Header("Fade Effect")]
    [SerializeField] Image fadeBackground;
    [SerializeField] float fadeDuration;

    public float ShowDuration { get => showDuration; set => showDuration = value; }

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

    #region Gameplay
    public void AddLetter(int newLetter)
    {
        letters[newLetter].SetActive(true);
    }

    public void ClearLetter()
    {
        foreach (var letter in letters)
        {
            letter.SetActive(false);
        }
    }

    public bool CheckLetters()
    {
        foreach (var letter in letters)
        {
            if (!letter.activeSelf)
                return false;
        }
        return true;
    }
    #endregion

    #region  Visuals

    async Task ShowEffect()
    {
        var currentTime = 0f;
        var tempColor = showBackground.color;
        while (currentTime < ShowDuration)
        {
            float t = currentTime / ShowDuration;

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
        while (currentTime < ShowDuration)
        {
            float t = currentTime / ShowDuration;

            t = t * t * (3f - 2f * t);

            tempColor.a = Mathf.Lerp(showBackground.color.a, 0, t);
            showBackground.color = tempColor;

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        tempColor.a = 0;
        showBackground.color = tempColor;
    }

    public async Task FadeInEffect()
    {
        fadeBackground.gameObject.SetActive(true);
        var currentTime = 0f;
        var tempColor = fadeBackground.color;
        while (currentTime < fadeDuration)
        {
            float t = currentTime / fadeDuration;

            t = t * t * (3f - 2f * t);

            fadeBackground.color = tempColor;
            tempColor.a = Mathf.Lerp(fadeBackground.color.a, 255, t);

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        tempColor.a = 255;
        fadeBackground.color = tempColor;
    }

    public async Task FadeOutEffect()
    {
        var currentTime = 0f;
        var tempColor = fadeBackground.color;
        while (currentTime < fadeDuration)
        {
            float t = currentTime / fadeDuration;

            t = t * t * (3f - 2f * t);

            tempColor.a = Mathf.Lerp(fadeBackground.color.a, 0, t);
            fadeBackground.color = tempColor;

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        tempColor.a = 0;
        fadeBackground.color = tempColor;
    }
    #endregion
}
