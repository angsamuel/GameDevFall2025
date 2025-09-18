using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class PlayerInputHandler : MonoBehaviour
{

    public Creature playerCreature;
    public CameraFollow cameraFollow;
    public float cameraRotateSpeed = 500;

    public List<Creature> sideCreatures;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            tempMovement.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            tempMovement.z -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            tempMovement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            tempMovement.x += 1;
        }


        //space and swimming
        //tempMovement = cameraFollow.transform.rotation * tempMovement;

        tempMovement = Quaternion.Euler(0, cameraFollow.transform.eulerAngles.y, 0) * tempMovement;


        //use this for first person body rotation
        //playerCreature.transform.rotation = Quaternion.Euler(0, cameraFollow.transform.eulerAngles.y, 0);


        playerCreature.Move(tempMovement);


        for (int i = 0; i < sideCreatures.Count; i++)
        {
            sideCreatures[i].Move(tempMovement);
        }



        //adjusting camera rotation
        cameraFollow.AdjustRotation(Input.GetAxis("Mouse X") * cameraRotateSpeed, Input.GetAxis("Mouse Y") * cameraRotateSpeed);




        if (Input.GetKeyDown(KeyCode.Space)){
            playerCreature.ThrowSmoke();
        }

        if(Input.GetKeyDown(KeyCode.R)){
            //reload the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }




    }
}
