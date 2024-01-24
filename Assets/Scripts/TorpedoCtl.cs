using UnityEngine;

public class TorpedoCtl : MonoBehaviour
{
    [SerializeField] int maxSpeed;
    private float accelerateTime = 0;
    private void FixedUpdate()
    {
        if(accelerateTime <= 1 ){
            accelerateTime += 0.03f;
        }
        float acceleratespeed = Mathf.Lerp(1f, maxSpeed, accelerateTime);
        this.transform.position += Vector3.up * Time.deltaTime * acceleratespeed;
        Debug.Log(Vector3.up * Time.deltaTime * acceleratespeed);
    }
    
    private void OnBecameInvisible(){
        Destroy(this.gameObject);
    }
}
