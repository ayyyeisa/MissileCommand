using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    public List<GameObject> buildings;
    private bool allDestroyed;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        allDestroyed = false;
    }

    public void UpdateList(GameObject building)
    {
        buildings.Remove(building);
    }

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
        allDestroyed = AllDestroyed(buildings);
        if (allDestroyed)
        {
            gameManager.GameOver();
        }
    }
}
