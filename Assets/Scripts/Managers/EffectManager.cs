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
    [Header("Depth of Field")]
    CancellationTokenSource cts = new CancellationTokenSource();
    CancellationToken depthToken;
    [SerializeField] Volume volume;
    DepthOfField depthOf;
    [SerializeField] float blurDuration;

    [Space(20f)]
    [Header("Music")]
    [SerializeField] AudioStruc musicStruc;

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

    public void StartMusic()
    {
        gameController.AudioManager.PlayMusic(musicStruc);
    }
}
