using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed = 100f;
    public GameObject ship;
    Rigidbody planets;
    const float G = 6674.08f;

    void Start()
    {
        planets = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        AddGravityForce(planets, ship.GetComponent<Rigidbody>() , G);
       
        var sun = GameObject.FindWithTag("Sun");
        this.transform.RotateAround(sun.transform.position, new Vector3(0.0f, 1.0f, 0.0f),
            speed * Time.deltaTime);
    }


    public static void AddGravityForce(Rigidbody planet, Rigidbody rb, float gravityForce)
    {
        float massProduct = rb.mass * planet.mass * gravityForce;
        Vector3 difference = planet.position - rb.position;
        float distance = difference.magnitude;

        float magnitude = (massProduct / (distance * distance));

        Vector3 direction = difference.normalized;
        Vector3 force = direction * magnitude;
        
        rb.AddForce(force);
    }
}
