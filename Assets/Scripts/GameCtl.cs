using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;


public class GameCtl : MonoBehaviour
{
    public Text ScoreText { get; private set; }
    public Text AmmoText { get; private set; }
    public Text ReloadingText { get; private set; }
    public Text GameOverText { get; private set;}
    public Image ReloadingBar { get; private set;}
    public bool isGameOver { get; set;}
    private float _score = 0;

    private void Awake(){
        Screen.SetResolution(1920, 1080, false);
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        AmmoText = GameObject.Find("Ammo").GetComponent<Text>();
        GameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        ReloadingText = GameObject.Find("Reloading").GetComponent<Text>();
        ReloadingBar = GameObject.Find("ReloadBar").GetComponent<Image>();
        SetScore();
        DisplayGameOverText(false);
        DisplayReloading(false);
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

    public void DisplayReloading(bool isDisplay){
        ReloadingBar.enabled = isDisplay;
        ReloadingText.enabled = isDisplay;
    }

    public void DisplayReloadBar(float reloading, float maxReloadTime){
        ReloadingBar.transform.localScale = new Vector3(reloading / maxReloadTime , ReloadingBar.transform.localScale.y , 0);
    }

}
