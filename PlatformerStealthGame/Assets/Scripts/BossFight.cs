using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public bool isBossLevel;
    public GameObject firstDialogue;
    public GameObject middleDialogue;
    public GameObject badEndDialogue;
    public GameObject normalEndDialogue;
    public GameObject TrueEndDialogue;
    public GameObject boss;
    public GameObject videoSource;
    public GameObject oldVideoSource;
    public AudioSource audioSource;
    public AudioClip fightingMusic;
    public AudioClip badEndMusic;
    public AudioClip GoodEndMusic;
    public QuestionPanel QPanel;
    public Dialogue dialogue;
    public PlayerController playerController;
    public bool endofBeginningDialogue = false;
    public bool endofMiddleDialogue = false;
    public bool endofEndingDialogue = false;
    public bool questionAnswered = true;
    public bool calledFirstQ = false;
    public Animator bossDyingAnim;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    private string enemyInQuestion;
    private GameObject enemy;
    //private int score = 0;
    private int noOfQuestionsinaPhase = 2; //even numbers only and do not put less than 3 or else the phase function will break
    private int totalnoOfQuestions;
    private bool haveCard;
    private int phase = 1;
    private int currentQnumber = 0;
    private float percentage = 50f;
    private bool endOf1stPhase = false;
    private bool endOf2ndPhase = false;


    public void Construct(bool _haveCard)
    {
        haveCard = _haveCard;
        // haveCard = _haveCard;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalnoOfQuestions = noOfQuestionsinaPhase * 2;
        StartCoroutine(Wait());
        firstDialogue.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if ((endofBeginningDialogue) || (endofMiddleDialogue))
        {
            if (!calledFirstQ) 
            {
                calledFirstQ = true;
                noOfQuestionsinaPhase--; //to prevent having one extra question in the 1st phase
                QuestionMgr.Instance.Show(); 
            }
            else
            {
                
                if(currentQnumber < noOfQuestionsinaPhase)
                {
                if (questionAnswered)
                    {
                        questionAnswered = false;
                        QuestionMgr.Instance.Show();
                        currentQnumber++;
                    }
                }
                else if ((currentQnumber >= noOfQuestionsinaPhase) && (questionAnswered)) //happens when 1st phase is over && phase is just updated to 2
                {
                    //inserts tv shut down animation
                    Debug.Log("current phase: " + phase);
                    Debug.Log("end of phase 1? " + endOf1stPhase);
                    Debug.Log("end of phase 2? " + endOf2ndPhase);
                    if((endOf1stPhase)&&(!endOf2ndPhase)) //ie phase 1 has just ended
                    {
                        //reset some values & start middle dialogue
                        currentQnumber = 0; //this needs to be in another place
                        noOfQuestionsinaPhase++;
                        questionAnswered = false;
                        //Debug.Log("Boss pops up");
                        //videoSource.GetComponent<VideoPlayer>();
                        //videoSource.GetComponent<VideoPlayer>();
                        videoSource.SetActive(true);
                        oldVideoSource.SetActive(false);
                        boss.SetActive(true);
                        middleDialogue.SetActive(true);
                    }
                    else if(endOf2ndPhase)
                    {
                        decideEnding();
                    }
                }
                else
                {
                    //Debug.Log("how did you get here")
                }
            } 
        }

        /*if ((currentQnumber >= noOfQuestionsinaPhase) && (endOf1stPhase))
        {
            endOf2ndPhase = true;
        }*/
        if ((currentQnumber >= noOfQuestionsinaPhase)&&(phase == 1)) 
        {
            endOf1stPhase = true;
            phase = 2;
            
        }
        else if ((currentQnumber >= noOfQuestionsinaPhase)&&(endofMiddleDialogue))
        {
            endOf2ndPhase = true;
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }

    public void killBoss()
    {
        // GetComponent<Animation>()["boss_dying"].wrapMode = WrapMode.Once;
        //GetComponent<Animation>().Play("boss_dying");
        Animator anim = GetComponent<Animator>();
        anim.SetBool("BossKilled", true);
        spriteRenderer.sprite = newSprite;

    }

    public void decideEnding()
    {
        percentage = QPanel.score / (float)(totalnoOfQuestions);
        Debug.Log("score in BF: " + QPanel.score);
        Debug.Log("noOfQuestions in BF: " + totalnoOfQuestions);
        Debug.Log("percentage in BF: " + percentage);


        if (percentage < 0.34)
        {

            //go to bad ending
            //some more dialogues
            if(audioSource.clip == fightingMusic)
            {
                audioSource.clip = badEndMusic;
                audioSource.Play();
            }
            audioSource.clip = badEndMusic;
            audioSource.Play();
            badEndDialogue.SetActive(true);
            if (endofEndingDialogue)
            {
                //Debug.Log("Bad End");
                if (endofEndingDialogue)
                {
                    SceneManager.LoadScene("bad end");
                }
                //SceneManager.LoadScene("bad end");
            }
        }
        else if ((percentage > 0.33) && (percentage < 0.67))
        {
            
            //go to normal ending
            //play boss dead animation
            killBoss();
            if(audioSource.clip == fightingMusic)
            {
                audioSource.clip = GoodEndMusic;
                audioSource.volume = 100 * 10;
                audioSource.Play();
            }
            
            //some more dialogues
            normalEndDialogue.SetActive(true);
            if (endofEndingDialogue)
            {
                //Debug.Log("Normal End");
                if (endofEndingDialogue)
                {
                    SceneManager.LoadScene("normal end");
                }
                //
            }
        }
        else
        {
            //Debug.Log("True End");
            //play boss dead animation
            killBoss();
            if(audioSource.clip == fightingMusic)
            {
                audioSource.clip = GoodEndMusic;
                audioSource.volume = 100 * 10;
                audioSource.Play();
            }
            
            //some more dialogues
            TrueEndDialogue.SetActive(true);
            haveCard = true;
            if (endofEndingDialogue)
            {
                GameObject.FindObjectOfType<PlayerController>().enabled = true;
            }

            //go to true ending
        }
    }
}
