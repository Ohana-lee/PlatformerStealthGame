using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionControl : MonoBehaviour
{
    public GameObject questionUI;
    public GameObject trueTipUI;
    public GameObject falseTipUI;

    // Start is called before the first frame update
    public void pressButtonA()
    {
        // GameObject.Find("robot").GetComponent<BoxCollider2D>().enabled = false;

        questionUI.SetActive(false);
        falseTipUI.SetActive(true);
        StartCoroutine(controlFalseTipUI());
        

    }
    public void pressButtonB()
    {
        // GameObject.Find("robot").GetComponent<BoxCollider2D>().enabled = false;
        questionUI.SetActive(false);
        falseTipUI.SetActive(true);
        StartCoroutine(controlFalseTipUI());



    }
    public void pressButtonC()
    {
        // GameObject.Find("robot").GetComponent<BoxCollider2D>().enabled = false;
        questionUI.SetActive(false);
        trueTipUI.SetActive(true);
        StartCoroutine(controlTrueTipUI());
        Debug.Log("pressed");

    }
    public void pressButtonD()
    {
        GameObject.Find("robot").GetComponent<BoxCollider2D>().enabled = false;
        questionUI.SetActive(false);
        falseTipUI.SetActive(true);
        StartCoroutine(controlFalseTipUI());


    }
    IEnumerator controlTrueTipUI()
    {
        yield return new WaitForSeconds(2);
        trueTipUI.SetActive(false);
        GameObject.Find("mc_sprite_walk_1").GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator controlFalseTipUI()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("mc_sprite_walk_1").GetComponent<PlayerController>().enabled = true;
        falseTipUI.SetActive(false);
    }
}
