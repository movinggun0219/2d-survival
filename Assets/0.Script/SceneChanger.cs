using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Singleton<SceneChanger>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CharactorSelet()
    {
        SceneManager.LoadScene("Select");    
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene("UI",LoadSceneMode.Additive); // 알아보기 additive
    }
}
