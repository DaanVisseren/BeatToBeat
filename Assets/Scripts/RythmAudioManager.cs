using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;

public class RhythmAudioManager : MonoBehaviour
{
    public static RhythmAudioManager Instance;

    [Header("Audio")]
    public AudioSource musicSource;
    public SongData currentSong;

    private double dspSongStartTime;
    private double secondsPerBeat;
    private bool songStarted = false;

    private int lane1Index = 0;
    private int lane2Index = 0;
    private int lane3Index = 0;

    public double SongPositionInBeats { get; private set; }

    private const double BeatTolerance = 0.01; // small margin for timing

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (musicSource == null)
            Debug.LogError("MusicSource not assigned in Inspector!");
        else
            Debug.Log("RhythmAudioManager Awake complete.");
    }

    // -----------------------------
    // LOAD SONG
    // -----------------------------
    public void LoadSong(SongData song)
    {
        Debug.Log("LoadSong called.");
        if (song == null || musicSource == null || song.audioClip == null)
        {
            Debug.LogError("Invalid SongData or AudioSource!");
            return;
        }

        currentSong = song;

        if (AreAllLaneBeatsEmpty(currentSong))
        {
            Debug.Log("No beats found, auto-generating...");
            AutoGenerateBeatmap();
        }

        musicSource.clip = currentSong.audioClip;
        secondsPerBeat = 60f / currentSong.bpm;

        ResetPlaybackState();
        Debug.Log($"Loaded song: {currentSong.songName} ({currentSong.bpm} BPM)");
        Debug.Log($"Lane1 beats: {currentSong.lane1Beats.Count}, Lane2 beats: {currentSong.lane2Beats.Count}, Lane3 beats: {currentSong.lane3Beats.Count}");
    }

    // -----------------------------
    // START SONG
    // -----------------------------
    public void StartSong()
    {
        Debug.Log("StartSong called.");
        if (musicSource == null || musicSource.clip == null)
        {
            Debug.LogError("No clip loaded!");
            return;
        }

        ResetLaneIndices();
        dspSongStartTime = AudioSettings.dspTime + 0.5; // schedule 0.5s in future
        musicSource.PlayScheduled(dspSongStartTime);
        songStarted = true;

        Debug.Log($"Song started at DSP time {dspSongStartTime}");
    }

    // -----------------------------
    // UPDATE TIMING
    // -----------------------------
    private void Update()
    {
        if (!songStarted || currentSong == null)
            return;

        double songPosition = AudioSettings.dspTime - dspSongStartTime - currentSong.firstBeatOffset;
        SongPositionInBeats = Mathf.Max(0f, (float)(songPosition / secondsPerBeat));

        Debug.Log($"Update: SongPositionInBeats = {SongPositionInBeats:F3}");

        // Optional: check the next beats in each lane for debugging
        if (currentSong.lane1Beats != null && lane1Index < currentSong.lane1Beats.Count)
            Debug.Log($"Next lane1 beat: {currentSong.lane1Beats[lane1Index].time}, ready={currentSong.lane1Beats[lane1Index].ready}");

        if (currentSong.lane2Beats != null && lane2Index < currentSong.lane2Beats.Count)
            Debug.Log($"Next lane2 beat: {currentSong.lane2Beats[lane2Index].time}, ready={currentSong.lane2Beats[lane2Index].ready}");

        if (currentSong.lane3Beats != null && lane3Index < currentSong.lane3Beats.Count)
            Debug.Log($"Next lane3 beat: {currentSong.lane3Beats[lane3Index].time}, ready={currentSong.lane3Beats[lane3Index].ready}");
    }

    // -----------------------------
    // AUTO BEATMAP GENERATION (placeholder)
    // -----------------------------
    public void AutoGenerateBeatmap()
    {
        if (currentSong == null || currentSong.audioClip == null)
        {
            Debug.LogError("Cannot generate beatmap: invalid SongData.");
            return;
        }

        AdvancedBeatGenerator.GenerateBeatmap(
            currentSong.audioClip,
            currentSong.bpm,
            out currentSong.lane1Beats,
            out currentSong.lane2Beats,
            out currentSong.lane3Beats,
            4,       // beat subdivision
            1.5f     // sensitivity
        );
    }

    // -----------------------------
    // SPAWN CHECKS (multi-beat)
    // -----------------------------
    public List<BeatTime> ShouldSpawnLane1() =>
        ShouldSpawnAllReady(currentSong?.lane1Beats, ref lane1Index);

    public List<BeatTime> ShouldSpawnLane2() =>
        ShouldSpawnAllReady(currentSong?.lane2Beats, ref lane2Index);

    public List<BeatTime> ShouldSpawnLane3() =>
        ShouldSpawnAllReady(currentSong?.lane3Beats, ref lane3Index);

    // -----------------------------
    // MULTI-BEAT SPAWN LOGIC
    // -----------------------------
    private List<BeatTime> ShouldSpawnAllReady(List<BeatTime> laneBeats, ref int laneIndex)
    {
        List<BeatTime> readyBeats = new List<BeatTime>();

        if (!songStarted)
        {
            Debug.Log("ShouldSpawnAllReady: song not started yet.");
            return readyBeats;
        }

        if (laneBeats == null)
        {
            Debug.Log("ShouldSpawnAllReady: laneBeats is null.");
            return readyBeats;
        }

        if (laneIndex >= laneBeats.Count)
        {
            Debug.Log("ShouldSpawnAllReady: laneIndex exceeded beat count.");
            return readyBeats;
        }

        while (laneIndex < laneBeats.Count)
        {
            BeatTime beat = laneBeats[laneIndex];
            if (beat == null)
            {
                Debug.Log($"Skipping null beat at index {laneIndex}");
                laneIndex++;
                continue;
            }

            if (SongPositionInBeats + BeatTolerance >= beat.time)
            {
                if (!beat.ready)
                {
                    beat.ready = true;
                    readyBeats.Add(beat);
                    Debug.Log($"Beat ready! LaneIndex={laneIndex}, BeatTime={beat.time}, Type={beat.BeatTypes}");
                }
                laneIndex++;
            }
            else
            {
                break; // next beat not ready yet
            }
        }

        return readyBeats;
    }

    // -----------------------------
    // HELPERS
    // -----------------------------
    private void ResetLaneIndices()
    {
        lane1Index = lane2Index = lane3Index = 0;
        Debug.Log("Lane indices reset.");
    }

    private void ResetPlaybackState()
    {
        songStarted = false;
        SongPositionInBeats = 0;
        ResetLaneIndices();

        // reset all beat ready flags
        if (currentSong != null)
        {
            foreach (var b in currentSong.lane1Beats) if (b != null) b.ready = false;
            foreach (var b in currentSong.lane2Beats) if (b != null) b.ready = false;
            foreach (var b in currentSong.lane3Beats) if (b != null) b.ready = false;
        }

        Debug.Log("Playback state reset.");
    }

    private bool AreAllLaneBeatsEmpty(SongData song)
    {
        if (song == null) return true;
        return (song.lane1Beats == null || song.lane1Beats.Count == 0) &&
               (song.lane2Beats == null || song.lane2Beats.Count == 0) &&
               (song.lane3Beats == null || song.lane3Beats.Count == 0);
    }
}