using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    [Header("Indivdual Canvas Objects need to be assigned")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject creditsCanvas;

    [SerializeField] private AudioClip selectSfx;
    [SerializeField] private float selectSfxVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        EnableMenuCanvas();
    }

    public void EnableMenuCanvas()
    {
        PlaySelectSfx();
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void EnableCreditsCanvas()
    {
        PlaySelectSfx();
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    private void PlaySelectSfx()
    {
        AudioSource.PlayClipAtPoint(selectSfx, Camera.main.transform.position, selectSfxVolume);
    }
}
