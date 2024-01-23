using UnityEngine;
using UnityEngine.UI;


public class GameCtl : MonoBehaviour
{
    public Text ScoreText { get; private set; }
    private int _score = 0;

    private void Awake(){
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        SetScore();
    }

    private void SetScore(){
        ScoreText.text = "SCORE " + _score; 
    }

    public void AddScore(int value){
        _score += value;
        SetScore();
    }
}
