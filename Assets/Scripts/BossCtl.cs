using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BossCtl : MonoBehaviour
{
    [SerializeField] BombCtl bullet;
    [SerializeField] Transform ShootPoint;
    [SerializeField] GameObject DestructionObject;
    [SerializeField] int hp;
    private GameCtl gameCtl;
    private GameObject bossBody;
    private Vector3 targetPoint = new Vector3(0, 5f, 0);

    private void Awake(){
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();
        StartCoroutine(MovePosition(targetPoint, 3f));
        StartCoroutine(RepeatingShot(8, 1f));
        bossBody = GameObject.Find("BossBase");
    }

    private void ShotEveryDirection(int valOfBullet){
            float angle =  (MathF.PI * 2) / valOfBullet;
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
        BombCtl bulletPrefab = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        bulletPrefab.SetDirection(anglePi);
    }
    IEnumerator RepeatingShot(int wave, float interval){
        for (int x = 0; x < wave; x++){
            ShotEveryDirection(8);
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

