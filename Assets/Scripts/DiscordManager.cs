using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions; // Voeg deze toe voor de ID-zoekfunctie

public class DiscordManager : MonoBehaviour
{
    private string webhookUrl = "https://discord.com/api/webhooks/1472601777445671116/Joe-TVOtJLiSyUxcckQDrm_cB69SQzScKqZhFd4xvAlNcGnMXDCcFqjFvYBlLixSovfP";

    public string gameVersie = "1.0.0"; // Deze kun je nu aanpassen in de Inspector

    [System.Serializable]
    private class DiscordMessage { public string content; }

    public void SendOrUpdate(string Name, string currentLevel, string score)
    {
        // STAP 1: Maak de level naam exact gelijk aan wat we verwachten (bv. "Level 1")
        // Als currentLevel "Level: 1" is, maken we er "Level 1" van.
        string formattedLevel = currentLevel.Replace(":", "").Trim();

        // STAP 2: Sla de score op met de opgeschoonde naam
        PlayerPrefs.SetString($"Score_{Name}_{formattedLevel}", score);
        PlayerPrefs.Save();

        // STAP 3: Haal alle scores op. Let op dat de namen exact "Level 1", "Level 2" etc. zijn
        string L1 = PlayerPrefs.GetString($"Score_{Name}_Level 1", "--:--:--");
        string L2 = PlayerPrefs.GetString($"Score_{Name}_Level 2", "--:--:--");
        string L3 = PlayerPrefs.GetString($"Score_{Name}_Level 3", "--:--:--");
        string L4 = PlayerPrefs.GetString($"Score_{Name}_Level 4", "--:--:--");

        // STAP 4: Bouw de tekst
        string fullContent = $"**Speler:** {Name} (Versie: {gameVersie})\n" +
                             $"----------------------------\n" +
                             $"**Level 1:** {L1}\n" +
                             $"**Level 2:** {L2}\n" +
                             $"**Level 3:** {L3}\n" +
                             $"**Level 4:** {L4}\n" +
                             $"----------------------------";

        string prefsKey = "DiscordMsgID_" + Name;
        string actualDiscordID = PlayerPrefs.GetString(prefsKey, "");

        StartCoroutine(SendToDiscord(Name, fullContent, actualDiscordID, prefsKey));
    }

    IEnumerator SendToDiscord(string nameKey, string contentText, string existingId, string prefsKey)
    {
        DiscordMessage msg = new DiscordMessage { content = contentText };
        string jsonPayload = JsonUtility.ToJson(msg);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest request;
        string baseUrl = webhookUrl.Trim();
        if (baseUrl.Contains("?")) baseUrl = baseUrl.Split('?')[0];

        // DE FIX ZIT HIER:
        if (string.IsNullOrEmpty(existingId))
        {
            // Nieuw bericht: URL is gewoon de webhook
            request = new UnityWebRequest(baseUrl + "?wait=true", "POST");
        }
        else
        {
            // Bewerken: URL MOET eindigen op het ID (het getal), NIET op de naam van de PlayerPrefs-sleutel
            // Gebruik hier 'existingId', NIET 'prefsKey'
            request = new UnityWebRequest($"{baseUrl}/messages/{existingId}", "PATCH");
        }

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (string.IsNullOrEmpty(existingId))
            {
                string response = request.downloadHandler.text;
                string newId = ExtractIdWithRegex(response);
                if (!string.IsNullOrEmpty(newId))
                {
                    PlayerPrefs.SetString(prefsKey, newId); // Hier slaan we het ID op onder de sleutel
                    PlayerPrefs.Save();
                    Debug.Log("<color=green>ID opgeslagen!</color>");
                }
            }
        }
        else
        {
            Debug.LogError($"[Discord Fout] {request.responseCode} - {request.downloadHandler.text}");
            // Als je een 400 blijft krijgen, komt dat omdat er nog troep in je PlayerPrefs staat.
            // Wis het dan eenmalig:
            if (request.responseCode == 400) PlayerPrefs.DeleteKey(prefsKey);
        }
    }

    private string ExtractIdWithRegex(string json)
    {
        // Zoekt naar een reeks van 17 tot 20 cijfers achter "id":
        Match match = Regex.Match(json, "\"id\":\\s*\"(\\d+)\"");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    }
}
