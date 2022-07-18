using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeAttack : MonoBehaviour
{
    
    public GameObject bulletPrefabs;
    public Transform firePoint;
    public float bulletSpeed;
    public float waitTime = 3f;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
    }

    public void Attacking()
    {
        Vector3 direction = firePoint.position - transform.position;

        if(robot != null)
        transform.LookAt(robot.transform.position);

        if (waitTime <= 0 && robot != null)
        {
            var bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce(direction * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            waitTime = 3f;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        
        
        
    }
}
