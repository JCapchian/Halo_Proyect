using System;
using System.Threading.Tasks;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    HaloController haloController;

    [Header("Components")]
    [SerializeField] Light MainLight;

    [Header("Glow Effect")]
    [SerializeField] float maxGlow;
    [SerializeField] float minGlow;

    [Header("Show Effect")]
    [SerializeField] Color showColor;
    [SerializeField] float showRange;
    [SerializeField] float showMaxIntensity;
    [SerializeField] float showMinIntensity;
    [SerializeField] float glowDuration;

    public void Initialize(HaloController _haloController)
    {
        haloController = _haloController;
        GlowUp();
    }

    public void StopGlowing()
    {
        FinishGlowDown().Start(); ;
    }


    async Task GlowUp()
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(minGlow, maxGlow, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = maxGlow;
        await Task.WhenAll(GlowDown());
    }

    async Task GlowDown()
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(maxGlow, minGlow, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = minGlow;
        await Task.WhenAll(GlowUp());
    }
    async Task FinishGlowDown()
    {
        var currentTime = 0f;
        while (currentTime < glowDuration)
        {
            float t = currentTime / glowDuration;

            t = t * t * (3f - 2f * t);

            MainLight.range = Mathf.Lerp(MainLight.range, minGlow, t);
            currentTime += Time.deltaTime;

            await Task.Yield();
        }

        MainLight.range = minGlow;

    }
}
