using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Objects
{

    public class NewMonoBehaviour : MonoBehaviour
    {
        public SongDatabase song;
        // Use this for initialization
        void Start()
        {
            foreach (SongData temp in song.allSongs)
            {
                if (temp != null && temp is SongData)
                {
                    RhythmAudioManager.Instance.LoadSong(temp);
                }
                RhythmAudioManager.Instance.StartSong();
                if (RhythmAudioManager.Instance.isActiveAndEnabled == true)
                {
                    Debug.Log("Music on");
                }
            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}