using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public bool gameIsRunning;
    [SerializeField] private GameObject startScreen;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] private bool canFire;
    private float timer;
    [SerializeField] private float timeBetweenFiring = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        EnableInputs();
       if(gameIsRunning)
        {
            RotateToCursor();

            if(!canFire)
            {
                timer += Time.deltaTime;
                if(timer > timeBetweenFiring)
                {
                    canFire = true;
                    timer = 0;
                }
            }
            if(Input.GetMouseButton(0) && canFire)
            {
                canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
        }

    }
    
    private void RotateToCursor()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Clamp(((Mathf.Atan2(rotation.y, rotation.x) - 1.5708f) * Mathf.Rad2Deg), -90f, 90f);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    #region KeyInputs
    private void EnableInputs()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameIsRunning)
            {
                gameIsRunning = true;
                startScreen.SetActive(false);
            }
        }
    }

    #endregion
}

