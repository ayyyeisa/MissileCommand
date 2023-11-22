using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingList list;
    [SerializeField] private GameObject building;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Missile")
        {
            Destroy(building);
            list.UpdateList(building);
        }
    }

}
