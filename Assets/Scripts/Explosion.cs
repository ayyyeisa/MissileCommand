using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //[SerializeField] private Transform explosion;

    private float growthRate = 1.5f;
    private float scale = 1f; //currentsize
    private float goalScale = 2.5f;
    // private float maxLifetime = 5f;

    private float startTime;

    /*
    private void ApplyScaleRate()
    {
        transform.localScale += Vector3.one * scaleRate;
    }

    public void ScaleOverTime()
    {
        if (transform.localScale.x < minScale)
        {
            scaleRate = Mathf.Abs(scaleRate);
        }
        else if (transform.localScale.x > maxScale)
        {
            scaleRate = -Mathf.Abs(scaleRate);
        }
        ApplyScaleRate();

        Destroy(this.gameObject, this.maxLifetime);
    }
    */

    private void Start()
    {
        //startTime = Time.time;
    }

    public void Update()
    {
        transform.localScale = Vector3.one * scale;
        scale += growthRate * Time.deltaTime;
       // Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
        if(scale >= goalScale)
        {
            goalScale = 1f;
           // Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
            scale -= growthRate / Time.deltaTime;
            if(scale <= goalScale)
            {
                Debug.Log("scale :" + scale + "\n goalscale: " + goalScale);
                Destroy(this.gameObject);
            }
        }
    }
}
