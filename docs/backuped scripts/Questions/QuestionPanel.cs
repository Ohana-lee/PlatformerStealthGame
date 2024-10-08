using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanel : MonoBehaviour
{
    private static QuestionPanel _instance;
    public static QuestionPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<QuestionPanel>();
            }
            return _instance;
        }
    }
    public CanvasGroup canvasGroup;
    public Text text_title;
    public Text text_tip;
    public Toggle[] toggles;
    private int currentSelect;
    private QuestionItem.RightOption right;

    public PlayerController script;

    public void Awake()
    {
        _instance = this;
        text_tip.gameObject.SetActive(false);
        toggles = transform.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((v) =>
            {
                if (v)
                {
                    currentSelect = index;
                    canvasGroup.interactable = false;
                    text_tip.gameObject.SetActive(true);
                    //text_tip.text = index == (int)right ? "Select Right!" : "Select Wrong! Correct: <color=green>" + right.ToString()+"</color>";
                    if (index == (int)right)
                    {
                        text_tip.text = "Correct!";
                        PlayerController.currentTime += 10f;
                    }
                    else
                    {
                        text_tip.text = "Incorrect! Correct: <color=green>" + right.ToString() + "</color>";
                        PlayerController.currentTime -= 10f;
                    }
                    StartCoroutine(_Close());
                }
            });
        }
    }
    IEnumerator _Close()
    {
        yield return new WaitForSeconds(2);
        text_tip.gameObject.SetActive(false);
        gameObject.SetActive(false);
        GameObject.FindObjectOfType<PlayerController>().enabled = true;
    }
    public void UpdateItem(QuestionItem item)
    {
        canvasGroup.interactable = true;
        gameObject.SetActive(true);
        currentSelect = -1;
        text_title.text = item.Title;
        Toggle toA = toggles[0];
        toA.transform.Find("Label").GetComponent<Text>().text = item.OptionA;
        toA.isOn = false;
        Toggle toB = toggles[1];
        toB.transform.Find("Label").GetComponent<Text>().text = item.OptionB;
        toB.isOn = false;
        Toggle toC = toggles[2];
        toC.transform.Find("Label").GetComponent<Text>().text = item.OptionC;
        toC.isOn = false;
        Toggle toD = toggles[3];
        toD.transform.Find("Label").GetComponent<Text>().text = item.OptionD;
        toD.isOn = false;
        right = item.Right;
    }
}