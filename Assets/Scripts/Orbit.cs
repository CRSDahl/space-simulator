using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sun = GameObject.Find("Sun");
        this.transform.RotateAround(sun.transform.position, new Vector3(0.0f, 1.0f, 0.0f),
            speed * Time.deltaTime);
    }
}
