using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    HaloController haloController;

    CancellationTokenSource cts = new CancellationTokenSource();

    [Header("Components")]
    [SerializeField] Light MainLight;

    [Header("Glow Effect")]
    [SerializeField] float maxGlow;
    [SerializeField] float minGlow;

    [Header("Show Effect")]
    CancellationToken showToken;
    [SerializeField] Color showColor;
    [SerializeField] float showRange;
    [SerializeField] float showMaxIntensity;
    [SerializeField] float showMinIntensity;
    [SerializeField] float glowDuration;

    public void Initialize(HaloController _haloController)
    {
        haloController = _haloController;

        showToken = cts.Token;
        GlowUp(showToken);
    }

    public async Task StopGlowing()
    {
        cts.Cancel();
        haloController.gameObject.SetActive(false);
        await FinishGlowDown();
    }


    async Task GlowUp(CancellationToken showToken)
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            showToken.ThrowIfCancellationRequested();
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(minGlow, maxGlow, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = maxGlow;
        await Task.WhenAll(GlowDown(showToken));
    }

    async Task GlowDown(CancellationToken showToken)
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            showToken.ThrowIfCancellationRequested();
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(maxGlow, minGlow, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = minGlow;
        await Task.WhenAll(GlowUp(showToken));
    }
    public async Task FinishGlowDown()
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(MainLight.range, showRange, t);
            MainLight.intensity = Mathf.Lerp(MainLight.intensity, showMaxIntensity, t);

            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = showRange;
        MainLight.intensity = showMaxIntensity;

    }
}
