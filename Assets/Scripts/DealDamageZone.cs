using UnityEngine;

public class DealDamageZone : MonoBehaviour
{


    public bool debbugOneDamage = false;
    Damage damage;

    void Start(){
        if(debbugOneDamage){
            damage = new Damage(1);
        }
    }



    public void StartDamage(Damage damage){
        this.damage = damage;
    }

    public void EndDamage(){
        damage = null;
    }

    void OnTriggerEnter(Collider other){
        if(damage == null){
            return;
        }
        TakeDamageZone tempTakeDamageZone = other.GetComponent<TakeDamageZone>();
        if(tempTakeDamageZone == null){
            return;
        }

        tempTakeDamageZone.TakeDamage(damage);
    }




    // Update is called once per frame
    void Update()
    {

    }
}
