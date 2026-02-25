using UnityEngine;

[CreateAssetMenu(fileName = "NewSong", menuName = "BeatToBeat/Songs")]
public class SongData : ScriptableObject
{
    public string songName;
    public AudioClip audioClip;
    public float bpm;
    public float firstBeatOffset;
}