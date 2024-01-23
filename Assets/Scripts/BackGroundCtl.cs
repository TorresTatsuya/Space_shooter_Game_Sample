using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCtl : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    void FixedUpdate(){
        if (transform.position.y <= -20.5f){
            transform.position = new Vector3(transform.position.x, 0f, 0f);
        }
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }
}
