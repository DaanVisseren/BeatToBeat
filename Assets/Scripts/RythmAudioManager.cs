using UnityEngine;

public class RhythmAudioManager : MonoBehaviour
{
    public static RhythmAudioManager Instance;

    public AudioSource musicSource;
    private SongData currentSong;

    private double dspSongStartTime;
    private double secondsPerBeat;
    private bool songStarted = false;

    public double SongPositionInBeats { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (musicSource == null)
        {
            Debug.LogError("MusicSource not assigned in Inspector!");
        }
    }

    public void LoadSong(SongData song)
    {
        currentSong = song;
        if (musicSource == null)
        {
            Debug.LogError("MusicSource is null!");
            return;
        }

        musicSource.clip = song.audioClip;
        secondsPerBeat = 60f / song.bpm;
    }

    public void StartSong()
    {
        if (musicSource.clip == null)
        {
            Debug.LogError("No clip loaded!");
            return;
        }

        dspSongStartTime = AudioSettings.dspTime + 1.0f;
        musicSource.PlayScheduled(dspSongStartTime);
        songStarted = true;
    }

    private void Update()
    {
        if (!songStarted) return;

        double songPosition = AudioSettings.dspTime - dspSongStartTime - currentSong.firstBeatOffset;
        SongPositionInBeats = songPosition / secondsPerBeat;
    }
}