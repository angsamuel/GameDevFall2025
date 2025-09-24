using UnityEngine;

public class TakeDamageZone : MonoBehaviour
{
    public Health health;
    public void TakeDamage(Damage damage){
        health.TakeDamage(damage);
    }
}
