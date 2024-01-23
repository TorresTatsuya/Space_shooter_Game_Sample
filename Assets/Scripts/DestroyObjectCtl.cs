using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCtl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           Invoke("DestroyObject", 0.7f);
    }

    private void DestroyObject(){
        Destroy(this.gameObject);
    }
}
