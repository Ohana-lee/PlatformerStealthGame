using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Questions : ScriptableObject
{
    public List<QuestionItem> QuestionItems = new List<QuestionItem>();
}
