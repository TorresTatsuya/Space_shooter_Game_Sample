// using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Build;


// using Unity.Mathematics;
using UnityEngine;

public class BossCtl : MonoBehaviour
{
    [SerializeField] BombCtl bombBullet;
    [SerializeField] GameObject beamBullet;
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject DestructionObject;
    [SerializeField] int hp;
    private GameObject player;
    private GameCtl gameCtl;
    private GameObject bossBody;
    private Vector3 targetPoint = new Vector3(0, 5f, 0);
    private bool isMoving = true;

    private void Awake(){
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();
        bossBody = GameObject.Find("BossBase");
        beamBullet.GetComponent<BeamBulletCtl>().Speed = 20;
        player = GameObject.Find("PlayerUnit");
    }

    private void Start(){
        StartCoroutine(MoveFirstPosition(targetPoint, 3f));
        StartCoroutine(CPU());
    }

    private void FixedUpdate(){
        if(player != null && isMoving == false ){
            float playerPositonX = player.transform.position.x;
            if(playerPositonX != this.transform.position.x){
                Vector3 direction = new Vector3( playerPositonX - this.transform.position.x, 0, 0);
                direction.Normalize();
                MoveBoss(direction, 1f);
            }
        }
    }

    IEnumerator CPU(){
        while(true){
            yield return new WaitForSeconds(1f);
            yield return RepeatingShot(Random.Range(3, 6), Random.Range(6, 12), 0.5f);
            yield return new WaitForSeconds(1f);
            yield return RapidShotBeam(Random.Range(5, 10));
        }
    }

    private void MoveBoss(Vector3 direction, float speed){
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void ShotEveryDirection(int valOfBullet){
            float angle =  (Mathf.PI * 2) / valOfBullet;
        for (int num = 0; num < valOfBullet; num++){
            Shot(angle * num);
        }
    }
    private void ShotThreeWay(){
        Shot(-Mathf.PI / 2 );
        Shot(-Mathf.PI / 4 );
        Shot(-Mathf.PI * 3 / 4 );
    }

    private void Shot(float anglePi){
        BombCtl bulletPrefab = Instantiate(bombBullet, ShootPoint.position, Quaternion.identity);
        bulletPrefab.SetDirection(anglePi);
    }

    IEnumerator RapidShotBeam(int shotWave){
        for (int x = 0; x < shotWave; x++){
            Vector3 shootPointRight = ShootPoint.position + new Vector3(0.5f, 0, 0);
            Vector3 shootPointLeft = ShootPoint.position + new Vector3(-0.5f, 0, 0);
            GameObject bullet = Instantiate(beamBullet, shootPointRight, Quaternion.identity);
            bullet.GetComponent<BeamBulletCtl>().SetSpeed(10);
            bullet = Instantiate(beamBullet, shootPointLeft, Quaternion.identity);
            bullet.GetComponent<BeamBulletCtl>().SetSpeed(10);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator RepeatingShot(int wave, int numOfBullet, float interval){
        for (int x = 0; x < wave; x++){
            ShotEveryDirection(numOfBullet);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator MoveFirstPosition(Vector3 targetPostion, float moveSpeed){
        while(transform.position != targetPostion){
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPostion;
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.CompareTag("PlayerBullet")){
            Hitted();
            Destroy(collider2D.gameObject);
        }
    }

    private void Hitted(){
        hp -= 1;
        bossBody.GetComponent<SpriteRenderer>().color=new Color32(255,0,0,255);
        Invoke("ResetColor", 0.2f);
        gameCtl.AddScore(5);
        if(hp == 0){
            Instantiate(DestructionObject, this.transform.position, Quaternion.identity);
            gameCtl.AddScore(1000);
            Destroy(this.gameObject);
        }
    }

    private void ResetColor(){
        bossBody.GetComponent<SpriteRenderer>().color=new Color32(255,255,255,255);
    }
}

