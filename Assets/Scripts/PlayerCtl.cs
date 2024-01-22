using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePointRight;
    [SerializeField] Transform firePointLeft;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            FireTorpedo();
        }
    }
    void FixedUpdate()
    {
        MoveUnit();
    }

    //移動関数
    private void MoveUnit(){
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(inputX, inputY, 0f);

        //斜め移動が速くならないように正規化
        moveDirection.Normalize();

        this.transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }

    private void FireTorpedo(){
        Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
        Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
    }
}
