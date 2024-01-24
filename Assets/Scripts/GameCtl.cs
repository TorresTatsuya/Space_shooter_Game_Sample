using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameCtl : MonoBehaviour
{
    public Text ScoreText { get; private set; }
    public Text GameOverText { get; private set;}
    public bool isGameOver { get; set;}
    private int _score = 0;

    private void Awake(){
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        SetScore();
        DisplayGameOverText(false);
    }

    private void Update(){
        if (GameOverText.isActiveAndEnabled == true){
            if (Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    private void SetScore(){
        ScoreText.text = "SCORE " + _score; 
    }

    public void AddScore(int value){
        _score += value;
        SetScore();
    }

    public void DisplayGameOverText(bool isDisplay){
        GameOverText.enabled = isDisplay;
    }

}
