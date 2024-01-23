using UnityEngine;

public class EnemyCtl : MonoBehaviour
{
    [SerializeField] int Speed;
    private void FixedUpdate(){
        MoveEnemy(Vector3.down, Speed);
    }

    private void MoveEnemy(Vector3 direction, int moveSpeed){
        //正規化
        direction.Normalize();
        this.transform.position += direction * moveSpeed * Time.deltaTime; 
    }

    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("hit" + collider.gameObject.tag);
        if (collider.gameObject.tag == "PlayerBullet"){
            Destroy(this.gameObject);
        }
    }
}
