using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    GameController gameController;

    // [Header("Room #1 Effects")]
    [Header("Dark")]
    [SerializeField] Texture2D[] darkLightmapDir, darkLightmapColor;
    [Header("Bright")]
    [SerializeField] Texture2D[] brightLightmapDir, brightLightmapColor;
    LightmapData[] brightLightmap, darkLightmap;

    public void Initialize(GameController _gameController)
    {
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


    }

    public void SwitchLightsDark()
    {
        LightmapSettings.lightmaps = darkLightmap;
    }
    public void SwitchLightsBright()
    {
        Debug.Log("Bright");
        LightmapSettings.lightmaps = brightLightmap;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
