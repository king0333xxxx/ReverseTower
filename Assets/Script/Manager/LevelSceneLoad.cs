using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSceneLoad : MonoBehaviour
{
    public int LevelLoad;
    public string SceneName;

    public void MulaiLevel(int LevelLoad)
    {
        string sceneName = "Level" + LevelLoad; // Pastikan scene name mengikuti format Level1, Level2, dst
        Debug.Log("Loading " + sceneName);
        SceneManager.LoadScene(sceneName);

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);


    }

    public void OnMouseDown()
    {
        MulaiLevel(LevelLoad);
    }

}
