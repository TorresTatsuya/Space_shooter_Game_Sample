using System.Collections;
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
            int randomAction = Random.Range(0, 3);
            switch(randomAction){
                case 0:
                yield return RapidShotBeam(Random.Range(5, 10));
                break;
                case 1:
                yield return ShotCurve(Random.Range(24, 32), 0.1f, Mathf.PI);
                break;
                case 2:
                yield return RepeatingShot(Random.Range(3, 6), Random.Range(6, 12), 0.5f);
                break;
                case 3:
                yield return ShotThreeWay(Random.Range(5, 10));
                break;
            }
            yield return new WaitForSeconds(1f);
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
    IEnumerator ShotThreeWay(int wave){
        for ( int x = 0; x < wave; x++ ){
            if ( player != null ){
                Vector3 playerPositon = player.transform.position + new Vector3(0, 4f, 0);
                Vector3 targetAngle = playerPositon - transform.position;
                float targetPi = Mathf.Atan2( targetAngle.y  , targetAngle.x );
                Debug.Log(player.transform.position);
                Shot( targetPi );
                Shot( targetPi + Mathf.PI / 4);
                Shot( targetPi - Mathf.PI / 4);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void Shot(float anglePi){
        BombCtl bulletPrefab = Instantiate(bombBullet, ShootPoint.position, Quaternion.identity);
        bulletPrefab.SetDirection(anglePi);
    }

    IEnumerator ShotCurve(int valOfBUllet, float interval, float offsetPi){
        float angle =  (Mathf.PI * 2) / valOfBUllet;    
        for ( int x = 0; x < valOfBUllet; x++){
            Shot( angle * x );
            Shot( angle * x + offsetPi );
            yield return new WaitForSeconds(interval);
        } 
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

