using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class UIAbilityController : MonoBehaviour
{
    public Image GrayoutIcon;

    public TextMeshProUGUI ColldownText;

    // Start is called before the first frame update
    void Start()
    {
        GrayoutIcon.fillAmount = 0;
        ColldownText.SetText("");
    }

    public void SetCooldown(float remainingTime, float totalCooldown)
    {
        if (remainingTime == 0f)
        {
            SetAvailable();
            return;
        }
        var cooldownPercentage = Percentage.GetPercentage(remainingTime, totalCooldown);
        GrayoutIcon.fillAmount = cooldownPercentage;
        var cooldownText = remainingTime.ToString("F1");
        ColldownText.SetText(cooldownText + "s");
    }

    public void SetAvailable()
    {
        GrayoutIcon.fillAmount = 0f;
        ColldownText.SetText("");
    }
}