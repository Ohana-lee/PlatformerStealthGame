using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMgr : MonoBehaviour
{
    public Questions questions;
    List<QuestionItem> QuestionItems;
    public static QuestionMgr Instance;
    public QuestionPanel questionPanel;

    public Dictionary<int, QuestionItem> QuestionMap;
    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        QuestionItems = new List<QuestionItem>();
        QuestionMap = new Dictionary<int, QuestionItem>();
        for (int i = 0; i < questions.QuestionItems.Count; i++)
        {
            QuestionItems.Add(questions.QuestionItems[i]);
            QuestionMap.Add(i, questions.QuestionItems[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public QuestionItem GetOneQuetion()
    {
        int index = Random.Range(0, QuestionItems.Count);
        QuestionItem item = QuestionItems[index];
        QuestionItems.Remove(item);
        QuestionMap.Remove(index);
        return item;
    }
    public void Show()
    {
        questionPanel.UpdateItem(GetOneQuetion());
    }
}
