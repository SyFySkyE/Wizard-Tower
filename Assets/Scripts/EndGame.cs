using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private UnityEngine.Playables.PlayableDirector timeline;

    private void Start()
    {
        timeline = GetComponent<UnityEngine.Playables.PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeline.Play();
        }
    }

    public void EndTheGame()
    {
#if UNITY_WEBGL
        FindObjectOfType<SceneManagement>().LoadScene(0);
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }
}
