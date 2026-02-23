using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assets.Scripts
{
    [System.Serializable]
    public class DallLeaderbord
	{
		private List<Score> Scores = new();
        private int maxEntries = 10;
        private bool MinimalScore(int point)
		{
			foreach (Score score in Scores)
			{
				if (score.Points < point)
				{
					return true;
				}
			}
			return false;
		}
		public List<Score> GetScores()
		{
			return Scores;
		}
		public bool DelScores()
		{
			Scores = null;
			return true;
		}

        public bool AddAndSortScore(Score playerscore)
        {
			if (MinimalScore(playerscore.Points))
			{
                Scores.Add(playerscore);

                Scores.Sort((a, b) => b.Points.CompareTo(a.Points));

                if (Scores.Count > maxEntries)
                {
                    Scores.RemoveRange(maxEntries, Scores.Count - maxEntries);
                }

                Debug.Log($"Added {playerscore.Name}'s score of {playerscore.Points}. Current leader: {Scores[0].Name}");
				return true;
            }
			return false;

        }

    }
}