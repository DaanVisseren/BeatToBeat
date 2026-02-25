using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongMenuSpawner : MonoBehaviour
{
    public SongDatabase songDatabase;    
    public GameObject buttonPrefab;
    public Transform contentParent ;

    void Start()
    {
        // Auto-find content if not assigned
        if (contentParent == null)
        {
            GameObject contentObj = GameObject.FindWithTag("SongContent");
            if (contentObj != null)
                contentParent = contentObj.transform;
            else
            {
                Debug.LogError("No object with tag 'SongContent' found!");
                return;
            }
        }

        // Clear previous buttons (optional)
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Create a button for each song
        foreach (SongData song in songDatabase.allSongs)
        {
            GameObject btnObj = Instantiate(buttonPrefab, contentParent);

            // Set text safely (supports legacy Text or TextMeshProUGUI)
            Text legacyText = btnObj.GetComponentInChildren<Text>();
            if (legacyText != null)
                legacyText.text = song.songName.ToString();
            else
            {
                TMP_Text tmpText = btnObj.GetComponentInChildren<TMP_Text>();
                if (tmpText != null)
                    tmpText.text = song.songName.ToString();
                else
                    Debug.LogError("Button prefab has no Text or TMP_Text component!");
            }

            // Assign the SongData to the button script
            SongSelectButton buttonScript = btnObj.GetComponent<SongSelectButton>();

            if (buttonScript != null)
                buttonScript.songData = song;
        }
    }
}