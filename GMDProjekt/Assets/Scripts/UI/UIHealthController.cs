using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    public GameObject PlayerThatHasHpController;

    private Slider _healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerThatHasHpController.GetComponent<HpController>().onHealthChange.AddListener(UpdateHealth);
        _healthSlider = GetComponent<Slider>();
        _healthSlider.value = 100;
    }

    void UpdateHealth(int currentHp, int maxHp)
    {
        var currentHpPercentage = (float)currentHp / maxHp;
        if (currentHpPercentage < 0)
        {
            currentHpPercentage = 0;
        }

        _healthSlider.value = currentHpPercentage;
    }
}