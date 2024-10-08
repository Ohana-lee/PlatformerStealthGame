using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
  public void Continue()
  {
      //int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
      //SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
      //SceneManager.LoadScene("Sprites+BG", LoadSceneMode.Additive);
      SceneManager.LoadScene(1);
  }
}
