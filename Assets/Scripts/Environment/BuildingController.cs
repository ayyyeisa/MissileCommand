/*****************************************************************************
// File Name : BuildingController.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description :  This is a file handles the behavior of the buildings.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [Tooltip("Object with BuildingList attached")]
    [SerializeField] private BuildingList list;
    [Tooltip("Object that this script is attached to")]
    [SerializeField] private GameObject building;

    /// <summary>
    /// Handles collisions between this building and other game objects.
    /// it should only react to missiles. When a missile hits a building, the
    /// building object is destroyed and the game notes that it's been destroyed.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Missile")
        {
            Destroy(building);
            list.UpdateList(building);
        }
    }

}
