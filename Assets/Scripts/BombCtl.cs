using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BombCtl : MonoBehaviour
{
   [SerializeField] int speed;
   private Vector3 direction;
    // Start is called before the first frame update
    public void SetDirection(float angle){
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        direction = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.position += direction * speed * Time.deltaTime;   
    }
    
    private void OnBecameInvisible(){
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "Player"){
            Destroy(this.gameObject);
        }
    }
}
