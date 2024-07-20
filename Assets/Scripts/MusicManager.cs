using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource mainMusicSource;

    public AudioSource loseCoinSource;
    public AudioSource loseCoinsSource;
    public AudioSource gainCoinSource;
    public AudioSource plantSource;
    public AudioSource harvestSource;
    public AudioSource wateringSource;
    public GameObject muteUI;
    public GameObject unmuteUI;

    void Awake()
    {
        mainMusicSource.Play();
        if (PlayerPrefs.GetInt("sound")==0)
        {
            MuteAndUnmuteAllMusic();
        }
    }

    public void MuteAndUnmuteAllMusic()
    {
        muteUI.SetActive(!mainMusicSource.mute);
        unmuteUI.SetActive(mainMusicSource.mute);
        loseCoinSource.mute = !mainMusicSource.mute;
        loseCoinsSource.mute = !mainMusicSource.mute;
        gainCoinSource.mute = !mainMusicSource.mute;
        plantSource.mute = !mainMusicSource.mute;
        harvestSource.mute = !mainMusicSource.mute;
        wateringSource.mute = !mainMusicSource.mute;
        mainMusicSource.mute = !mainMusicSource.mute;
        PlayerPrefs.SetInt("sound", mainMusicSource.mute ? 0 : 1);

    }
    public void LoseCoinAudioClip()
    {
        loseCoinSource.Play();

    }
    public void LoseCoinsAudioClip()
    {
        loseCoinsSource.Play();

    }
    public void GainCoinAudioClip()
    {
        gainCoinSource.Play();

    }
    public void PlantAudioClip()
    {
        plantSource.Play();

    }
    public void HarvestAudioClip()
    {
        harvestSource.Play();

    }
    public void WateringAudioClip()
    {
        wateringSource.Play();

    }

}
