using UnityEngine;

public class EnemyType2Movement : MonoBehaviour
{
    public float speed = 2f;
    public float frequency = 2f;
    public float magnitude = 0.5f;
    private Vector3 axis;
    private Vector3 pos;
    
    
    void Start()
    {
        pos = transform.position;
        axis = transform.right;
    }

    void Update()
    {
        pos += Vector3.down * speed * Time.deltaTime;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}