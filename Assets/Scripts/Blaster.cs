using UnityEngine;
using System.Collections;

public class Blaster : MonoBehaviour
{
    [Header("Stats")]
    public float projectileSpeed = 10;
    public float cooldownPeriod = .25f;

    [Header("Prefabs")]
    public GameObject projectilePrefab;

    [Header("Helpers")]
    public Transform projectileSpawnPoint;

    [Header("Audio")]
    public float pitchRange = .1f;


    //trackers
    bool coolingDown = false;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Blast(){
        if(coolingDown){
            return;
        }

        coolingDown = true;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(1f-pitchRange, 1+pitchRange);
        audioSource.PlayOneShot(audioSource.clip);
        Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation).GetComponent<Projectile>();
        newProjectile.Shoot(transform.forward * projectileSpeed);

        StartCoroutine(CooldownRoutine());

        IEnumerator CooldownRoutine(){
            yield return new WaitForSeconds(cooldownPeriod);
            coolingDown = false;
        }




    }
}
