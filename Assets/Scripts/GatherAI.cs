using UnityEngine;
using System.Collections;

public class GatherAI : MonoBehaviour
{
    Creature creature;



    float stateTime = 0;
    public float patrolDist = 10;
    Vector3 homePosition;


    [Header("Scene Objects")]
    public AppleGroup appleGroup;
    public GameObject depot;

    void Start(){
        homePosition = transform.position;
        creature = transform.parent.GetComponent<Creature>();
        ChangeState(PauseState());
    }

    void ChangeState(IEnumerator newState){
        stateTime = 0;
        StartCoroutine(newState);
    }

    void Update(){
        stateTime += Time.deltaTime;
    }

    IEnumerator PauseState(){
        creature.Stop();
        yield return new WaitForSeconds(3f);
        GameObject nearestApple = appleGroup.FindNearestApple(transform.position);
        if(nearestApple != null){
            ChangeState(GrabApple(nearestApple));
            yield break;
        }


        ChangeState(PatrolState());
        yield break;
        Debug.Log("Still in state D:")
    }

    IEnumerator GrabApple(GameObject apple){

        Vector3 correctedApplePos = apple.transform.position;
        correctedApplePos.y = transform.position.y;
        //move towads apple
        while(Vector3.Distance(transform.position,apple.transform.position) > 1f){
            creature.MoveToward(apple.transform.position);
            yield return null;
        }

        //pickup apple
        creature.GrabObject(apple);

        ChangeState(DropOffApple());
        yield break;
        yield return null;
    }

    IEnumerator DropOffApple(){
        while(Vector3.Distance(creature.transform.position, depot.transform.position) > 1){
            creature.MoveToward(depot.transform.position);
            yield return null;

        }
        creature.DropObject();
        ChangeState(PatrolState());
        yield break;
    }

    IEnumerator PatrolState(){

        Vector3 patrolPos = Random.insideUnitSphere;

        patrolPos *= patrolDist;
        patrolPos = patrolPos + homePosition;

        patrolPos.y = transform.position.y;

        while((transform.position - patrolPos).sqrMagnitude > 1f){
            creature.MoveToward(patrolPos);
            yield return null;
        }
        ChangeState(PauseState());
        yield break;
        yield return null;
    }

    IEnumerator GatherState(){
        yield return null;
    }

}
