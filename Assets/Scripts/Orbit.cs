using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float speed = 100f;

    // Update is called once per frame
    void Update()
    {
        var sun = GameObject.FindWithTag("Sun");
        this.transform.RotateAround(sun.transform.position, new Vector3(0.0f, 1.0f, 0.0f),
            speed * Time.deltaTime);
    }
}
