using UnityEngine;

public class EnemyGeneratorCtl : MonoBehaviour
{
    [SerializeField] GameObject generateObject;
    [SerializeField] float GenerateInterval;
    private void Start(){
        InvokeRepeating("RandomGenerate", 0f, GenerateInterval);
    }

    private void RandomGenerate(){
        Vector3 generatePoint = transform.position + new Vector3(Random.Range(-8f, 8f), 0, 0);
        GenerateUnit(generatePoint);
    }

    private void GenerateUnit(Vector3 generatePoint){
        Instantiate(generateObject, generatePoint, Quaternion.identity);
    }
}
