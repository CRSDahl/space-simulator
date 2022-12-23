using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed = 100f;
    Rigidbody planets;
    const float G = 6674.08f;
    GameObject ship;
    private bool points;
    public Texture2D[] textures;
    

    void Start()
    {
        var texture = textures[Random.Range(0, textures.Length-1)];
        GetComponent<Renderer>().material.mainTexture = texture as Texture2D;
        planets = GetComponent<Rigidbody>();
        ship = GameObject.FindWithTag("Player");
        points = false;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
    
        AddGravityForce(planets, ship.GetComponent<Rigidbody>() , G);
       
        var sun = GameObject.FindWithTag("Sun");
        this.transform.RotateAround(sun.transform.position, Vector3.up,
            speed * Time.deltaTime);

        if(Vector3.Distance(ship.transform.position, transform.position) <= 1000){
            if(!points){
                ship.GetComponent<SpaceshipController>().score += 100f;
                points = true;
                Debug.Log("Added Points!");
            }
        }
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
