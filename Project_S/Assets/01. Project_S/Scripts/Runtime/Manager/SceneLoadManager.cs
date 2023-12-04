using UnityEngine.SceneManagement;

public class SceneLoadManager : GSingleton<SceneLoadManager>
{
    private void Awake()
    {

    }

    private void Start()
    {

    }

    public void LoadScene(int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);       
    }       // LoadScene()
}
