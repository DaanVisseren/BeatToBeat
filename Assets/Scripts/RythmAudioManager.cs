using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private const double BeatTolerance = 0.0001;

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

    // -----------------------------
    // SONG LOADING
    // -----------------------------
    public void LoadSong(SongData song)
    {
        if (song == null)
        {
            Debug.LogError("LoadSong called with null SongData!");
            return;
        }

        if (musicSource == null)
        {
            Debug.LogError("MusicSource is null!");
            return;
        }

        if (song.audioClip == null)
        {
            Debug.LogError("SongData has no AudioClip assigned!");
            return;
        }
        currentSong = song;
        if (AreAllLaneBeatsEmpty(currentSong))
        {
            AutoGenerateBeatmap();
        }
        musicSource.clip = currentSong.audioClip;
        secondsPerBeat = 60f / currentSong.bpm;

        ResetPlaybackState();
    }

    // -----------------------------
    // START SONG
    // -----------------------------
    public void StartSong()
    {
        if (musicSource == null || musicSource.clip == null)
        {
            Debug.LogError("No clip loaded!");
            return;
        }

        ResetLaneIndices();

        dspSongStartTime = AudioSettings.dspTime + 1.0f;
        musicSource.PlayScheduled(dspSongStartTime);

        songStarted = true;
    }

    // -----------------------------
    // UPDATE TIMING
    // -----------------------------
    private void Update()
    {
        if (!songStarted || currentSong == null)
            return;

        double songPosition = AudioSettings.dspTime - dspSongStartTime;

        // Clamp before first beat
        songPosition -= currentSong.firstBeatOffset;
        if (songPosition < 0)
        {
            SongPositionInBeats = 0;
            return;
        }

        SongPositionInBeats = songPosition / secondsPerBeat;
    }

    // -----------------------------
    // AUTO BEATMAP GENERATION
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
            4 // 4 = quarter notes
        );
    }

    // -----------------------------
    // LANE SPAWN CHECKS (SAFE)
    // -----------------------------
    public bool ShouldSpawnLane1() =>
        ShouldSpawn(currentSong?.lane1Beats, ref lane1Index);

    public bool ShouldSpawnLane2() =>
        ShouldSpawn(currentSong?.lane2Beats, ref lane2Index);

    public bool ShouldSpawnLane3() =>
        ShouldSpawn(currentSong?.lane3Beats, ref lane3Index);

    // Core spawn logic (frame-skip safe)
    private bool ShouldSpawn(List<float> laneBeats, ref int laneIndex)
    {
        if (!songStarted || laneBeats == null)
            return false;

        bool shouldSpawn = false;

        // Handle multiple skipped beats in one frame
        while (laneIndex < laneBeats.Count &&
               SongPositionInBeats + BeatTolerance >= laneBeats[laneIndex])
        {
            laneIndex++;
            shouldSpawn = true;
        }

        return shouldSpawn;
    }

    // -----------------------------
    // RESET HELPERS
    // -----------------------------
    private void ResetLaneIndices()
    {
        lane1Index = 0;
        lane2Index = 0;
        lane3Index = 0;
    }

    private void ResetPlaybackState()
    {
        songStarted = false;
        SongPositionInBeats = 0;
        ResetLaneIndices();
    }
    private bool AreAllLaneBeatsEmpty(SongData song)
    {
        if (song == null)
            return true;

        bool lane1Empty = song.lane1Beats == null || song.lane1Beats.Count == 0;
        bool lane2Empty = song.lane2Beats == null || song.lane2Beats.Count == 0;
        bool lane3Empty = song.lane3Beats == null || song.lane3Beats.Count == 0;

        return lane1Empty && lane2Empty && lane3Empty;
    }
}