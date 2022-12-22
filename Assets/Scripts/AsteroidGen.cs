using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGen : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public int fieldRadius = 100;
    public int asteroidCount = 100;
    public GameObject center;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i= 0; i < asteroidCount; i++)
        {
            Vector3 pos = (Random.insideUnitSphere * fieldRadius) + center.transform.position;
            pos.y = Random.Range(-fieldRadius/2, fieldRadius/2);
            GameObject temp = Instantiate(asteroidPrefab, pos, Random.rotation);
            temp.transform.localScale = temp.transform.localScale * Random.Range(0.5f, 5f);
        }
    }


}
