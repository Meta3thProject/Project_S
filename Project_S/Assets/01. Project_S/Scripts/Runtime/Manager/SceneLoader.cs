using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader : GSingleton<SceneLoader>
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
