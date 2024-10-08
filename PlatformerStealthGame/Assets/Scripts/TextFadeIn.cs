using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshPro tex;
    Color col;
    void Start()
    {
        col = tex.color;
        col.a = 0;
        tex.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        if (col.a < 256)
        {
            col.a += Time.deltaTime;
            tex.color = col;
        }

    }
}