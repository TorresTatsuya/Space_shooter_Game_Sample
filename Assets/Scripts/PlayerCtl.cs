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
    private GameCtl gameCtl;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();

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

        Vector3 movePosition = transform.position + (moveDirection * Time.deltaTime * moveSpeed);

        //移動範囲の制限
        float moveRangeLimitX = Mathf.Clamp(movePosition.x, -8.5f, 8.5f);
        float moveRangeLimitY = Mathf.Clamp(movePosition.y, -5.5f, 3.5f);
        movePosition = new Vector3(moveRangeLimitX, moveRangeLimitY, movePosition.z);

        this.transform.position = movePosition;
    }

    private void FireTorpedo(){
        audioSource.PlayOneShot(fireSE);
        Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
        Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "EnemyUnit" || collider2D.gameObject.tag == "EnemyBullet"){
            Instantiate(destroyObject, this.transform.position, Quaternion.identity);
            gameCtl.DisplayGameOverText(true);
            Destroy(this.gameObject);
        }
    }
}
