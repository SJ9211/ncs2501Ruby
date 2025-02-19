using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI shadowText;
    private int countRobotLeft;
    float timerDisplay;

    void Start()
    {
        timerDisplay = -1.0f;
        countRobotLeft = GameObject.FindGameObjectsWithTag("ENEMY").Length;
        dialogBox.SetActive(false);
        SetDisplayText();
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

    public bool NoticeRobotFixed()
    {
        countRobotLeft--;
        bool isCompleted = (countRobotLeft <= 0) ? true : false;
        /*
        if (countRobotLeft <= 0)
        {
            isCompleted = true;
        }
        else
        {
            isCompleted = false;
        }
        */
        SetDisplayText(isCompleted);
        return isCompleted;
    }


    private void SetDisplayText(bool isCompleted = false)
    {
        if (isCompleted)
        {
            shadowText.text = questText.text =
            " Very Good job!\nyou made it!";
        }
        else
        {
            shadowText.text = questText.text = $"Help!\nFix the Robots!\nLeft: {countRobotLeft}";
        }

    }
}
