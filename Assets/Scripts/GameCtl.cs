using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameCtl : MonoBehaviour
{
    public Text ScoreText { get; private set; }
    public Text AmmoText { get; private set; }
    public Text GameOverText { get; private set;}
    public bool isGameOver { get; set;}
    private float _score = 0;

    private void Awake(){
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        AmmoText = GameObject.Find("Ammo").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        SetScore();
        DisplayGameOverText(false);
    }

    private void Update(){
        if (GameOverText.isActiveAndEnabled == true){
            if (Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene("MainScene");
            }
        }else{
            TimeAddScore(20);
        }
    }

    private void SetScore(){
        ScoreText.text = "SCORE " + (int)_score; 
    }

    public void SetAmmo(int value){
        AmmoText.text = "* " + value;
    }

    private void TimeAddScore(int addScorePerSec){
        _score += Time.deltaTime * addScorePerSec; 
        SetScore();
    }

    public void AddScore(int value){
        _score += value;
        SetScore();
    }

    public void DisplayGameOverText(bool isDisplay){
        GameOverText.enabled = isDisplay;
    }

}
