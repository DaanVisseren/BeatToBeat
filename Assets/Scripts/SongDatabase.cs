using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongDatabase", menuName = "BeatToBeat/Song Database")]
public class SongDatabase : ScriptableObject
{
    public List<SongData> allSongs;
}
