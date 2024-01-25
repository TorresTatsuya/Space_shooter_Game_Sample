using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtl : MonoBehaviour
{
    [SerializeField] BombCtl bullet;
    [SerializeField] Transform ShootPoint;
    private Vector3 targetPoint = new Vector3(0, 5f, 0);

    private void Awake(){
        StartCoroutine(MovePosition(targetPoint, 3f));
        InvokeRepeating("shotThreeWay", 2f, 1f);
    }

    private void shotThreeWay(){
        Shot(-Mathf.PI / 2 );
        Shot(-Mathf.PI / 4 );
        Shot(-Mathf.PI * 3 / 4 );
    }

    private void Shot(float anglePi){
        BombCtl bulletPrefab = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        bulletPrefab.SetDirection(anglePi);
    }

    IEnumerator MovePosition(Vector3 targetPostion, float moveSpeed){
        while(transform.position != targetPostion){
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPostion;
    }
}

