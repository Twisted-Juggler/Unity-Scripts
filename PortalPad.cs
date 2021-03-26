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


    private void Update()
    {

        StartTimer(cooldown);
    }

    private void OnTriggerEnter(Collider other)
    {
        timeStamp = Time.time;

        if (other.tag == "Portal")
        {
            isOnPortalOne = true;
        }

        else if (other.tag == "Portal2")
        {
            isOnPortalTwo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Portal")
        {
            isOnPortalOne = false;
        }

        else if (other.tag == "Portal2")
        {
            isOnPortalTwo = false;
        }
    }

    

    void StartTimer(float cooldownTime)
    {
        float timer = 0f;

        if (timer < cooldownTime)
            timer += Time.deltaTime;

        if (timer == cooldownTime && isOnPortalOne)
        {
            player.position = portal2Position.position;
        }
        
        else if (timer < cooldownTime && isOnPortalTwo)
        {
            player.position = portalPosition.position;
        }
    }


}
