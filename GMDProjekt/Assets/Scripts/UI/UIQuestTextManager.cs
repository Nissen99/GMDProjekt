using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuestTextManager : MonoBehaviour
{
    public TextMeshProUGUI QuestText;

    private void Awake()
    {
        QuestText.text = "";
    }

    public void SetQuestText(string text)
    {
        QuestText.text = text;
    }
}
