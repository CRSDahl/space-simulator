using UnityEngine;
using TMPro;

public class SpaceshipController : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;

    public float fuelLevel = 100000f;

    float fuelLevelFull;

    public TextMeshProUGUI fuel_display;
    public TextMeshProUGUI speed_display;
    public TextMeshProUGUI timer_display;
    public Animator animator;
    public ParticleSystem left_thruster;
    public ParticleSystem right_thruster;
    public ParticleSystem left_rear_thruster;
    public ParticleSystem right_rear_thruster;
    public ParticleSystem explosion;
    GameObject[] planets;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        fuelLevelFull = fuelLevel;
        fuel_display =  GameObject.Find("Fuel").GetComponent<TextMeshProUGUI>();
        speed_display =  GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();
        timer_display =  GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody.angularVelocity = Vector3.zero;
        timer_display.text = "Time left: " + +Mathf.Round((30 - Time.realtimeSinceStartup));
        fuel_display.text = "Fuel: " + Mathf.Round((fuelLevel / fuelLevelFull) * 100);
        speed_display.text = "Speed: " + Mathf.Round(m_Rigidbody.velocity.magnitude);

        if(Input.GetKey(KeyCode.UpArrow) )
        {
            if (fuelLevel > 0){
                //animator.SetInteger("State", 1);
                m_Rigidbody.AddRelativeForce(Vector3.forward * m_Thrust);
                fuelLevel -= 0.1f;
                //left_rear_thruster.GetComponent<ParticleSystem>().enableEmission = true;
                //right_rear_thruster.GetComponent<ParticleSystem>().enableEmission = true;
            } 

        } else {
            //left_rear_thruster.GetComponent<ParticleSystem>().enableEmission = false;
            //right_rear_thruster.GetComponent<ParticleSystem>().enableEmission = false;
        }
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (fuelLevel > 0)
            {
                right_thruster.GetComponent<ParticleSystem>().enableEmission = true;
                m_Rigidbody.AddRelativeForce(Vector3.left * m_Thrust);
                fuelLevel -= 0.1f;
            }
        } else {
            right_thruster.GetComponent<ParticleSystem>().enableEmission = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //animator.SetInteger("State", 2);
            if (fuelLevel > 0)
            {
                m_Rigidbody.AddRelativeForce(Vector3.right * m_Thrust);
                left_thruster.GetComponent<ParticleSystem>().enableEmission = true;
                fuelLevel -= 0.1f;
            }
        }
        else {
            left_thruster.GetComponent<ParticleSystem>().enableEmission = false;
        }


        //if out of bounds, this could trigger a lose that way if the player drifts too far they can restart. Values can be adjusted
        if( transform.position.z  > 20000 || transform.position.z < -20000 || transform.position.x > 20000 || transform.position.x < -20000 || transform.position.y > 20 || transform.position.y < -20)
        {
            fuelLevel = 0;
            Debug.Log("You've strayed too far!");
        }

    }

    private void OnCollisionEnter(Collision other) {
        explosion.GetComponent<ParticleSystem>().enableEmission = true;
        m_Rigidbody.drag = 20;
    }
}
