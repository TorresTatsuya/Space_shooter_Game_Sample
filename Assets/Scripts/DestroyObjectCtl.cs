using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCtl : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
           Invoke("DestroyObject", lifeTime);
    }

    private void DestroyObject(){
        Destroy(this.gameObject);
    }
}
