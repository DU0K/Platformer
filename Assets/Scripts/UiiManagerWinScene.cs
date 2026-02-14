using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UiiManagerWinScene : MonoBehaviour
{
    [SerializeField] private Image bronzeImage;
    [SerializeField] private Image silverImage;
    [SerializeField] private Image goldImage;
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private TMP_Text Level;
    public static string CurrentLevel;
    private AudioSource audioSource;
    private void Start()
    {
        Level.text = CurrentLevel;
        audioSource = GetComponent<AudioSource>();
        bronzeImage.color = Color.black;
        silverImage.color = Color.black;
        goldImage.color = Color.black;
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

        tmp.text = ($"{SpeedrunTimer.timerMinutes:00}:{SpeedrunTimer.timerSeconds}");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Finish.finishSceneIsOpen = false;
            StartCoroutine(WaitForSoundSelect());
        }
    }

    private IEnumerator WaitForSoundSelect()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Collectables.Bronze = false;
        Collectables.Silver = false;
        Collectables.Gold = false;
        SceneManager.LoadScene("LevelSelector");
    }
}
