using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Creature : MonoBehaviour
{



    [Header("Stats")]
    int _maxHealth = 99;
    public int currentHealth = 9;
    public float rotationSpeed = 100f;

    [Header("Gravity and Ground")]
    public float gravity = -9.81f;
    public float groundedGravity = -2;
    float gravityTracker = 0;
    public Transform groundReferenceTransform;
    public LayerMask groundLayerMask;

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            if (value > 99)
            {
                _maxHealth = 99;
            }
            else
            {
                _maxHealth = value;
            }
        }
    }
    public float _baseSpeed = 10;



    [Header("Prefabs")]
    public GameObject smokePrefab;

    CharacterController characterController;


    void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        SimulateGravity();
    }

    void SimulateGravity(){

        if(IsOnGround()){
            gravityTracker = groundedGravity;
        }

        gravityTracker += gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, gravityTracker, 0) * Time.deltaTime);
    }

    bool IsOnGround(){
        return Physics.OverlapSphere(groundReferenceTransform.position, .25f, groundLayerMask).Length > 0;
    }

    public void Move(Vector3 unitMovement){
        if(unitMovement.sqrMagnitude == 0){
            return;
        }

        unitMovement = unitMovement.normalized;
        // transform.localPosition += unitMovement * _baseSpeed * Time.deltaTime;

        //transform.rotation = Quaternion.LookRotation(unitMovement);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(unitMovement), rotationSpeed * Time.deltaTime);

        characterController.Move(unitMovement * _baseSpeed * Time.deltaTime);
    }

    bool throwingSmoke = false;
    public void ThrowSmoke(){

        if(throwingSmoke){
            return;
        }

        throwingSmoke = true;

        int numToThrow = 3;
        float smokePauseTime = 0.1f;
        float smokeRange = 2f;
        StartCoroutine(ThrowSmokeRoutine());

        IEnumerator ThrowSmokeRoutine(){
            for(int i = 0; i<numToThrow; i++){
                GameObject newSmoke = Instantiate(smokePrefab,
                transform.position + new Vector3(
                    Random.Range(-smokeRange,smokeRange),
                    0,
                    Random.Range(-smokeRange,smokeRange)
                ), Quaternion.identity);


                yield return new WaitForSeconds(smokePauseTime);
            }
            yield return null;
            throwingSmoke = false;
        }
    }




}
