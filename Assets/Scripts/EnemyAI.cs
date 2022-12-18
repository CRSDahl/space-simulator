using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    Transform Player;
    float dist;
    public float maxDist;
    public Transform head;
    public GameObject _projectile;
    public float projectileSpeed;
    public float fireRate, nextFire;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Player.position, transform.position);
        if(dist <= maxDist)
        {
            head.LookAt(Player);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                shoot();
            }
            
        }
    }

    void shoot()
    {
        GameObject clone = Instantiate(_projectile, head.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        Destroy(clone, 10);
    }
}
