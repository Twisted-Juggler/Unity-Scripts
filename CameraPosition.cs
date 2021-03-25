using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    [SerializeField] Transform newPos;
    void Update()
    {
        transform.position = newPos.position;
    }
}
