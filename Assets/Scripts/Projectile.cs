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
        rb.linearVelocity = new Vector3(0,0,10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Destructable")){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
}
