using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class BeamBulletCtl : MonoBehaviour
{
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.position += Vector3.down * speed * Time.deltaTime;   
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
