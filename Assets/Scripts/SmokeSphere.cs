using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmokeSphere : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public float growTime = 1; //how long will it take to reach max size
    public float maxSize = 5.5f;
    public float growPauseTime = 2f;


    bool hitMaxSize = false;

    float timeTracker = 0;
    void Start()
    {
        //transform.localScale = Vector3.zero;
        //Destroy(gameObject,lifeTime);
        GrowShrink();
    }

    // Update is called once per frame
    void Update()
    {
        // timeTracker += Time.deltaTime;
        // timeTracker = Mathf.Clamp(timeTracker,0,growTime);

        // transform.localScale = Vector3.one * maxSize * (timeTracker / growTime);

        // hitMaxSize = timeTracker == growTime;
    }

    void GrowShrink(){

        StartCoroutine(GrowShrinkRoutine());

        IEnumerator GrowShrinkRoutine()
        {
            float timer = 0;

            while(timer < growTime){
                timer += Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, growTime);
                transform.localScale = Vector3.one * maxSize * (timer/ growTime);

                yield return null;
            }

            yield return new WaitForSeconds(growPauseTime);

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, growTime);
                transform.localScale = Vector3.one * maxSize * (timer / growTime);

                yield return null;
            }

            yield return null;

            Destroy(this.gameObject);
        }
    }



}
