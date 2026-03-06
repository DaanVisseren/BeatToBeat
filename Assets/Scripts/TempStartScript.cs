using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{

    public class NewMonoBehaviour : MonoBehaviour
    {
        public SongDatabase song;
        public PointSystem pointSystem;
        //Temp
        public SpawnPoints pointSpawner;

        public List<BeatTime> lane1Beats = new();
        public List<BeatTime> lane2Beats = new();
        public List<BeatTime> lane3Beats = new();
        private bool _actionPerformed = false;

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
            if (RhythmAudioManager.Instance.SongPositionInBeats > 68 && !_actionPerformed)
            {
                pointSystem.ShowEndScreen();
                _actionPerformed = true;
            }
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

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 10.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 10.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 11.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 12.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 12.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 13.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 14.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 14.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 15.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Standard, o + 16.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 17.0f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 18.0f));

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
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 26f));

            lane3Beats.Add(new BeatTime(BeatTypes.Side, o + 28f));
            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 28.5f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 29f));

            lane1Beats.Add(new BeatTime(BeatTypes.Crash, o + 30.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 30.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 31.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 32.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Standard, o + 32.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 33.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 34.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Crash, o + 34.5f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 35.0f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 36.0f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 36.0f));

            lane3Beats.Add(new BeatTime(BeatTypes.Side, o + 38.0f));
            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 38.5f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 39.0f));

            // ========== FINAL BUILD (40-60s HARD) ==========

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 40f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 40.25f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 40.5f));

            lane1Beats.Add(new BeatTime(BeatTypes.Side, o + 41f));
            lane2Beats.Add(new BeatTime(BeatTypes.Side, o + 41.25f));
            lane3Beats.Add(new BeatTime(BeatTypes.Side, o + 41.5f));

            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 42f));
            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 42f));

            lane2Beats.Add(new BeatTime(BeatTypes.Touch, o + 43f));
            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 43f));

            lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + 44f));
            lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + 44f));

            // Rapid ending spam (very hard)

            for (float t = 45f; t <= 60f; t += 1.5f)
            {
                lane1Beats.Add(new BeatTime(BeatTypes.Touch, o + t));
                lane2Beats.Add(new BeatTime(BeatTypes.Standard, o + t + 0.3f));
                lane3Beats.Add(new BeatTime(BeatTypes.Touch, o + t + 0.6f));
            }
        }
    }
}