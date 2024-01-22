using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoCtl : MonoBehaviour
{
    [SerializeField] int speed;
    void FixedUpdate()
    {
        this.transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
