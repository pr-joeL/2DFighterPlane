using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    // Adjust this speed in the Inspector to control movement pace
    public float movementSpeed = 5f;

    private float minX, maxX, minY, maxY;
    private float playerHalfWidth, playerHalfHeight;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        
        // Calculate player extents (half-width/height) from the SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            playerHalfWidth = spriteRenderer.bounds.extents.x;
            playerHalfHeight = spriteRenderer.bounds.extents.y;
        }
        else
        {
            Debug.LogError("PlayerBoundaries script requires a SpriteRenderer component.");
        }

        CalculateScreenBounds();
    }

    void Update()
    {
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * movementSpeed * Time.deltaTime;
        transform.position += movement;

        // Apply horizontal wrapping
        if (transform.position.x < minX - playerHalfWidth)
        {
            transform.position = new Vector3(maxX + playerHalfWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxX + playerHalfWidth)
        {
            transform.position = new Vector3(minX - playerHalfWidth, transform.position.y, transform.position.z);
        }

        // Apply vertical clamping to the bottom half of the screen
        float clampedY = Mathf.Clamp(transform.position.y, minY + playerHalfHeight, maxY - playerHalfHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    // Helper to calculate world space bounds from the camera
    void CalculateScreenBounds()
    {
        // Get the edges of the screen in world coordinates
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        // Horizontal boundaries are the full screen width for wrapping
        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;

        // Vertical boundaries are the bottom half of the screen
        minY = screenBottomLeft.y; // Bottom edge of screen
        maxY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, mainCamera.nearClipPlane)).y; // Middle of screen
    }

    // Recalculate bounds if the screen size or camera changes (optional, for dynamic windows)
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            CalculateScreenBounds();
        }
    }
}

