using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed = 100f;
    Rigidbody planets;
    const float G = 6674.08f;
    GameObject ship;

    void Start()
    {
        planets = GetComponent<Rigidbody>();
        ship = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        AddGravityForce(planets, ship.GetComponent<Rigidbody>() , G);
       
        var sun = GameObject.FindWithTag("Sun");
        this.transform.RotateAround(sun.transform.position, Vector3.up,
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
