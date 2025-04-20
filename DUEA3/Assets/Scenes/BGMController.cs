using UnityEngine;
using UnityEngine.UI;

public class BGMController : MonoBehaviour
{
    public AudioSource bgmSource;
    public Button toggleButton;
    private bool isPlaying = true;

    void Start()
    {
        bgmSource.Play();
        toggleButton.onClick.AddListener(ToggleBGM);
    }

    void ToggleBGM()
    {
        if (isPlaying)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.Play();
        }
        isPlaying = !isPlaying;
    }
}
