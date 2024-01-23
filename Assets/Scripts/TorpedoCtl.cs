using UnityEngine;

public class TorpedoCtl : MonoBehaviour
{
    [SerializeField] int speed;
    private void FixedUpdate()
    {
        this.transform.position += Vector3.up * Time.deltaTime * speed;
    }
    
    private void OnBecameInvisible(){
        Destroy(this.gameObject);
    }
}
