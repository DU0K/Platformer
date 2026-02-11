using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text HPDisplay;

    public void UpdateHP(int HP)
    {
        HPDisplay.text = "HP: " + HP.ToString();
    }
}
