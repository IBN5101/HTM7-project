using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPumpkin : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private float timer;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        timer = 1f;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Force();
            GetComponent<BouncingPumpkin>().enabled = false;
        }
    }

    private void Force()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float forceMultiplier = 1000;
        rigidbody2d.AddForce(randomDirection * forceMultiplier);

        float torque = 50;
        rigidbody2d.AddTorque(torque);
    }
}
