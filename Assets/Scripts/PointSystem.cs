using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointSystem : MonoBehaviour
{
    //the lower the index, the higher the accuracy level (0 is best!)
    public List<float> pointsGainedBasedOnAccuracy;
    //Again, lower index is better (0 best) This has to have the same amount + 1 of items as the list above
    public List<float> timingForPoints;
    public float maxTimeOff;
    public float maxMultiplier;
    public float hitAmountForMultIncrease;
    public float multIncreaseAmount;

    public float currentStreak;
    public float currentMultiplier = 1;

    public float totalPoints;
    void Start()
    {
        maxTimeOff = timingForPoints[timingForPoints.Count - 1];
    }

    void Update()
    {
        
    }


    //The lower the accuracy, the better
    public void GainPoints(float timeOff)
    {
        totalPoints += pointsGainedBasedOnAccuracy[CalculateAccuracy(timeOff)] * currentMultiplier;
        GainMultiplier();
    }

    public void GainMultiplier()
    {
        hitAmountForMultIncrease++;
        currentStreak++;
        if (currentStreak % hitAmountForMultIncrease == 0)
        {
            hitAmountForMultIncrease = 0;
            if(currentMultiplier <= maxMultiplier)
            {
                currentMultiplier += multIncreaseAmount;
            }
        }
    }

    public int CalculateAccuracy(float timeOff)
    {
        int accuracy = 0;
        foreach(float pointTiming in timingForPoints)
        {
            if(timeOff <= pointTiming)
            {
                break;
            }
            accuracy++;
        }
        return accuracy;
    }

    public void BreakStreak()
    {
        currentStreak = 0;
        currentMultiplier = 1;
    }
}
