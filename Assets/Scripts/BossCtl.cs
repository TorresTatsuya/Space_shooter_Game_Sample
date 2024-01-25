using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtl : MonoBehaviour
{
    private Vector3 targetPoint = new Vector3(0, 5f, 0);

    private void Awake(){
        StartCoroutine(MovePosition(targetPoint, 3f));
    }


    IEnumerator MovePosition(Vector3 targetPostion, float moveSpeed){
        while(transform.position != targetPostion){
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPostion;
    }
}

