using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestionItem 
{
    public string Title;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public string OptionD;
    public RightOption Right;   
    
    public enum RightOption
    {
        A,
        B,
        C,
        D
    }
}

