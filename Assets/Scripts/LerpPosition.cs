using UnityEngine;
using System.Collections;

public class LerpPosition : MonoBehaviour
{

    public Transform start;
    public Transform end;
    public float lerpTime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LerpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LerpRoutine(){
        float timer = 0;

        while(timer < lerpTime){
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start.position,end.position, timer/lerpTime);
            yield return null;
        }
        yield return null;
    }
}
