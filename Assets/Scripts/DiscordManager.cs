using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions; // Voeg deze toe voor de ID-zoekfunctie

public class DiscordManager : MonoBehaviour
{
    public string webhookUrl = "https://discord.com/api/webhooks/1472635784346734623/K2KZ2CHrBBj3UZ2psecX9Mg0yY2R4xXyVOE8bCbLhhqhGN4O375b0vVLh6vm6ZpJhnQr";

    [System.Serializable]
    private class DiscordMessage { public string content; }

    public void SendOrUpdate(string Name, string level, string message)
    {
        // Maak een unieke sleutel voor de combinatie van naam en level
        // Bijv: "DiscordID_Jan_Level1" of "DiscordID_Jan_Level2"
        string playerKey = "DiscordID_" + Name + "_" + level;

        string savedId = PlayerPrefs.GetString(playerKey, "");

        // De rest van je code blijft hetzelfde...
        string fullContent = $"**Name:** {Name}\n**Level:** {level}\n**Time:** {message}";
        StartCoroutine(SendToDiscord(Name, level, fullContent, savedId));
    }

    IEnumerator SendToDiscord(string nameKey, string levelKey, string contentText, string existingId)
    {
        DiscordMessage msg = new DiscordMessage { content = contentText };
        string jsonPayload = JsonUtility.ToJson(msg);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest request;
        // Zorg dat de URL ALTIJD eindigt op ?wait=true voor POST
        string baseUrl = webhookUrl.Trim();
        if (baseUrl.Contains("?")) baseUrl = baseUrl.Split('?')[0];

        if (string.IsNullOrEmpty(existingId))
        {
            request = new UnityWebRequest(baseUrl + "?wait=true", "POST");
        }
        else
        {
            request = new UnityWebRequest($"{baseUrl}/messages/{existingId}", "PATCH");
        }

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Als het een nieuw bericht was (POST), moeten we het ID opslaan
            if (string.IsNullOrEmpty(existingId))
            {
                string response = request.downloadHandler.text;
                string newId = ExtractIdWithRegex(response);

                if (!string.IsNullOrEmpty(newId))
                {
                    PlayerPrefs.SetString("DiscordID_" + nameKey + "_" + levelKey, newId);
                    PlayerPrefs.Save();
                    Debug.Log($"<color=green>[Discord] Nieuw bericht voor {nameKey} op {levelKey} opgeslagen!</color>");
                }
            }
            else
            {
                // Dit was een PATCH (update). Discord stuurt code 200 en de JSON terug.
                Debug.Log($"<color=cyan>[Discord] Bericht voor {nameKey} op {levelKey} succesvol bijgewerkt!</color>");
            }
        }
        else
        {
            // Echte fouten (zoals 400, 404, 500)
            Debug.LogError($"[Discord Fout] {request.responseCode} - {request.downloadHandler.text}");
            if (request.responseCode == 404) PlayerPrefs.DeleteKey("DiscordID_" + nameKey + "_" + levelKey);
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
