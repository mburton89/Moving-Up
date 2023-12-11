using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOffScreen : MonoBehaviour
{

    private Rigidbody2D rb;
    public float bounceForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x <= 0 || viewPos.x >= 1)
        {
            // Player has reached left or right side of the screen
            Bounce();
        }
    }

    void Bounce()
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = -newVelocity.x * bounceForce;
        rb.velocity = newVelocity;
    }

}
