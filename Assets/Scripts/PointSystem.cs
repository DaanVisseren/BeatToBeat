using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PointSystem : MonoBehaviour
{
    //the lower the index, the higher the accuracy level (0 is best!)
    public List<float> pointsGainedBasedOnAccuracy;
    public float pointsFromBlank;
    //Again, lower index is better (0 best) This has to have the same amount + 1 of items as the list above
    public List<float> timingForPoints;
    public List<float> negativeTimingForPoints;
    //
    public List<string> scoreTexts;
    public List<string> negativeScoreTexts;
    //
    public List<Color> scoreColors;
    public string missText;
    public Color missColor;
    public float maxTimeOff;
    public float negativeMaxTimeOff;
    public float maxMultiplier;
    public float hitAmountForMultIncrease;
    public float multIncreaseAmount;

    public float currentStreak;
    public float currentMultiplier = 1;
    public float highestStreak = 0;

    public float totalPoints;

    public TextMeshProUGUI pointTxt;
    public TextMeshProUGUI multTxt;
    public TextMeshProUGUI streakTxt;
    public TextMeshProUGUI hitScoreTxt;
    public float fadeOutSpeed;
    private float a;
    private Color currentColor;

    //Endscreen 
    public Canvas endScreenCanvas;
    public TextMeshProUGUI endScreenPointsTxt;
    public TextMeshProUGUI endScreenStreakTxt;

    void Start()
    {
        maxTimeOff = timingForPoints[timingForPoints.Count - 1];
        negativeMaxTimeOff = negativeTimingForPoints[timingForPoints.Count - 1];
    }


    //The lower the accuracy, the better
    public void GainPoints(float timeOff)
    {
        totalPoints += pointsGainedBasedOnAccuracy[CalculateAccuracy(timeOff)] * currentMultiplier;
        totalPoints = Mathf.Round(totalPoints * 10.0f) * 0.1f;
        GainMultiplier();
        UpdateTextUI();
    }
    //For gaing points from blanks
    public void GainPoints()
    {
        totalPoints += pointsFromBlank * currentMultiplier;
        totalPoints = Mathf.Round(totalPoints * 10.0f) * 0.1f;
        GainMultiplier();
        UpdateTextUI();
    }

    public void GainMultiplier()
    {
        currentStreak++;
        if(currentStreak > highestStreak) { highestStreak = currentStreak;  }
        if (currentStreak % hitAmountForMultIncrease == 0)
        {
            if(currentMultiplier <= maxMultiplier)
            {
                currentMultiplier += multIncreaseAmount;
                currentMultiplier = Mathf.Round(currentMultiplier * 10.0f) * 0.1f;
            }
        }
    }

    public int CalculateAccuracy(float timeOff)
    {
        int accuracy = 0;

        if(timeOff >= 0)
        {
            foreach (float pointTiming in timingForPoints)
            {
                if (timeOff <= pointTiming)
                {
                    break;
                }
                accuracy++;
            }
            ShowHitScore(accuracy, scoreTexts);
            return accuracy;
        }
        else
        {
            foreach (float pointTiming in negativeTimingForPoints)
            {
                if (timeOff >= pointTiming)
                {
                    break;
                }
                accuracy++;
            }
            ShowHitScore(accuracy, negativeScoreTexts);
            return accuracy;
        }
    }

    public void BreakStreak()
    {
        currentStreak = 0;
        currentMultiplier = 1;
        ShowHitScore();
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        pointTxt.text ="Points: " + totalPoints.ToString();
        multTxt.text = "Multiplier: " + currentMultiplier.ToString() + "x";
        streakTxt.text = "Streak: " + currentStreak.ToString();
    }

    public void ShowHitScore(int accuracy, List<string> list)
    {
        a = 1;
        currentColor = new Color(scoreColors[accuracy].r, scoreColors[accuracy].g, scoreColors[accuracy].b, a);
        hitScoreTxt.color = currentColor;
        hitScoreTxt.text = list[accuracy];
    }

    public void ShowHitScore()
    {
        a = 1;
        currentColor = new Color(missColor.r, missColor.g, missColor.b, a);
        hitScoreTxt.color = currentColor;
        hitScoreTxt.text = missText;
    }

    public void ShowEndScreen()
    {
        endScreenCanvas.enabled = !endScreenCanvas.enabled;
        endScreenPointsTxt.text = "Total Points: " + totalPoints;
        endScreenStreakTxt.text = "Highest Streak: " + highestStreak;
    }

    void Update()
    {
        if(hitScoreTxt.color.a != 0)
        {
            a -= Time.deltaTime*fadeOutSpeed;
            Color color = new Color(currentColor.r, currentColor.g, currentColor.b, a);
            hitScoreTxt.color = color;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShowEndScreen();
        }
    }

}
