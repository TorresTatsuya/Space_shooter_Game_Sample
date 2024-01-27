using UnityEngine;

public class BeamBulletCtl : MonoBehaviour
{
    public int Speed { get; set; }
    // Start is called before the first frame update
    public void SetSpeed(int value)
    {
        Speed = value;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        this.transform.position += Vector3.down * Speed * Time.deltaTime;   
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
