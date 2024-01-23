using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGeneratorCtl : MonoBehaviour
{
    [SerializeField] GameObject generateObject;
    private void Start(){
        InvokeRepeating("GenerateUnit", 0f, 2f);
    }

    private void GenerateUnit(){
        Instantiate(generateObject, transform.position, Quaternion.identity);
    }
}
