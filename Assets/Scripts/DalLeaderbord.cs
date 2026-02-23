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
		public List<Score> Scores = new();
        private int maxEntries = 10;
        public bool MinimalScore(int point)
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

        public void AddAndSortScore(Score playerscore)
        {
            PlayerScore newEntry = new PlayerScore(name, score);
            Scores.Add(newEntry);

            // 2. Sort the list (Highest score at index 0)
            // This uses a "Lambda" expression: it compares score B to score A
            highScores.Sort((a, b) => b.Score.CompareTo(a.Score));

            // 3. Optional: Trim the list so it only keeps the top X scores
            if (highScores.Count > maxEntries)
            {
                highScores.RemoveRange(maxEntries, highScores.Count - maxEntries);
            }

            Debug.Log($"Added {name}'s score of {score}. Current leader: {highScores[0].Name}");
        }

    }
}