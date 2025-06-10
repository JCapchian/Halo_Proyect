using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    GameController gameController;

    public void Initialize(GameController _gameController)
    {
        gameController = _gameController;
    }

    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
