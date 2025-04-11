using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button tutorialButton;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private List<GameObject> tutorialPages;

    private int page = 0;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Button optionButton;

    [SerializeField]
    private Canvas tutorialCanvas;

    void Start(){
        // 各ボタンにクリック時に実行する関数を割り当て
        startButton.onClick.AddListener(PlayGame);
        tutorialButton.onClick.AddListener(ShowTutorial);
        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(BackPage);
        exitButton.onClick.AddListener(ExitTutoeial);
        tutorialCanvas.enabled = false;
        for(int i = 1; i < tutorialPages.Count; i++){
            tutorialPages[i].SetActive(false);
        }
    }

    private void PlayGame(){
        // ゲームシーンに遷移
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
    }

    private void ShowTutorial(){
        tutorialCanvas.enabled = true;
        nextButton.gameObject.SetActive(true);
        tutorialPages[0].SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    private void NextPage(){
        if(page != tutorialPages.Count - 1){
            tutorialPages[page].SetActive(false);
            page += 1;
            tutorialPages[page].SetActive(true);
            backButton.gameObject.SetActive(true);
        }

        // 最後のページの時は次に進むボタンを表示しない
        if(page == tutorialPages.Count - 1){
            nextButton.gameObject.SetActive(false);
        }
    }

    private void BackPage(){
        if(page != 0){
            tutorialPages[page].SetActive(false);
            page -= 1;
            tutorialPages[page].SetActive(true);
            nextButton.gameObject.SetActive(true);
        }

        // 最初のページの時は前に戻るボタンを表示しない
        if(page == 0){
            backButton.gameObject.SetActive(false);
        }
    }

    private void ExitTutoeial(){
        tutorialCanvas.enabled = false;
        page = 0;
        for(int i = 1; i < tutorialPages.Count; i++){
            tutorialPages[i].SetActive(false);
        }
    }
}