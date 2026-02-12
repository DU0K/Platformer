using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Button Level1;
    [SerializeField] private Button Level2;
    [SerializeField] private Button Level3;
    [SerializeField] private Button Level4;
    [SerializeField] AudioSource AudioSourceSelect;

    private void Start()
    {
        Level1.onClick.AddListener(goToLevel1);
        Level2.onClick.AddListener(goToLevel2);
        Level3.onClick.AddListener(goToLevel3);
        Level4.onClick.AddListener(goToLevel4);
    }
    private void goToLevel1()
    {
        StartCoroutine(WaitForSoundSelect1());
        
    }
    private void goToLevel2()
    {
        StartCoroutine(WaitForSoundSelect2());
        
    }
    private void goToLevel3()
    {
        StartCoroutine(WaitForSoundSelect3());
        
    }
    private void goToLevel4()
    {
        StartCoroutine(WaitForSoundSelect4());
        
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

