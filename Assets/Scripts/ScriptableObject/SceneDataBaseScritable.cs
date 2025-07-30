using UnityEngine;

[CreateAssetMenu(fileName = "SceneDataBaseScritable", menuName = "Scriptable Objects/SceneDataBaseScritable")]
public class SceneDataBaseScritable : ScriptableObject
{
    [SerializeField]
    public SceneScriptableObject[] sceneScritableObjects;
}
