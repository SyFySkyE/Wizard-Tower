using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    [Header("Indivdual Canvas Objects need to be assigned")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        EnableMenuCanvas();
    }

    public void EnableMenuCanvas()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void EnableCreditsCanvas()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }
}
