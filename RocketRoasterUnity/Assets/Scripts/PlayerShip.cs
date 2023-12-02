using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public Rigidbody rigidBody;
    public AudioSource hitSound;
    public ParticleSystem thrustParticles;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;

    public GameObject explosionPrefab;

    public float distanceBelowCameraForDeath;

    public float maxFuel;
    float currentFuel;
    public float fuelConsumptionRate;
    public Image fuelFill;

    public void Awake()
    {
        currentArmor = maxArmor;
        currentFuel = maxFuel;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Thrust();
        }

        if (transform.position.y < Camera.main.transform.position.y - distanceBelowCameraForDeath)
        {
            Explode();
        }
    }

    void FixedUpdate()
    {
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
    }

    public void Thrust()
    {
        rigidBody.AddForce(transform.up * acceleration); //Add force in the direction we're facing
        currentSpeed = maxSpeed; //Set our speed to our max speed
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        thrustParticles.Emit(1);

        currentFuel -= fuelConsumptionRate;
        fuelFill.fillAmount = currentFuel;

        if (currentFuel <= 0)
        {
            Explode();
        }
    }

    public void TakeDamage(int damageToTake)
    {
        currentArmor -= damageToTake;
        hitSound.Play();
        if (currentArmor <= 0)
        {
            Explode();
        }

        if (GetComponent<PlayerShip>())
        {
            //HUD.Instance.UpdateHealthBar(currentArmor, maxArmor);
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);

        //ScreenShaker.Instance.ShakeScreen();
        ScreenShakeManager.Instance.ShakeScreen();

        GameManager.Instance.HandlePlayerDestroyed();

        Destroy(gameObject);
    }
}
