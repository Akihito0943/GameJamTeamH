using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MultiAudio_Yamashina : MonoBehaviour
{
    public AudioClip[] audioClipsBGM; // Array for multiple BGM clips
    public AudioClip[] audioClipsSE;  // Array for multiple SE clips

    public AudioSource bgmSource;
    public AudioSource seSource;

    // Audio Mixer Groups to assign different mixer settings
    public AudioMixerGroup bgmMixerGroup;
    public AudioMixerGroup seMixerGroup;

    private Dictionary<string, AudioClip> sEClipDictionary;
    private Dictionary<string, AudioClip> BGMClipDictionary;

    // Singleton
    public static MultiAudio_Yamashina ins;

    private void Awake()
    {
        // Singleton pattern
        if (ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        if (audioClipsBGM == null || audioClipsBGM.Length == 0)
        {
            Debug.LogWarning("audioClipsBGM is null or empty!");
        }
        // Initialize dictionaries for easy access by name
        InitializeDictionaries();

        bgmSource=transform.Find("BGM").GetComponent<AudioSource>();
        Debug.Log(bgmSource);
        seSource = transform.Find("SE").GetComponent<AudioSource>();
        Debug.Log(seSource);
        Audiovolume_Yamashina.audioSourceBGM = bgmSource;

        Audiovolume_Yamashina.audioSourceSE = seSource;





    }

    private void Start()
    {

        // Assign mixer groups to the audio sources
        if (bgmSource != null) bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        if (seSource != null)
        {
            seSource.outputAudioMixerGroup = seMixerGroup;
        }


    }

    private void InitializeDictionaries()
    {
        // SE clips
        sEClipDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClipsSE)
        {
            sEClipDictionary[clip.name] = clip;
        }

        // BGM clips
        BGMClipDictionary = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClipsBGM)
        {
            BGMClipDictionary[clip.name] = clip;
        }


    }

    public void PlayBGM_ByName(string bgmName)
    {
#if DEBUG
        if (BGMClipDictionary == null)
        {
            Debug.LogWarning("BGMClipDictionary is not initialized.");
            return;
        }
#endif
        if (BGMClipDictionary.TryGetValue(bgmName, out var clip))
        {
            PlayBGM(clip);
            //Debug.Log($"Playing BGM: {bgmName}");
        }
        else
        {
            Debug.LogWarning("BGM with name not found: " + bgmName);
        }
    }

    public void PlaySEByName(string name)
    {
        if (sEClipDictionary.TryGetValue(name, out var clip))
        {
            PlaySE(clip);
        }
        else
        {
            Debug.LogWarning("SE with name not found: " + name);
        }
    }



    private void PlaySE(AudioClip clip)
    {

        if (clip != null)
        {
            seSource.clip = clip;
            seSource.PlayOneShot(seSource.clip);
            //Debug.Log("Playing SE: " + clip.name);
        }
        else
        {
            Debug.LogWarning("SE clip is null");
        }
    }



    private void PlayBGM(AudioClip clip)
    {
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.loop = true; // Loop playback
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM clip is null");
        }
    }

    // Optional: Play SE by index
    public void ChooseSongs_SE(int index)
    {
        if (index >= 0 && index < audioClipsSE.Length)
        {
            PlaySE(audioClipsSE[index]);
        }
        else
        {
            Debug.LogWarning("SE index out of range");
        }
    }


}
