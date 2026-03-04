using Assets.Scripts.Objects;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSong", menuName = "BeatToBeat/Songs")]
public class SongData : ScriptableObject
{
    public string songName;
    public AudioClip audioClip;
    public float bpm;
    public float firstBeatOffset;
    public List<BeatTime> lane1Beats = new();
    public List<BeatTime> lane2Beats = new();
    public List<BeatTime> lane3Beats = new();
}