using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public float speed;
    public Transform moveSpot;
    public float startWaitTime;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(-8, 8), 0.55f, Random.Range(-10, 10));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot.transform.position) <= 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(-8, 8), 0.55f, Random.Range(-10, 10));
                waitTime = startWaitTime;
            }else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
