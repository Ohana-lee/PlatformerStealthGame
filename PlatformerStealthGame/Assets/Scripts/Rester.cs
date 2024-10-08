using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rester : MonoBehaviour
{
    public Button yesbutton;
    public Button nobutton;
    void Start()
    {
        yesbutton.onClick.AddListener(onyesclick);
        nobutton.onClick.AddListener(onnoclick);
    }


    public void onyesclick()
    {

        UnityWebRequest rqs = new UnityWebRequest("https://www.ibm.com/uk-en/about?utm_content=SRCWW&p1=Search&p4=43700075078293807&p5=e&gclid=CjwKCAjw9J2iBhBPEiwAErwpeSQWgmlyU8P_H-9wMGxSVPAetHpqWugBRi-r48xI-OKU3JMSptm2gRoCzo8QAvD_BwE&gclsrc=aw.ds");
        Application.OpenURL(rqs.url);
    }

    public void onnoclick()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
