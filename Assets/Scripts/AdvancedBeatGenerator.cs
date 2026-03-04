using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AdvancedBeatGenerator
{
    public static void GenerateBeatmap(
        AudioClip clip,
        float bpm,
        out List<BeatTime> lane1,
        out List<BeatTime> lane2,
        out List<BeatTime> lane3,
        int beatSubdivision = 4,
        float sensitivity = 0.5f)
    {
        lane1 = new List<BeatTime>();
        lane2 = new List<BeatTime>();
        lane3 = new List<BeatTime>();

        if (clip == null)
        {
            Debug.LogError("No clip provided.");
            return;
        }

        int sampleRate = clip.frequency;
        int totalSamples = clip.samples;

        float[] samples = new float[totalSamples];
        clip.GetData(samples, 0);

        int windowSize = 1024;
        int historySize = 43;

        float[] historyLow = new float[historySize];
        float[] historyMid = new float[historySize];
        float[] historyHigh = new float[historySize];

        int historyIndex = 0;

        float secondsPerBeat = 60f / bpm;
        float subdivisionLength = 1f / beatSubdivision;

        for (int i = 0; i < totalSamples - windowSize; i += windowSize)
        {
            float low = 0f;
            float mid = 0f;
            float high = 0f;

            for (int j = 0; j < windowSize; j++)
            {
                float sample = samples[i + j];
                float abs = Mathf.Abs(sample);

                if (j < windowSize * 0.2f)
                    low += abs;
                else if (j < windowSize * 0.6f)
                    mid += abs;
                else
                    high += abs;
            }

            float avgLow = Average(historyLow);
            float avgMid = Average(historyMid);
            float avgHigh = Average(historyHigh);

            float time = (float)i / sampleRate;
            float beatPos = time / secondsPerBeat;

            float snappedBeat =
                Mathf.Round(beatPos / subdivisionLength) * subdivisionLength;

            if (low > avgLow * sensitivity)
                AddIfNotDuplicate(lane1, snappedBeat, 20);

            if (mid > avgMid * sensitivity)
                AddIfNotDuplicate(lane2, snappedBeat, 15);

            if (high > avgHigh * sensitivity)
                AddIfNotDuplicate(lane3, snappedBeat, 10);

            historyLow[historyIndex] = low;
            historyMid[historyIndex] = mid;
            historyHigh[historyIndex] = high;

            historyIndex = (historyIndex + 1) % historySize;
        }

        Debug.Log($"Generated beats → L1:{lane1.Count} L2:{lane2.Count} L3:{lane3.Count}");
    }

    private static float Average(float[] arr)
    {
        float sum = 0f;
        for (int i = 0; i < arr.Length; i++)
            sum += arr[i];

        return sum / arr.Length;
    }

    private static void AddIfNotDuplicate(List<BeatTime> list, float time, int number)
    {

        if (list.Count == 0 || Mathf.Abs(list[list.Count - 1].time - time) > 0.05f)
        {
            int random = UnityEngine.Random.Range(0,number);
            switch (random)
            {
                case <= 1:
                    list.Add(new BeatTime { time = time, type = type });
                    break;
                case <= 5:
                    list.Add(new BeatTime { time = time, type = type });
                    break;
                case <= 10:
                    list.Add(new BeatTime { time = time, type = type });
                    break;
                case <= 20:
                    list.Add(new BeatTime { time = time, type = type });
                    break;
                default:
                    list.Add(new BeatTime { time = time, type = type });
                    break;
            }
            
            
        }
    }
}