using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Звуки игрока")]
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    [Header("Звуки врагов")]
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioClip bossDeath;

    [Header("Фоновая музыка")]
    public AudioClip backgroundMusic;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 0.3f;
        sfxSource.volume = 0.5f;

        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip, volume);
    }
}