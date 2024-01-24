using UnityEngine;

public class EnemyCtl : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] GameObject destroyedObject;
    [SerializeField] ENEMY_TYPE enemyType;

    private GameCtl gameCtl;
    private float offSet;

    private void Awake(){
        gameCtl = GameObject.Find("GameCtl").GetComponent<GameCtl>();
        offSet = Random.Range(0, 2f*Mathf.PI);
    }

    private void FixedUpdate(){
        if(enemyType == ENEMY_TYPE.Simple){
            MoveEnemy(Vector3.down, Speed);    
        }
        if(enemyType == ENEMY_TYPE.Lateral){
            MoveEnemy(Speed);    
        }

    }

    private void MoveEnemy(Vector3 direction, int moveSpeed){
        //正規化
        direction.Normalize();
        this.transform.position += direction * moveSpeed * Time.deltaTime; 
    }

    private void MoveEnemy(int moveSpeed){
        Vector3 movePostion = this.transform.position;
        movePostion += new Vector3(Mathf.Cos(Time.frameCount*0.001f + offSet), -1f, 0f) * Time.deltaTime * moveSpeed; 
        this.transform.position = movePostion;
    }

    private void OnBecameInvisible(){
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "PlayerBullet"){
            gameCtl.AddScore(100);
            Instantiate(destroyedObject, this.transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }

    private enum ENEMY_TYPE{
        Simple,
        Lateral
    }
}
