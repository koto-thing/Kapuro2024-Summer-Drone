using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    // @brief シーンの読み込み
    // @param sceneName シーン名
    public static void LoadScene(String sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }
}
