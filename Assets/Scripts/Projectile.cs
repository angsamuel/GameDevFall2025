using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 30);
        //rb.linearVelocity = new Vector3(0,0,10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 newVelocity){
        rb.linearVelocity = newVelocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Destructable")){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
