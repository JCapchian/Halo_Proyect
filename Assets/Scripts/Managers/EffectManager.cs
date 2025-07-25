using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    GameController gameController;

    [Header("Room #1 Effects")]
    [Header("Dark")]
    [SerializeField] Texture2D[] darkLightmapDir, darkLightmapColor;
    [Header("Bright")]
    [SerializeField] Texture2D[] brightLightmapDir, brightLightmapColor;
    LightmapData[] brightLightmap, darkLightmap;

    [Space(20f)]
    [SerializeField] Volume volume;
    [Header("Depth of Field")]
    CancellationTokenSource cts = new CancellationTokenSource();
    CancellationToken depthToken;
    DepthOfField depthOf;
    [SerializeField] float blurDuration;
    [Space(20f)]
    [Header("Color Adjustment")]
    ColorAdjustments colorAdj;
    [Space(20f)]
    [Header("Film Grain")]
    FilmGrain filmGrain;


    [Space(20f)]
    [Header("Music")]
    [SerializeField] AudioStruc room1Music;
    [SerializeField] AudioStruc room2Music;
    [SerializeField] AudioStruc room2Dialog;
    [SerializeField] AudioStruc room3Music;
    [SerializeField] AudioStruc room3Dialog;

    public void Initialize(GameController _gameController)
    {
        #region Lights
        gameController = _gameController;

        List<LightmapData> dLightMap = new List<LightmapData>();

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = darkLightmapDir[i];
            lmdata.lightmapColor = darkLightmapColor[i];

            dLightMap.Add(lmdata);
        }

        darkLightmap = dLightMap.ToArray();

        List<LightmapData> bLightMap = new List<LightmapData>();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = brightLightmapDir[i];
            lmdata.lightmapColor = brightLightmapColor[i];

            bLightMap.Add(lmdata);
        }

        brightLightmap = bLightMap.ToArray();
        #endregion

        #region Depth
        if (volume.profile.TryGet<DepthOfField>(out depthOf))
        {
            depthOf.active = true;
        }
        depthToken = cts.Token;
        #endregion

        #region Color Adjustment
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdj))
        {
            colorAdj.active = false;
        }
        depthToken = cts.Token;
        #endregion
        #region Film Grain
        if (volume.profile.TryGet<FilmGrain>(out filmGrain))
        {
            filmGrain.active = false;
        }
        depthToken = cts.Token;
        #endregion
    }

    #region Light Functions
    public void SwitchLightsDark()
    {
        LightmapSettings.lightmaps = darkLightmap;
    }
    public void SwitchLightsBright()
    {
        LightmapSettings.lightmaps = brightLightmap;
    }
    #endregion

    #region  Depth Functions
    public void CancelDepth()
    {
        cts.Cancel();
    }
    public async Task NormalDepth()
    {
        var currentTime = 0f;
        while (currentTime < blurDuration)
        {
            depthToken.ThrowIfCancellationRequested();
            float t = currentTime / blurDuration;

            t = t * t * (3f - 2f * t);

            depthOf.focusDistance.value = Mathf.Lerp(depthOf.focusDistance.value, 15f, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        depthOf.focusDistance.value = 15f;
    }
    public async Task BlurDepth()
    {
        Debug.Log("blur");
        var currentTime = 0f;
        while (currentTime < blurDuration)
        {
            depthToken.ThrowIfCancellationRequested();
            float t = currentTime / blurDuration;

            t = t * t * (3f - 2f * t);

            depthOf.focusDistance.value = Mathf.Lerp(depthOf.focusDistance.value, 0f, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        depthOf.focusDistance.value = 0f;
    }
    #endregion

    #region Color Adjustment

    public void EnableColorAdjustment()
    {
        colorAdj.active = true;
    }

    public void DisableColorAdjustment()
    {
        colorAdj.active = false;
    }

    #endregion

    #region Film Grain

    public void EnableFilmGrain()
    {
        filmGrain.active = true;
    }

    public void DisableFilmGrain()
    {
        filmGrain.active = false;
    }

    #endregion

    public void StartRoom1Music()
    {
        gameController.AudioManager.StopAudioClip(room1Music.Type);
        gameController.AudioManager.PlayMusic(room1Music);
    }
    public void StartRoom2Music()
    {
        gameController.AudioManager.StopAudioClip(room2Music.Type);
        gameController.AudioManager.PlayMusic(room2Music);
    }
    public void StartRoom2Dialog()
    {
        gameController.AudioManager.PlaySound(room2Dialog);
    }
    public void StartRoom3Music()
    {
        gameController.AudioManager.StopAudioClip(room3Music.Type);
        gameController.AudioManager.PlayMusic(room3Music);
    }
    public void StartRoom3Dialog()
    {
        gameController.AudioManager.PlaySound(room3Dialog);
    }

}
