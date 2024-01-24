using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject destroyObject;
    [SerializeField] Transform firePointRight;
    [SerializeField] Transform firePointLeft;
    [SerializeField] AudioClip fireSE;
    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

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
        audioSource.PlayOneShot(fireSE);
        Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
        Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "EnemyUnit"){
            Instantiate(destroyObject, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
