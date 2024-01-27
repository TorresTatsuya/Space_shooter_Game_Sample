// using System;
using System.Collections;
// using Unity.Mathematics;
using UnityEngine;

public class BossCtl : MonoBehaviour
{
    [SerializeField] BombCtl bombBullet;
    [SerializeField] GameObject beamBullet;
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject DestructionObject;
    [SerializeField] int hp;
    private GameCtl gameCtl;
    private GameObject bossBody;
    private Vector3 targetPoint = new Vector3(0, 5f, 0);

    private void Awake(){
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();
        bossBody = GameObject.Find("BossBase");
        beamBullet.GetComponent<BeamBulletCtl>().Speed = 20;
    }

    private void Start(){
        StartCoroutine(MovePosition(targetPoint, 3f));
        StartCoroutine(CPU());
    }

    IEnumerator CPU(){
        while(true){
            yield return new WaitForSeconds(1f);
            yield return RepeatingShot(Random.Range(3, 6), Random.Range(6, 12), 0.5f);
            yield return new WaitForSeconds(1f);
            yield return RapidShotBeam(Random.Range(5, 10));
        }
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
        Vector3 shootPointRight = ShootPoint.position + new Vector3(0.5f, 0, 0);
        Vector3 shootPointLeft = ShootPoint.position + new Vector3(-0.5f, 0, 0);
        for (int x = 0; x < shotWave; x++){
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

    IEnumerator MovePosition(Vector3 targetPostion, float moveSpeed){
        while(transform.position != targetPostion){
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPostion;
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

