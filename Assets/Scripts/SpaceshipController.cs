using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;

    public float fuelLevel = 100000f;

    public float score = 0.0f;
    float fuelLevelFull;

    public TextMeshProUGUI fuel_display;
    public TextMeshProUGUI speed_display;
    public TextMeshProUGUI timer_display;
    public TextMeshProUGUI game_over_text;
    public TextMeshProUGUI score_text;
    public Animator animator;
    public ParticleSystem left_thruster;
    public ParticleSystem right_thruster;
    public ParticleSystem left_rear_thruster;
    public ParticleSystem right_rear_thruster;
    public ParticleSystem explosion;
    public Camera minimap_camera;
    GameObject[] planets;

    public Button restart;
    public Button menu;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        fuelLevelFull = fuelLevel;
        timer = 30.0f;
        restart.onClick.AddListener(RestartGame);
        menu.onClick.AddListener(LoadMenu);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            m_Rigidbody.drag = 20;
            Debug.Log("Time's up!");
            minimap_camera.rect = new Rect(0,0,1,1);
            game_over_text.text = "Time's Up! Youre Score: " + score;
            game_over_text.enabled = true;
            restart.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            return;
        }
        m_Rigidbody.angularVelocity = Vector3.zero;
        timer_display.text = "Time left: " + +Mathf.Round((timer));
        fuel_display.text = "Fuel: " + Mathf.Round((fuelLevel / fuelLevelFull) * 100);
        speed_display.text = "Speed: " + Mathf.Round(m_Rigidbody.velocity.magnitude);
        score_text.text = "Score: " + score;
        


        if(Input.GetKey(KeyCode.UpArrow) )
        {
            if (fuelLevel > 0){
                //animator.SetInteger("State", 1);
                m_Rigidbody.AddRelativeForce(Vector3.forward * m_Thrust);
                fuelLevel -= 0.1f;
                left_rear_thruster.GetComponent<ParticleSystem>().enableEmission = true;
                right_rear_thruster.GetComponent<ParticleSystem>().enableEmission = true;
            } 

        } else {
            left_rear_thruster.GetComponent<ParticleSystem>().enableEmission = false;
            right_rear_thruster.GetComponent<ParticleSystem>().enableEmission = false;
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
            minimap_camera.rect = new Rect(0,0,1,1);
            game_over_text.enabled = true;
            StartCoroutine(EndGame());
        }

        if(fuelLevel <= 0)
        {
            Debug.Log("You've run out of fuel!");
            minimap_camera.rect = new Rect(0,0,1,1);
            game_over_text.enabled = true;
            StartCoroutine(EndGame());
        }

    }

    private void OnCollisionEnter(Collision other) {
        explosion.GetComponent<ParticleSystem>().enableEmission = true;
        m_Rigidbody.drag = 20;
        Debug.Log("You've crashed!");
        minimap_camera.rect = new Rect(0,0,1,1);
        game_over_text.text = "You've Crashed!";
        game_over_text.enabled = true;
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Title Screen");
    }

    private void RestartGame(){
        SceneManager.LoadScene("Level1");
    }

    private void LoadMenu(){
        SceneManager.LoadScene("Title Screen");
    }
}
