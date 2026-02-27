using UnityEngine;

public class RhythmAudioManager : MonoBehaviour
{
    public static RhythmAudioManager Instance;

    public AudioSource musicSource;
    public SongData currentSong;

    private double dspSongStartTime;
    private double secondsPerBeat;
    private bool songStarted = false;
    private int lane1Index = 0;
    private int lane2Index = 0;
    private int lane3Index = 0;

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
        lane1Index = 0;
        lane2Index = 0;
        lane3Index = 0;

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

    public void AutoGenerateBeatmap()
    {
        AdvancedBeatGenerator.GenerateBeatmap(
            currentSong.audioClip,
            currentSong.bpm,
            out currentSong.lane1Beats,
            out currentSong.lane2Beats,
            out currentSong.lane3Beats,
            4 // 4 = quarter notes, 8 = eighth notes, etc.
        );
    }

    public bool ShouldSpawnLane1()
    {
        if (lane1Index >= currentSong.lane1Beats.Count)
            return false;

        if (SongPositionInBeats >= currentSong.lane1Beats[lane1Index])
        {
            lane1Index++;
            return true;
        }

        return false;
    }

    public bool ShouldSpawnLane2()
    {
        if (lane2Index >= currentSong.lane2Beats.Count)
            return false;

        if (SongPositionInBeats >= currentSong.lane2Beats[lane2Index])
        {
            lane2Index++;
            return true;
        }

        return false;
    }

    public bool ShouldSpawnLane3()
    {
        if (lane3Index >= currentSong.lane3Beats.Count)
            return false;

        if (SongPositionInBeats >= currentSong.lane3Beats[lane3Index])
        {
            lane3Index++;
            return true;
        }

        return false;
    }
}