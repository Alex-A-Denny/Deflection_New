using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerMovement : MonoBehaviour
{
    public Rigidbody rig;
    public Transform trans;

    [Header("Hover")]
    public float hoverHeight = 1.5f;
    public float hoverForce = 10f;
    public float gravForce = 9.81f;
    public float hoverForceLimit = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
    }

    private void Hover()
    {
        RaycastHit hit;
        Ray ray = new Ray(trans.position, -trans.up);

        if (Physics.Raycast(ray, out hit))
        {
            float proportionalHeight = hoverHeight - hit.distance;

            // Limit hover force when close to the hover height
            float limitedHoverForce = Mathf.Clamp(proportionalHeight * hoverForce, -hoverForceLimit, hoverForceLimit);

            Vector3 appliedHoverForce = Vector3.up * limitedHoverForce;
            rig.AddForce(appliedHoverForce, ForceMode.Force);
        }
        else
        {
            rig.AddForce(Vector3.down * gravForce, ForceMode.Force);
        }
    }
}
