using UnityEngine;

public class EnemyCtl : MonoBehaviour
{
    [SerializeField] int Speed;
    [SerializeField] GameObject destroyedObject;
    private void FixedUpdate(){
        MoveEnemy(Vector3.down, Speed);
    }

    private void MoveEnemy(Vector3 direction, int moveSpeed){
        //正規化
        direction.Normalize();
        this.transform.position += direction * moveSpeed * Time.deltaTime; 
    }

    private void OnBecameInvisible(){
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "PlayerBullet"){
            Instantiate(destroyedObject, this.transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
