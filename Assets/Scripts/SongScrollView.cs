using UnityEngine;
using UnityEngine.UI;

public class SongScrollView : MonoBehaviour
{
    public SongDatabase songDatabase;    // Your ScriptableObject with all songs
    public GameObject songButtonPrefab;  // Prefab with SongSelectButton + UI Button
    public Transform content;            // The Content object of your ScrollView

    private void Start()
    {
        PopulateSongs();
    }

    private void PopulateSongs()
    {
        // Clear existing buttons
        foreach (Transform child in content)
        {
            Debug.Log("Child destroying");
            Destroy(child.gameObject);
        }

        // Loop through all songs and create a button for each
        foreach (SongData song in songDatabase.allSongs)
        {
            Debug.Log("Uit Songdatabase halen");
            GameObject newButton = Instantiate(songButtonPrefab, content);

            // Set song data on the SongSelectButton component
            SongSelectButton selectButton = newButton.GetComponent<SongSelectButton>();
            if (selectButton != null)
            {
                selectButton.songData = song;
            }

            // Optional: Set button text (if prefab has a Text component)
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = song.songName; // Assuming SongData has a songName field
            }
        }
    }
}