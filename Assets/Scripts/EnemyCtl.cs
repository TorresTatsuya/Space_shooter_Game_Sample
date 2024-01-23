using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

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
}
