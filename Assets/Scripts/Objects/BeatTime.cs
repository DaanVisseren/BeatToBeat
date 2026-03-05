using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Objects
{
    using UnityEngine;

    [System.Serializable]
    public class BeatTime
    {
        public BeatTypes BeatTypes;
        public float time; // in beats
        [HideInInspector]
        public bool ready = false; // internal use for spawn
        public BeatTime(BeatTypes type, float time)
        {
            BeatTypes = type;
            this.time = time;
        }
        public BeatTime( float time, BeatTypes type)
        {
            BeatTypes = type;
            this.time = time;
        }
        public BeatTime()
        {

        }
    }

}