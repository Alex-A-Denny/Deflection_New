using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getSpeed : MonoBehaviour
{
    public Rigidbody rig;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = rig.velocity.magnitude.ToString();
    }
}
