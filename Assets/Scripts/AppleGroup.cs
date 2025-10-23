using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class AppleGroup : MonoBehaviour
{
    public List<GameObject> apples;

    void Awake(){
        foreach(Transform child in transform){
            apples.Add(child.gameObject);
        }
    }

    public GameObject FindNearestApple(Vector3 searchPosition){
        if(apples.Count < 1){
            return null;
        }
        GameObject nearestApple = apples[0];
        float nearestAppleDistance = Vector3.Distance(apples[0].transform.position, searchPosition);

        for(int i = 1; i<apples.Count; i++){
            float tempDist = Vector3.Distance(searchPosition,apples[i].transform.position);
            if(tempDist < nearestAppleDistance){
                nearestApple = apples[i];
                nearestAppleDistance = tempDist;
            }
        }

        apples.Remove(nearestApple);
        return nearestApple;
    }
}
