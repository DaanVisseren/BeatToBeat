using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SongDatabasePopulator
{
    [InitializeOnLoadMethod]
    [MenuItem("BeatToBeat/Populate Song Database")]
    public static void PopulateDatabase()
    {
        // Load the SongDatabase asset
        SongDatabase db = AssetDatabase.LoadAssetAtPath<SongDatabase>("Assets/Songs/SongDatabase.asset");
        if (db == null)
        {
            Debug.LogError("SongDatabase.asset not found in Assets!");
            return;
        }

        // Clear the array first
        db.allSongs = new List<SongData>();

        // Load all SongData assets in Assets/Songs folder
        string[] guids = AssetDatabase.FindAssets("t:SongData", new[] { "Assets/Songs" });
        SongData[] songs = new SongData[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            songs[i] = AssetDatabase.LoadAssetAtPath<SongData>(path);
        }

        // Assign the new array to the database
        foreach (SongData temp in songs)
        {
            db.allSongs.Add(temp);
        }
        

        // Mark as dirty and save
        EditorUtility.SetDirty(db);
        AssetDatabase.SaveAssets();

        Debug.Log("Populated SongDatabase with " + songs.Length + " songs!");
    }
}