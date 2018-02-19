using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveEffectTrigger : MonoBehaviour
{

    public Material DisolveMaterial;
    public float speed;
    public float max;

    private float currentY, startTime;

    private void Start()
    {
        max = GetComponent<Renderer>().bounds.size.y;
        print(max);
    }

    private void Update()
    {
        // If this is during the animation
        if (currentY < max)
        {
            DisolveMaterial.SetFloat("_DisolveY", currentY);
            currentY += Time.deltaTime * speed;
        }
                    
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerEffect();
            print(max);

            print(GetComponent<Renderer>().bounds.size);
            //print(GetComponent<Mesh>().bounds.size);
        }
    }
    private void TriggerEffect()
    {
        startTime = Time.time;
        currentY = 0;
    }
}
