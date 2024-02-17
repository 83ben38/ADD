using System;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager manager;
    public TextMeshPro text;
    public TutorialScriptableObject tutorial;
    private int phase = -1;
    private int textNum;
    public int tutorialNum;

    private void Start()
    {
        if (!SaveData.save.isTutorialCompleted(tutorialNum))
        {
            manager = this;
            runNext();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void runNext()
    {
        phase++;
        gameObject.SetActive(true);
        TutorialPhase tp = tutorial.phases[phase];
        textNum = 0;
        for (int i = 0; i < tp.disable.Length; i++)
        {
            tp.disable[i].SetActive(false);
        }

        text.text = tp.text[textNum];
    }
    public void Continue()
    {
        textNum++;
        if (textNum == tutorial.phases[phase].text.Length)
        {
            if (phase == tutorial.phases.Length - 1)
            {
                SaveData.save.completeTutorialPhase(tutorialNum);
            }

            gameObject.SetActive(false);
            for (int i = 0; i < tutorial.phases[phase].enable.Length; i++)
            {
                tutorial.phases[phase].enable[i].SetActive(true);
            }
        }
        else
        {
            text.text = tutorial.phases[phase].text[textNum];
        }
    }
}
