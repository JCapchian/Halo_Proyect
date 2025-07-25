using UnityEngine;

[CreateAssetMenu(fileName = "PopUpScritable", menuName = "Scriptable Objects/PopUpScritable")]
public class PopUpScriptable : ScriptableObject
{
    public string Title;
    public string Description;
    public string ButtonText;
    public AudioStruc ShowAudio;
    public AudioStruc HideAudio;
    public GameObject PopUpPrefab;
}
