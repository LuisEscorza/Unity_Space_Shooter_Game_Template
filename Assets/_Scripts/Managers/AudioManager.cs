using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [field: Header("Sources")]
    [field: SerializeField] private AudioSource[] _sfxSources;
    [field: SerializeField] private AudioSource _musicSource;
    public static AudioManager Instance;
    #endregion

    #region Misc. Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Audio Methods

    public void PlaySFX(AudioClip soundClip, float volume = 1.0f, float pitch = 1.0f)
    {
        foreach (AudioSource source in _sfxSources)
        {
            if (!source.isPlaying)
            {
                source.clip = soundClip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                return; //exit early
            }
        }
    }

    public void PlayMusic(AudioClip musicClip, float volume = 1.0f)
    {
        _musicSource.clip = musicClip;
        _musicSource.volume = volume;
        _musicSource.Play();
    }
    #endregion
}
