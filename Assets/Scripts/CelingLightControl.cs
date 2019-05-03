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
    Renderer render;

    public virtual void Start()
    {
        render = gameObject.GetComponent<Renderer>();
    }

   
    public virtual void Update()
    {
        
    }

    public virtual void LightOn()
    {
        Material[] mats = render.materials;
        mats[2] = LightOn_mat;
        render.materials = mats;
        LightObject.SetActive(true);
        particleObject.SetActive(true);
    }

    public virtual void LightOff()
    {
        Material[] mats = render.materials;
        mats[2] = Lightoff_mat;
        render.materials = mats;
        LightObject.SetActive(false);
        particleObject.SetActive(false);
    }


}
