using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Next scene index: " + nextSceneIndex);
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex--;
            Debug.LogWarning("SceneManagement tried to load a scene with a build index greater than the number of scenes in Build Settings. Reloading scene...");
            ReloadCurrentScene();
        }
        SceneManager.LoadScene(nextSceneIndex);        
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }

    private void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadCurrentScene();
        }
#endif
    }
}
