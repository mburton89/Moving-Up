using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gutsPrefab;
    public float health;

    public float colorChangeSpeed = 0.5f; // Speed at which the color changes
    private Renderer objectRenderer;
    private Color currentColor;
    public Color endColor;

    public Rigidbody2D rb;

    public GameObject comicTextPrefab;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        currentColor = objectRenderer.material.color;
        Destroy(gameObject, 30);
    }

    public void Init(int xDirection, float xSpeed)
    {
        rb.AddForce(new Vector3(xDirection, 0, 0) * xSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            collision.gameObject.GetComponent<PlayerShip>().Explode();
            ExplodeWithNoMessage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            collision.gameObject.GetComponent<PlayerShip>().Explode();
            ExplodeWithNoMessage();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        print("OnParticleCollision");

        currentColor = Color.Lerp(currentColor, endColor, colorChangeSpeed);
        objectRenderer.material.color = currentColor;

        health -= 1;
        if (health <= 0)
        {
            PlayerShip.Instance.AddFuel(0.5f);
            Explode();
        }
    }


    public void Explode()
    {
        Instantiate(gutsPrefab, transform.position, transform.rotation, null);
        SoundManager.Instance.guts.Play();
        GameObject comicText = Instantiate(comicTextPrefab);

        string message;

        int rand = Random.Range(0, 7);
        if (rand == 0)
        {
            message = "Ewww gross";
        }
        else if (rand == 1)
        {
            message = "Splat!";
        }
        else if (rand == 2)
        {
            message = "See ya!";
        }
        else
        {
            message = "Fuel Gained!";
        }

        comicText.GetComponent<ComicText>().Init(message, transform);
        Destroy(gameObject);
    }

    public void ExplodeWithNoMessage()
    {
        Instantiate(gutsPrefab, transform.position, transform.rotation, null);
        SoundManager.Instance.guts.Play();
        Destroy(gameObject);
    }

}
