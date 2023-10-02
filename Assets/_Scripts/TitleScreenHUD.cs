using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenHUD : MonoBehaviour
{
    [field: SerializeField] private Scene _gameplayScene;

    public void OnStartGameButtonPressed()
    {
        SceneManager.LoadScene(1); 
    }
}
