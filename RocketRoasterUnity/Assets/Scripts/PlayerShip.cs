using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerShip : MonoBehaviour
{
    public static PlayerShip Instance;

    public Rigidbody2D rb;
    public AudioSource hitSound;
    public ParticleSystem thrustParticles;

    public float acceleration;
    public float maxXSpeed;
    public float maxYSpeed;
    public int maxArmor;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;

    public GameObject explosionPrefab;

    public float distanceBelowCameraForDeath;

    public float maxFuel;
    float currentFuel;
    public float fuelConsumptionRate;
    public Image fuelFill;
    public TextMeshProUGUI distance;
    public float tiltForce = 10f;

    public TextMeshProUGUI highscore;

    void Start()
    {
        highscore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore"));
        Input.gyro.enabled = true; // Enable the gyroscope
    }

    public void Awake()
    {
        currentArmor = maxArmor;
        currentFuel = maxFuel;
        Instance = this;
    }

    bool shouldThrust;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch (index 0)

            if (touch.phase == TouchPhase.Began)
            {
                shouldThrust = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                shouldThrust = false;
            }
        }
        else
        {
            SoundManager.Instance.rocket.volume = 0;
        }



        if (Input.GetKey(KeyCode.UpArrow))
        {
            Thrust();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SoundManager.Instance.rocket.volume = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * acceleration / 2);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * acceleration / 2);
        }

        if (transform.position.y < Camera.main.transform.position.y - distanceBelowCameraForDeath)
        {
            Explode();
        }

        fuelFill.fillAmount = currentFuel;
        distance.SetText("" + (int)transform.position.y);

        if (transform.position.y > PlayerPrefs.GetInt("HighScore"))
        {
            distance.color = new Color(1, .5f, 0, 1);
            PlayerPrefs.SetInt("HighScore", (int)transform.position.y);
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.x > maxXSpeed)
        {
            rb.velocity = new Vector3(maxXSpeed, rb.velocity.y, 0);
        }
        else if (rb.velocity.x < -maxXSpeed)
        {
            rb.velocity = new Vector3(-maxXSpeed, rb.velocity.y, 0);
        }

        if (rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, 0);
        }

        //// Get the gyroscope data
        //Vector3 gyroInput = -Input.gyro.rotationRateUnbiased;

        //// Apply horizontal force based on the gyroscope data
        //float horizontalForce = gyroInput.z * tiltForce;
        //rigidBody.AddForce(Vector3.right * horizontalForce, ForceMode.Force);

        // Get the acceleration data
        Vector3 deviceAcceleration = Input.acceleration;

        // Calculate tilt angle
        //float tiltAngle = Mathf.Atan2(deviceAcceleration.x, deviceAcceleration.y) * Mathf.Rad2Deg;

        float tiltAngle = Input.acceleration.x;

        // Apply force based on tilt angle
        float horizontalForce = tiltAngle * tiltForce;
        //rigidBody.AddForce(Vector3.right * horizontalForce, ForceMode.Force);
        //rigidBody.velocity = new Vector3(horizontalForce, rigidBody.velocity.y, rigidBody.velocity.z);

        if (shouldThrust)
        {
            Thrust();
        }
    }

    public void Thrust()
    {
        rb.AddForce(transform.up * acceleration); //Add force in the direction we're facing
        currentSpeed = maxXSpeed; //Set our speed to our max speed
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        thrustParticles.Emit(1);

        currentFuel -= fuelConsumptionRate;

        SoundManager.Instance.rocket.volume = 1;

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

        SoundManager.Instance.splode.Play();
        SoundManager.Instance.rocket.volume = 0;

        Destroy(gameObject);
    }

    public void AddFuel(float fuelToAdd)
    {
        currentFuel += fuelToAdd;

        if (currentFuel > 1)
        {
            currentFuel = 1;
        }
    }
}
