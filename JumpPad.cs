using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Find Tag")]
    [Space(5)]
    public GameObject[] jumpPad;

    [Header("Controls")]
    [Space(5)]
    public float explosionForce = 1000f;
    public float explosionMultiplier = 500;
    public Rigidbody rb;
    public Transform player;
    bool onTrigger = false;

    private void Update()
    {
        if (onTrigger == true)
        {
            rb.AddForce(Vector3.up * explosionForce * explosionMultiplier, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpPad")
        {
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "JumpPad")
        {
            onTrigger = false;
        }
    }
}
