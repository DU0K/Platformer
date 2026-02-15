using System.Net;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public static bool finishSceneIsOpen = false;
    private int currentLevel = LevelSelect.CurrentLevelInt;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && finishSceneIsOpen == false)
        {
            SpeedrunTimer.TimerLoop = false;
            finishSceneIsOpen = true;
            audioSource.Play();
            Player.SetActive(false);
            SceneManager.LoadScene("WinScene", LoadSceneMode.Additive);
            if (SpeedrunTimer.TotalTime < SpeedrunTimer.TotalTimeRecord[currentLevel])
            {
                SpeedrunTimer.TotalTimeRecord[currentLevel] = SpeedrunTimer.TotalTime;
                SpeedrunTimer.TotalTime = 0;
                PlayerPrefs.SetFloat("TimeRecordLVL1", SpeedrunTimer.TotalTimeRecord[0]);
                PlayerPrefs.SetFloat("TimeRecordLVL2", SpeedrunTimer.TotalTimeRecord[1]);
                PlayerPrefs.SetFloat("TimeRecordLVL3", SpeedrunTimer.TotalTimeRecord[2]);
                PlayerPrefs.SetFloat("TimeRecordLVL4", SpeedrunTimer.TotalTimeRecord[3]);
                SendMs(NameInputManager.Name ,UiiManagerWinScene.CurrentLevel, ($"{SpeedrunTimer.timerMinutes:00}.{SpeedrunTimer.timerSecondsString}"));
            }
        }
    }

    static void SendMs(string Name, string level, string message)
    {
        string webhook = "https://discord.com/api/webhooks/1472545141070888986/CaW_gKDbjaZXjSVGgoiI9VPeVy-NZoixQjTlHgbLXLO2OjBr8nZI0pdMvfTeRX9gHQmn";

        WebClient client = new WebClient();
        client.Headers.Add("Content-Type", "application/json");
        string payload = "{\"content\": \"" + "UserName: " + Name + ", " + level + ", " + " In: " + message + "\"}";
        client.UploadData(webhook, Encoding.UTF8.GetBytes(payload));
    }
}
