using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Tile;

public class SoundManager : MonoBehaviour
{
    public static SoundManager current;
    public static bool SoundEnabled = true;
    public AudioSource MusicSource;
    public AudioSource[] SFXSource;
    public AudioClip boost, crash, sticky, supply, burning, fall, invalid, mainmenu, gameover, gamepause, gametrack, gamelostmusic;
    public Dictionary<TileType, AudioClip> TileSounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        TileSounds = new Dictionary<TileType, AudioClip>
    {
        { TileType.Obstacle, crash },
        { TileType.Normal, null },
        { TileType.Burning, burning },
        { TileType.Supplies, supply },
        { TileType.Boost, boost },
        { TileType.Sticky, sticky },
        { TileType.Empty, fall },
    };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(AudioClip music)
    {
        if (!SoundEnabled)
            return;
        MusicSource.clip = music;
        MusicSource.Play();
        MusicSource.loop = true;
    }

    public void PlaySFX(AudioClip sfx)
    {
        if (!SoundEnabled)
            return;
        bool played = false;
        foreach (AudioSource source in SFXSource)
        {
            if (!source.isPlaying)
            {
                source.clip = sfx;
                source.Play();
                played = true;
                break;
            }
        }

        if (!played)
        {
            SFXSource[0].clip = sfx;
            SFXSource[0].Play();
        }
    }

    public void StopMusic() => MusicSource.Stop();

    public void StopSFX()
    {
        foreach (AudioSource source in SFXSource)
            source.Stop();
    }

    public void MuteGame()
    {
        SoundEnabled = false;
        MusicSource.volume = 0;
        foreach (AudioSource source in SFXSource)
            source.volume = 0;
    }

    public void UnMuteGame()
    {
        SoundEnabled = true;
        MusicSource.volume = 1;
        foreach (AudioSource source in SFXSource)
            source.volume = 1;
    }

    public void SetSoundEnabled(bool val)
    {
        if (val)
            UnMuteGame();
        else
            MuteGame();
    }

    public void PlayBoost() => PlaySFX(boost);
    public void PlayCrash() => PlaySFX(crash);
    public void PlaySticky() => PlaySFX(sticky);
    public void PlaySupply() => PlaySFX(supply);
    public void PlayBurning() => PlaySFX(burning);
    public void PlayFall() => PlaySFX(fall);
    public void PlayInvalid() => PlaySFX(invalid);
    public void PlayMainMenu() => PlayMusic(mainmenu);
    public void PlayGameOver() => PlayMusic(gameover);
    public void PlayGamePause() => PlayMusic(gamepause);
    public void StopMainMenu() => StopMusic();
    public void StopGameOver() => StopMusic();
    public void StopGamePause() => StopMusic();

    public Dictionary<TileType, bool> IsTileSoundPlayingUpdate()
    {
        return new Dictionary<TileType, bool>
        {
            { TileType.Obstacle, SFXSource.Any(source => source.clip == crash && source.isPlaying) },
            { TileType.Normal, false },
            { TileType.Burning, SFXSource.Any(source => source.clip == burning && source.isPlaying) },
            { TileType.Supplies, SFXSource.Any(source => source.clip == supply && source.isPlaying) },
            { TileType.Boost, SFXSource.Any(source => source.clip == boost && source.isPlaying) },
            { TileType.Sticky, SFXSource.Any(source => source.clip == sticky && source.isPlaying) },
            { TileType.Empty, SFXSource.Any(source => source.clip == fall && source.isPlaying) },
        };
    }

}
