using UnityEngine;

[CreateAssetMenu(fileName = "SceneScritableObject", menuName = "Scriptable Objects/SceneScritableObject")]
public class SceneScriptableObject : ScriptableObject
{
    public int index;
    public LoadMode loadMode;
    public AudioStruc musicScene;
}
