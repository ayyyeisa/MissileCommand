/********************************************************************
// File Name : PlayerController.cs
// Author : Isa Luluquisin
// Creation Date : November 19, 2023
//
// Brief Description :  This is a file that handles the behavior of key inputs,
                        as well as the rotation of the player barrel and launching
                        of bullets.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //reference to camera
    private Camera mainCam;
    //where the mouse cursor clicked
    private Vector3 mousePos;

    [Tooltip("Whether game is running or not")]
    public bool gameIsRunning;
    [Tooltip("Screen that appears at the start of the game")]
    [SerializeField] private GameObject startScreen;

    [Header("")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletTransform;
    [SerializeField] private bool canFire;
    [SerializeField] private float timeBetweenFiring = 0.3f;
    //used to keep track of time to pace firing
    private float timer;

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

            //spaces out bullet firing
            if(!canFire)
            {
                timer += Time.deltaTime;
                if(timer > timeBetweenFiring)
                {
                    canFire = true;
                    timer = 0;
                }
            }
            //when there is a left click, a bullet is instantiated at the position and rotation of the barrel
            if(Input.GetMouseButton(0) && canFire)
            {
                canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
        }

    }
    
    /// <summary>
    /// Rotates the barrel object to follow the cursor. The rotation is clamped so that it can never 
    /// go off-screen.
    /// </summary>
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

