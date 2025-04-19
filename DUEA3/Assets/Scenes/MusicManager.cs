using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    private bool isPlaying = true;

    void Start()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    public void ToggleMusic()
    {
        if (musicSource != null)
        {
            if (isPlaying)
                musicSource.Pause();
            else
                musicSource.Play();

            isPlaying = !isPlaying;
        }
    }
}
