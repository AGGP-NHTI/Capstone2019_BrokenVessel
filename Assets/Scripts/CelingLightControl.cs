using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class CelingLightControl : MonoBehaviour
{
    public Material LightOn_mat;
    public Material Lightoff_mat;
    public GameObject LightObject;
    public GameObject particleObject;
    public float flickerMax = 2f;
    public float flickerMin = .5f;
    float flickerRange = 0;
    float flickercount = 0;
    public bool IsLightOn = true; 

    Renderer render;

    public virtual void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        flickerRange = Random.Range(flickerMin, flickerMax);
        flickercount = 0;
    }

   
    public virtual void Update()
    {
        flickercount += Time.deltaTime;


        if (flickercount > flickerRange)
        {
            if (IsLightOn)
            {
                LightOff();
            }
            else
            {
                LightOn();
            }
            flickerRange = Random.Range(flickerMin, flickerMax);
            IsLightOn = !IsLightOn;
            flickercount = 0;
        }

    }

    public virtual void LightOn()
    {
        if (!LightObject || !particleObject)
        {
            Debug.Log("CeilingLightControl: Can't find the LightObject or particleObject");
            return;
        }
        if (!LightObject)
        {
            Debug.Log("CeilingLightControl: LightObject is already set to false");
            return;
        }
        if (!particleObject)
        {
            Debug.Log("CeilingLightControl: particleObject is already set to true");
            return;
        }

        Material[] mats = render.materials;
        mats[2] = LightOn_mat;
        render.materials = mats;
        LightObject.SetActive(true);
        particleObject.SetActive(false);

    }

    public virtual void LightOff()
    {
        if (!LightObject || !particleObject)
        {
            Debug.Log("CeilingLightControl: Can't find the LightObject or particleObject");
            return;
        }
        if (!LightObject)
        {
            Debug.Log("CeilingLightControl: LightObject is already set to false");
            return;
        }
        if (!particleObject)
        {
            Debug.Log("CeilingLightControl: particleObject is already set to true");
            return;
        }
        
        
            Material[] mats = render.materials;
            mats[2] = Lightoff_mat;
            render.materials = mats;
            LightObject.SetActive(false);
            particleObject.SetActive(true);
        
    }


}
