using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerMovement : MonoBehaviour
{
    public Rigidbody rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 1.5f) {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        
        
        }
    }
}
