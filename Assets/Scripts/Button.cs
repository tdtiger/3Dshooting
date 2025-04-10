using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void OnClickStart(){
        // "PlayScene"を読み込む
        SceneManager.LoadScene("PlayScene");
    }
}