using UnityEngine;

public class SongSelectButton : MonoBehaviour
{
    public SongData songData;

    public void SelectSong()
    {
        RhythmAudioManager.Instance.LoadSong(songData);
        RhythmAudioManager.Instance.StartSong();
    }
}