using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCtl : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject destroyObject;
    [SerializeField] Transform firePointRight;
    [SerializeField] Transform firePointLeft;
    [SerializeField] AudioClip fireSE;
    private int remainingAmmo;
    private AudioSource audioSource;
    private GameCtl gameCtl;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();
        remainingAmmo = 5;
        gameCtl.SetAmmo(remainingAmmo);

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            FireTorpedo();
        }
        if( remainingAmmo == 0){
            Reload(3);
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
        if ( 0 < remainingAmmo ){
            audioSource.PlayOneShot(fireSE);
            Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
            Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
            remainingAmmo -= 1;
            gameCtl.SetAmmo(remainingAmmo);
        }
    }

    private float reloading = 0;
    private void Reload(float reloadTime){
        gameCtl.DisplayReloading(true);
        reloading += Time.deltaTime;
        if ( reloadTime <= reloading ){
            remainingAmmo = 5;
            gameCtl.SetAmmo(remainingAmmo);
            gameCtl.DisplayReloading(false);
            reloading = 0;
        }


    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "EnemyUnit" || collider2D.gameObject.tag == "EnemyBullet"){
            Instantiate(destroyObject, this.transform.position, Quaternion.identity);
            gameCtl.DisplayGameOverText(true);
            Destroy(this.gameObject);
        }
    }
}
