using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    private UnityEngine.Playables.PlayableDirector timeline;

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<UnityEngine.Playables.PlayableDirector>();
    }

    public void StartCutsceneTimeline()
    {
        timeline.Play();
    }
}
