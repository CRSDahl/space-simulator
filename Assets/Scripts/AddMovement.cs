using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMovement : MonoBehaviour
{
    public float minSpin = 1f;
    public float maxSpin = 10f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;
    private float spinSpeed;


    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = Random.Range(minSpin, maxSpin);
        float thrustSpeed = Random.Range(minSpeed, maxSpeed);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrustSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}
