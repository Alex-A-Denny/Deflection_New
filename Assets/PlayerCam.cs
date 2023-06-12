using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //Camera vars
    public float xSens;
    public float ySens;

    public Transform orientation;

    float xRot;
    float yRot;

    public void Start() {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    public void Update() {

        //mouse inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRot, 0f);


    }
}
