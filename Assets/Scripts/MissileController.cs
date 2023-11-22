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
    [SerializeField] private float maxLifetime;

    [SerializeField] private List<Transform> buildings;
    private Transform target;
    private Vector3 targetVec;

    // Start is called before the first frame update
    void Start()
    {
        missile.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Debug.Log("bullet to missile collision");
            Destroy(gameObject);
        }
        if(collision.transform.tag == "Constraints")
        {
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Building")
        {
            Destroy(gameObject);
            buildings.Remove(target);
        }
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private Vector2 GenerateRandomTarget()
    {
        int i = Random.Range(0, buildings.Count);
        target = buildings[i];
        return target.transform.position;
    }

    private void SetTrajectory()
    {
        targetVec = GenerateRandomTarget();
        speed = Random.Range(minSpeed, maxSpeed);
        //missile.velocity = new Vector2(target.x, target.y).normalized * speed;
        missile.transform.position = Vector2.MoveTowards(missile.transform.position, targetVec, speed * Time.deltaTime);
        float rot = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        Destroy(this.gameObject, this.maxLifetime);
    }

    public void Update()
    {
        SetTrajectory();
    }
}
