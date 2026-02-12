using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text HPDisplay;
    [SerializeField] private Image HPBar;

    public void UpdateHP(float HP)
    {
        HPDisplay.text = "HP: " + HP.ToString();
        HPBar.fillAmount = HP / 100f;
    }
}
