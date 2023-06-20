using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [Header("Movement")]
    public float moveSpeed;

    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public float groundDrag;
    public Transform groundCheckTrans;

    public Transform orientation;
    public PlayerCam playerCam;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;

    public Rigidbody rig;

    public Vector3 gravForce;
    // Start is called before the first frame update
    void Start() {
        Physics.gravity = gravForce;
        rig.GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        readyToJump = true;
        
    }


    // Update is called once per frame
    private void Update() {
       
        grounded = Physics.Raycast(groundCheckTrans.position, Vector3.down, 0.1f, whatIsGround);

        movePlayer();
        speedControl();

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded) {
            
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        
        }


        if (grounded && rig.velocity == Vector3.zero)
        {
            rig.drag = groundDrag;
            readyToJump = true;
        }
        else {
            
            rig.drag = 0f;
        
        }
    }


    private void movePlayer() {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //caculate movement
        moveDir = (orientation.forward * verticalInput + orientation.right * horizontalInput) * Time.deltaTime;

        //boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rig.drag = 0;
            rig.AddForce( playerCam.transform.forward * moveSpeed * 150f, ForceMode.Acceleration);
            rig.AddForce(playerCam.transform.forward * moveSpeed * 400f, ForceMode.Acceleration);

        }
        else {

            if (grounded)
            {
                rig.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else
            {
                rig.AddForce(moveDir.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
            }

        }


        
         
    }

    private void speedControl() {

        Vector3 flatVel = new Vector3(rig.velocity.x, 0f, rig.velocity.z);

        if (flatVel.magnitude > moveSpeed) {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rig.velocity = new Vector3(limitedVel.x, rig.velocity.y, limitedVel.z);

        }

    }

    private void Jump() {

        rig.velocity = new Vector3(rig.velocity.x, 0f, rig.velocity.z);

        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);    
    
    }

    private void resetJump() { 
        readyToJump = true;
    }
}
