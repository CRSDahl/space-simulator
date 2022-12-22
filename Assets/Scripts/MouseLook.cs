using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public Transform player;
    public float distanceFromPlayer = 2f;
    public float height = 2;
    void LateUpdate()
    {
        
        transform.position = player.position - player.forward * distanceFromPlayer;
        transform.LookAt(player.position);
        transform.position = new Vector3(transform.position.x, height+transform.position.y, transform.position.z);
    }
}

