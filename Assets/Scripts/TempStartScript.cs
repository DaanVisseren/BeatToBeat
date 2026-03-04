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
            // Check Lane 1
            if (RhythmAudioManager.Instance.ShouldSpawnLane1())
            {
                pointSpawner.CreatePoint(0, pointSpawner.slashPointPrefab);
                Debug.Log("Lane 1");
            }


            // Check Lane 2
            if (RhythmAudioManager.Instance.ShouldSpawnLane2())
            {
                pointSpawner.CreatePoint(1, pointSpawner.slashPointPrefab);
                Debug.Log("Lane 2");
            }

            // Check Lane 3
            if (RhythmAudioManager.Instance.ShouldSpawnLane3())
            {
                pointSpawner.CreatePoint(2, pointSpawner.slashPointPrefab);
                Debug.Log("Lane 3");
            }
        }
    }
}