using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float zoomSpeed;
    public float orthographicSizeMin;
    public float orthographicSizeMax;
    public float fovMin;
    public float fovMax;
    private Camera myCamera;

    public GameObject lookAtMe;
    public float rotateSpeed = 100.0f;
    private float xRotateMove, yRotateMove;


    private float xRotate = 25f, yRotate;
    private float xSpeed = 5f;

    bool isAlt;
    Vector2 clickPoint;
    float dragSpeed = 30.0f;

    // Use this for initialization
    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // 클릭한 경우
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed; ;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed; ;

            yRotate = yRotate + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 
            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

            Quaternion quat = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
            transform.rotation
                = Quaternion.Slerp(transform.rotation, quat, xSpeed);

        }
        if (myCamera.orthographic)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                myCamera.orthographicSize += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                myCamera.orthographicSize -= zoomSpeed;
            }
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                myCamera.fieldOfView += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                myCamera.fieldOfView -= zoomSpeed;
            }
            myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView, fovMin, fovMax);
        }
    }
}