using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    //GameObject BackgroundMusic;
    public AudioSource backmusic;

    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
       // BackgroundMusic = GameObject.Find("BGM");
        //backmusic = this.GetComponent<AudioSource>(); // πË∞Ê¿Ωæ« ¿˙¿Â«ÿµ“ 
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
        {
            //backmusic.Play();
            if (backmusic.isPlaying) return;
            else
            {
                backmusic.Play();
            }
        }
        else
        {
            backmusic.Pause();
        }
    }
}
