using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Objects
{

    public class NewMonoBehaviour : MonoBehaviour
    {
        public SongDatabase song;

        //Temp
        public SpawnPoints pointSpawner;


        // Use this for initialization
        void Start()
        {
                SongData songToPlay = song.allSongs[0];
                RhythmAudioManager.Instance.LoadSong(songToPlay);
                RhythmAudioManager.Instance.StartSong();         
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}