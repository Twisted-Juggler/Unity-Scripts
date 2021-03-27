using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPad : MonoBehaviour
{
    [Header("Second Portal")]
    [Space(5)]


    public Transform portalPosition;
    public Transform portal2Position;
    public GameObject[] portalPad;

    [Header("Player")]
    [Space(5)]

    public Transform player;
    public Rigidbody rb;


    public float cooldown = 2f;
    float timer;
    float timeStamp;
    bool isOnPortalOne = false;
    bool isOnPortalTwo = false;

    private void OnTriggerEnter(Collider other)
    {
        timeStamp = Time.time;

        if (other.tag == "Portal")
        {
            Invoke("FirstPortal", cooldown);
        }

        else if (other.tag == "Portal2")
        {
            Invoke("SecondPortal", cooldown);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Portal")
        {
            CancelInvoke("FirstPortal");
        }

        else if (other.tag == "Portal2")
        {
            CancelInvoke("SecondPortal");
        }
    }

    void FirstPortal()
    {
        player.position = portal2Position.position;
    }

    void SecondPortal()
    {
        player.position = portalPosition.position;
    }
}
