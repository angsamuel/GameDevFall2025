using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimationStateChanger : MonoBehaviour
{

    public string currentState = "Idle";
    Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState, float newSpeed = 1, float transitionTime = 0.05f){
        animator.speed = newSpeed;
        if(newState == currentState){
            return;
        }
        animator.CrossFadeInFixedTime(newState,transitionTime);
        currentState = newState;
    }
}
