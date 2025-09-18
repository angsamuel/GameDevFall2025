using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;

    public bool enableFreeLook = true;

    [Header("Mouse Look")]
    float xRotation = 0f;
    float yRotation = 0f;

    float maxPitch = 85f;
    float minPitch = -30f;

    public float smoothCameraSpeed = 10;

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = cameraTarget.position;
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, smoothCameraSpeed * Time.deltaTime);
    }

    public void AdjustRotation(float speedY, float speedX)
    {
        if(!enableFreeLook){
            return;
        }
        speedX *= Time.deltaTime;
        speedY *= Time.deltaTime;

        xRotation += speedX;
        yRotation += speedY;

        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation,0);

    }







}
