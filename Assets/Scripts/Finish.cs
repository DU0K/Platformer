using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private DiscordManager discordManager;
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
                discordManager.SendOrUpdate(NameInputManager.Name ,UiiManagerWinScene.CurrentLevel, ($"{SpeedrunTimer.timerMinutes:00}.{SpeedrunTimer.timerSecondsString}"));
            }
        }
    }
}
