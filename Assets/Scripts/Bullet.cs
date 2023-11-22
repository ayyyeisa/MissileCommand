using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector2 target;
    private Camera mainCam;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bullet = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector2(mousePos.x, mousePos.y);

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        bullet.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void FixedUpdate()
    {
        BulletExplosion();
    }

    private void BulletExplosion()
    {
        if(bullet.position != target)
        {
            Vector2 explosion = Vector3.MoveTowards(bullet.position, target, speed * Time.deltaTime);
            bullet.MovePosition(explosion);
        }
        else if(bullet.position == target)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.transform.tag == "Missile")
        {
            Destroy(gameObject);
        }
    }

}
