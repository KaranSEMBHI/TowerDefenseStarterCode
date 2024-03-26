using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource menuMusic;
    public AudioSource gameMusic;
    public GameObject audioSourcePrefab;
    public AudioClip[] uiSounds;
    public AudioClip[] towerSounds;
    public AudioClip[] fxs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void StartMenuMusic()
    {
        gameMusic.Stop();
        menuMusic.Play();
    }

    public void StartGameMusic()
    {
        menuMusic.Stop();
        gameMusic.Play();
    }

    public void PlayUISound()
    {
        int index = Random.Range(0, uiSounds.Length);
        PlaySound(uiSounds[index]);
    }

    public void PlayTowerSound(int towerTypeIndex)
    {
        if (towerTypeIndex >= 0 && towerTypeIndex < towerSounds.Length)
        {
            PlaySound(towerSounds[towerTypeIndex]);
        }
    }

    public void PlayFXSound(FXType fxType)
    {
        int index = (int)fxType;
        if (index >= 0 && index < fxs.Length)
        {
            PlaySound(fxs[index]);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(soundGameObject, clip.length);
    }
}

public enum FXType
{
    StartGame,
    StartWave,
    EnemyReachesEnd,
    WinGame,
    LoseGame,
    BuildTower
}
