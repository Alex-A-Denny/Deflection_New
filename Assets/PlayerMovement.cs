using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [Header("Movement")]
    public float moveSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public float groundDrag;

    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;

    public Rigidbody rig;

    // Start is called before the first frame update
    void Start() {
        rig.GetComponent<Rigidbody>();
        rig.freezeRotation = true;
    }


    // Update is called once per frame
    private void Update() {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        movePlayer();
        speedControl();

        if (grounded)
            rig.drag = groundDrag;
        else
            rig.drag = 0;
    }


    private void movePlayer() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //caculate movement
        moveDir = (orientation.forward * verticalInput + orientation.right * horizontalInput) * Time.deltaTime;

        rig.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

    }

    private void speedControl() {

        Vector3 flatVel = new Vector3(rig.velocity.x, 0f, rig.velocity.z);

        if (flatVel.magnitude > moveSpeed) {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rig.velocity = new Vector3(limitedVel.x, rig.velocity.y, limitedVel.z);

        }

    }
}
