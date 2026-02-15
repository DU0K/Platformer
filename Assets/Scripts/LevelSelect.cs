using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Button Level1;
    [SerializeField] private Button Level2;
    [SerializeField] private Button Level3;
    [SerializeField] private Button Level4;
    [SerializeField] private TMP_Text UserName;
    [SerializeField] AudioSource AudioSourceSelect;

    public static int CurrentLevelInt;

    private void Start()
    {
        Level1.onClick.AddListener(goToLevel1);
        Level2.onClick.AddListener(goToLevel2);
        Level3.onClick.AddListener(goToLevel3);
        Level4.onClick.AddListener(goToLevel4);
        SpeedrunTimer.TotalTimeRecord[0] = PlayerPrefs.GetFloat("TimeRecordLVL1", 6000);
        SpeedrunTimer.TotalTimeRecord[1] = PlayerPrefs.GetFloat("TimeRecordLVL2", 6000);
        SpeedrunTimer.TotalTimeRecord[2] = PlayerPrefs.GetFloat("TimeRecordLVL3", 6000);
        SpeedrunTimer.TotalTimeRecord[3] = PlayerPrefs.GetFloat("TimeRecordLVL4", 6000);
        UserName.text = NameInputManager.Name;
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Collectables.Bronze = false;
            Collectables.Silver = false;
            Collectables.Gold = false;
            SpeedrunTimer.TotalTime = 0;
            if (Input.GetKeyDown(KeyCode.R) && Input.GetKeyDown(KeyCode.Z))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
    private void goToLevel1()
    {
        StartCoroutine(WaitForSoundSelect1());
        SpeedrunTimer.TimerLoop = true;
        UiiManagerWinScene.CurrentLevel = "Level: 1";
        CurrentLevelInt = 0;
    }
    private void goToLevel2()
    {
        StartCoroutine(WaitForSoundSelect2());
        SpeedrunTimer.TimerLoop = true;
        UiiManagerWinScene.CurrentLevel = "Level: 2";
        CurrentLevelInt = 1;
    }
    private void goToLevel3()
    {
        StartCoroutine(WaitForSoundSelect3());
        SpeedrunTimer.TimerLoop = true;
        UiiManagerWinScene.CurrentLevel = "Level: 3";
        CurrentLevelInt = 2;
    }
    private void goToLevel4()
    {
        StartCoroutine(WaitForSoundSelect4());
        SpeedrunTimer.TimerLoop = true;
        UiiManagerWinScene.CurrentLevel = "Level: 4";
        CurrentLevelInt = 3;
    }

    private IEnumerator WaitForSoundSelect1()
    {
        AudioSourceSelect.Play();
        yield return new WaitForSeconds(AudioSourceSelect.clip.length);
        SceneManager.LoadScene("Level1");
    }
    private IEnumerator WaitForSoundSelect2()
    {
        AudioSourceSelect.Play();
        yield return new WaitForSeconds(AudioSourceSelect.clip.length);
        SceneManager.LoadScene("Level2");
    }
    private IEnumerator WaitForSoundSelect3()
    {
        AudioSourceSelect.Play();
        yield return new WaitForSeconds(AudioSourceSelect.clip.length);
        SceneManager.LoadScene("Level3");
    }
    private IEnumerator WaitForSoundSelect4()
    {
        AudioSourceSelect.Play();
        yield return new WaitForSeconds(AudioSourceSelect.clip.length);
        SceneManager.LoadScene("Level4");
    }
}

