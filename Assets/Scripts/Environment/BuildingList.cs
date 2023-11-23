/*****************************************************************************
// File Name : BuildingList.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description :  This is a file that stores the location of the buildings in the game,
                        as well as what occurs after the all buildings have been destroyed.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    [Tooltip("Building objects that missiles are headed towards")]
    public List<GameObject> buildings;

    //whether all building objects have been destroyed
    private bool allDestroyed;

    [Tooltip("Reference to the gameManager")]
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        allDestroyed = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// This is called on whenever a missile hits a building. This method removes 
    /// the building from the list.
    /// </summary>
    /// <param name="building">gameobject that building is refering to</param>
    public void UpdateList(GameObject building)
    {
        buildings.Remove(building);
    }

    /// <summary>
    /// This method checks to see if each of the building game objects in the list
    /// are null. 
    /// </summary>
    /// <param name="buildings">list of game objects</param>
    /// <returns>true if list is null. false otherwise </returns>
    private bool AllDestroyed(List<GameObject> buildings)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        //sets allDestroyed to the AllDestroyed() method. game is over if all buildings are destoyed
        allDestroyed = AllDestroyed(buildings);
        if (allDestroyed)
        {
            gameManager.GameOver();
        }
    }
}
