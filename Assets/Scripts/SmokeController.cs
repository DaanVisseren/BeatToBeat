using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class SmokeController: MonoBehaviour
	{
        public ParticleSystem particle;
        public PlayerMovement player;

        // Use this for initialization
        void Start()
		{

		}
        void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
		{
            if (player != null && !player.notDisable())
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                    Debug.Log("Smoke");
                }
            }
        }
	}
}