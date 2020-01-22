using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private AudioClip selectSfx;
    [SerializeField] private float selectSfxVolume = 0.5f;

    private PlayerHealth player;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        if (player)
        {
            player.OnDeath += Player_OnDeath;
        }        
    }

    private void Player_OnDeath()
    {
        ReloadCurrentScene();
    }

    public void LoadNextScene()
    {
        PlaySelectSfx();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex--;
            Debug.LogWarning("SceneManagement tried to load a scene with a build index greater than the number of scenes in Build Settings. Reloading scene...");
            ReloadCurrentScene();
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadNextSceneWithoutSfx()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
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
        PlaySelectSfx();
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

    private void PlaySelectSfx()
    {
        AudioSource.PlayClipAtPoint(selectSfx, Camera.main.transform.position, selectSfxVolume);
    }
}
