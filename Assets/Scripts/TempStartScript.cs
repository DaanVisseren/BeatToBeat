using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{

    public class NewMonoBehaviour : MonoBehaviour
    {
        public SongDatabase song;

        //Temp
        public SpawnPoints pointSpawner;

        public List<BeatTime> lane1Beats = new();
        public List<BeatTime> lane2Beats = new();
        public List<BeatTime> lane3Beats = new();

        // Use this for initialization
        void Start()
        {
                SongData songToPlay = song.allSongs[0];
            tempfill();
            songToPlay.lane1Beats = lane1Beats;
            songToPlay.lane2Beats = lane2Beats;
            songToPlay.lane3Beats = lane3Beats;
            RhythmAudioManager.Instance.LoadSong(songToPlay);
                RhythmAudioManager.Instance.StartSong();         
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void tempfill()
        {

            float o = 2.5f; // offset

            // ========== INTRO (Slow Build 0-20s) ==========

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 0.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 1.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 2f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 2.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 3.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 4f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 4.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 5.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Standard, o + 6f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 7f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 8f));

            // ========== MID BUILD (20-40s) ==========

            lane1Beats.Add(new BeatTime(BeatTypes.Crash, o + 20f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 20.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 21f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 22f));
            lane2Beats.Add(new BeatTime(BeatTypes.Standard, o + 22.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 23f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 24f));
            lane2Beats.Add(new BeatTime(BeatTypes.Crash, o + 24.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 25f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 26f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 26f)); // double hit

            lane3Beats.Add(new BeatTime(BeatTypes.Side, o + 28f));
            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 28.5f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 29f));

            // ========== FINAL BUILD (40-60s HARD) ==========

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 40f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 40.25f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 40.5f));

            lane1Beats.Add(new BeatTime(BeatTypes.Side, o + 41f));
            lane2Beats.Add(new BeatTime(BeatTypes.Side, o + 41.25f));
            lane3Beats.Add(new BeatTime(BeatTypes.Side, o + 41.5f));

            lane1Beats.Add(new BeatTime(BeatTypes.Crash, o + 42f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 42f));

            lane2Beats.Add(new BeatTime(BeatTypes.Crash, o + 43f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 43f));

            lane3Beats.Add(new BeatTime(BeatTypes.Crash, o + 44f));
            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 44f));

            // Rapid ending spam (very hard)

            for (float t = 45f; t <= 60f; t += 1f)
            {
                lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + t));
                lane2Beats.Add(new BeatTime(BeatTypes.Side, o + t + 0.1f));
                lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + t + 0.2f));
            }
        }
    }
}