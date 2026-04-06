using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : BulletMove
{
    public float rotateSpeed = 3f, followDuration = 1f;
    private Transform player;

    private WaitForSeconds physicsTimeStep;

    // Start is called before the first frame update
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        physicsTimeStep = new WaitForSeconds(Time.fixedDeltaTime);
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        StartCoroutine(StartFollow(followDuration));
    }

    IEnumerator StartFollow(float followDuration)
    {
        while(followDuration > 0f)
        {
            followDuration -= Time.fixedDeltaTime;

            if (player != null)
            {
                Vector3 dir = player.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.fixedDeltaTime);
            }

            myRb.velocity = transform.forward * speed;

            yield return physicsTimeStep;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
