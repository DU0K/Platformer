using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text HPDisplay;
    [SerializeField] private Image HPBar;

    [SerializeField] private Image bronzeImage;
    [SerializeField] private Image silverImage;
    [SerializeField] private Image goldImage;

    private void Start()
    {
        bronzeImage.color = Color.black;
        silverImage.color = Color.black;
        goldImage.color = Color.black;
        CheckForCollectables();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SpeedrunTimer.TimerLoop = false;
            SpeedrunTimer.timerMinutes = 0;
            SpeedrunTimer.TotalTime = 0;
            SceneManager.LoadScene("LevelSelector");
        }
    }

    public void UpdateHP(float HP)
    {
        HPDisplay.text = "HP: " + HP.ToString();
        HPBar.fillAmount = HP / 100f;
    }

    public void CheckForCollectables()
    {
        if (Collectables.Bronze)
        {
            bronzeImage.color = Color.white;
        }
        if (Collectables.Silver == true)
        {
            silverImage.color = Color.white;
        }
        if (Collectables.Gold == true)
        {
            goldImage.color = Color.white;
        }
    }
}
