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

    //[SerializeField] private List<Transform> buildings;
    private GameObject target;
    private Vector3 targetVec;

    [SerializeField] private BuildingList buildingList;
    [SerializeField] private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        missile.GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(!playerController.gameIsRunning)
        {
            missile.velocity = Vector2.zero;
        }
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
        }
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private Vector2 GenerateRandomTarget()
    {
        int i = Random.Range(0, buildingList.buildings.Count);
        target = buildingList.buildings[i];
        return target.transform.position;
    }

    public void SetTrajectory()
    {
        targetVec = GenerateRandomTarget();
        speed = Random.Range(minSpeed, maxSpeed);

        Vector3 direction = targetVec - transform.position;
        
        missile.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(0, 0, rot -90);

        Destroy(this.gameObject, this.maxLifetime);
    }

}
