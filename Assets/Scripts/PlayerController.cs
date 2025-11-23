using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;

    private GameManager gameManager;
    private bool isShieldActive = false;
    public int currentHealth = 100;
    public int maxHealth = 100;
    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

    }
    public void ActivatePowerUp(PowerUpScript.PowerUpType type, float duration, float amount)
    {
        switch (type)
        {
            
            case PowerUpScript.PowerUpType.Shield:
                StartCoroutine(ShieldRoutine(duration));
                break;
            
        }
    }

    IEnumerator ShieldRoutine(float duration)
    {
        isShieldActive = true;
        Debug.Log("Shield activated!");
        yield return new WaitForSeconds(duration);
        isShieldActive = false;
        Debug.Log("Shield deactivated.");
    }

    // Example of how shield might be used in a damage function
    public void TakeDamage(int damageAmount)
    {
        if (!isShieldActive)
        {
            currentHealth -= damageAmount;
            Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);
            if (currentHealth <= 0)
            {
                Debug.Log("Player defeated!");
                // Handle player death
            }
        }
        else
        {
            Debug.Log("Shield absorbed damage!");
        }
    }
}
