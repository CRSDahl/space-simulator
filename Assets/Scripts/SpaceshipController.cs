using UnityEngine;
using TMPro;

public class SpaceshipController : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;

    public float fuelLevel = 10000f;

    float fuelLevelFull;

    public TextMeshProUGUI fuel_display;
    public TextMeshProUGUI speed_display;
    public TextMeshProUGUI timer_display;
    GameObject[] planets;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        planets = GameObject.FindGameObjectsWithTag("Planet");
        fuelLevelFull = fuelLevel;
    }

    // Update is called once per frame
    void Update()
    {
        m_Rigidbody.angularVelocity = Vector3.zero;
        timer_display.text = "Time left: " + +Mathf.Round((30 - Time.realtimeSinceStartup));
        fuel_display.text = "Fuel: " + Mathf.Round((fuelLevel / fuelLevelFull) * 100);
        speed_display.text = "Speed: " + Mathf.Round(m_Rigidbody.velocity.magnitude);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (fuelLevel > 0)
            {
                m_Rigidbody.AddRelativeForce((Vector3.left + Vector3.forward) * m_Thrust);
                fuelLevel -= 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (fuelLevel > 0)
            {
                m_Rigidbody.AddRelativeForce((Vector3.right + Vector3.forward) * m_Thrust);
                fuelLevel -= 0.1f;
            }
        }

        foreach (GameObject planet in planets)
        {
            if ((planet.transform.position - transform.position).magnitude < 5000)
            {
                Vector3 force = ((planet.transform.position - transform.position).normalized * 120000000) / Mathf.Pow((planet.transform.position - transform.position).magnitude, 2.0f);
                //if((m_Rigidbody.velocity + force).magnitude < m_Rigidbody.velocity.magnitude) {
                //    force *= 0.1f;
                //}
                m_Rigidbody.AddForce(force);
            }
        }
    }
}
