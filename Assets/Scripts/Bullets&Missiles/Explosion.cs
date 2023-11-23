/*****************************************************************************
// File Name : Explosion.cs
// Author : Isa Luluquisin
// Creation Date : November 22, 2023
//
// Brief Description :  This is a file that handles the behavior of explosions.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //rate at which transformation occurs
    private float growthRate = 1.5f;
    //current scale (starts at 1)
    private float scale = 1f; 
    //size at which objects stop growing
    private float goalScale = 2.5f;


    public void Update()
    {
        //calls on transformobject every frame
        TransformObject();
    }

    /// <summary>
    /// Grows object over time according to growth rate. Once it has enlargened,
    /// the object shrinks back to its original size over time, before being destroyed.
    /// </summary>
    private void TransformObject()
    {
        transform.localScale = Vector3.one * scale;
        scale += growthRate * Time.deltaTime;
        // Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
        if (scale >= goalScale)
        {
            goalScale = 1f;
            // Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
            scale -= growthRate / Time.deltaTime;
            if (scale <= goalScale)
            {
                // Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
                Destroy(this.gameObject);
            }
        }
    }
}
