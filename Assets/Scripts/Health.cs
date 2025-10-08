using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public UnityEvent onTakeDamageEvent;
    public UnityEvent onDeathEvent;

    HashSet<Damage> damageHistory;
    const float DAMAGE_FORGIVENESS_TIME = .5f;

    void Awake(){
        damageHistory = new HashSet<Damage>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(Damage damage){
        if(damageHistory.Contains(damage)){
            return;
        }

        damageHistory.Add(damage);
        StartCoroutine(RemoveFromDamageHistoryRoutine(damage));

        currentHealth -= damage.Amount;
        onTakeDamageEvent.Invoke();
        if(currentHealth <= 0){
            //do something on death
            onDeathEvent.Invoke();
        }


        IEnumerator RemoveFromDamageHistoryRoutine(Damage damageToRemove){
            yield return new WaitForSeconds(DAMAGE_FORGIVENESS_TIME);
            damageHistory.Remove(damageToRemove);
        }
    }
}
