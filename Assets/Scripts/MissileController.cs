using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D missile;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        missile.GetComponent<Rigidbody2D>();
    }

    public void SetTrajectory(Vector3 buildingPos)
    {
        speed = Random.Range(minSpeed, maxSpeed);
        missile.transform.position = Vector3.MoveTowards(transform.position, buildingPos, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if(collision.transform.tag == "Constants")
        {
            Destroy(gameObject);
        }
    }
}
