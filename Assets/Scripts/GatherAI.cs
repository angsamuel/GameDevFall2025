using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class GatherAI : MonoBehaviour
{
    Creature creature;



    float stateTime = 0;
    public float patrolDist = 10;
    Vector3 homePosition;


    [Header("Scene Objects")]
    public AppleGroup appleGroup;
    public GameObject depot;

    [Header("Navigation Testing")]
    public Transform goalPos;
    NavMeshAgent navMeshAgent;

    public delegate IEnumerator TakeObjectState(GameObject value);


    void Start(){


        homePosition = transform.position;
        creature = transform.parent.GetComponent<Creature>();

        navMeshAgent = creature.GetComponent<NavMeshAgent>();
        ChangeState(PauseState());

        //creature.GetComponent<CharacterController>().enabled = false;

        // navMeshAgent.isStopped = false;
        // navMeshAgent.destination = goalPos.position;


    }

    IEnumerator FollowPathState(GameObject destination, TakeObjectState nextState){
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(destination.transform.position,path);
        int pathIndex = 0;
        while(pathIndex<path.corners.Length){
            Vector3 adjustedPos = path.corners[pathIndex] + new Vector3(0,.5f,0);;

            if(Vector3.Distance(creature.transform.position, adjustedPos) > 0.15f){
                creature.MoveToward(adjustedPos);
            }else{
                pathIndex++;
            }

            yield return null;
        }
        ChangeState(nextState(destination));

        Debug.Log("DONE");
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
            ChangeState(FollowPathState(nearestApple,GrabApple));
            yield break;
        }


        ChangeState(PauseState());
        yield break;

    }

    IEnumerator GrabApple(GameObject apple){

        creature.GrabObject(apple);
        ChangeState(FollowPathState(depot,(_)=>DropOffApple()));
        yield return null;

    }

    IEnumerator DropOffApple(){

        creature.DropObject();
        ChangeState(PauseState());
        yield break;
    }



    IEnumerator GatherState(){
        yield return null;
    }

}
