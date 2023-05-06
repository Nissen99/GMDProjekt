using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
    }
}
