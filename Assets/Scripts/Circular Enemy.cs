using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CircularEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float radius = 3f;
    private float angle = 0f;
    private Vector3 centerPoint;

    void Start()
    {
        centerPoint = transform.position;
    }

    void Update()
    {
        angle += speed * Time.deltaTime;
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0);
    }
}
