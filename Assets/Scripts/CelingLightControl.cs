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
    private float flickerMax = 5;
    private float flickerMin = 0;
    private float flickerRange;
    WaitForSeconds WFS;
    Renderer render;

    public virtual void Start()
    {
        render = gameObject.GetComponent<Renderer>();
    }

   
    public virtual void Update()
    {
        flickerRange = Random.Range(flickerMax, flickerMin);

        if (flickerRange > flickerMax / 2)
        {
            LightOn();
            WFS = new WaitForSeconds(2);
        }
        if(flickerRange < flickerMax / 2)
        {
            LightOff();
            WFS = new WaitForSeconds(2);
        }
    }

    public virtual void LightOn()
    {
        if (LightObject == null || particleObject == null)
        {
            Debug.Log("CeilingLightControl: Can't find the LightObject or particleObject");
            return;
        }
        else if (LightObject == true)
        {
            Debug.Log("CeilingLightControl: LightObject is already set to true");
            return;
        }
        else if (particleObject == false)
        {
            Debug.Log("CeilingLightControl: particleObject is already set to false");
            return;
        }
        else
        {
            Material[] mats = render.materials;
            mats[2] = LightOn_mat;
            render.materials = mats;
            LightObject.SetActive(true);
            particleObject.SetActive(false);
        }
    }

    public virtual void LightOff()
    {
        if (LightObject == null || particleObject == null)
        {
            Debug.Log("CeilingLightControl: Can't find the LightObject or particleObject");
            return;
        }
        else if (LightObject == false)
        {
            Debug.Log("CeilingLightControl: LightObject is already set to false");
            return;
        }
        else if (particleObject == true)
        {
            Debug.Log("CeilingLightControl: particleObject is already set to true");
            return;
        }
        else
        {
            Material[] mats = render.materials;
            mats[2] = Lightoff_mat;
            render.materials = mats;
            LightObject.SetActive(false);
            particleObject.SetActive(true);
        }
    }


}
