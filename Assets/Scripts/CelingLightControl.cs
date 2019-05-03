using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelingLightControl : MonoBehaviour
{
    public Material LightOn_mat;
    public Material Lightoff_mat;
    public Light LightObject;
    public ParticleSystem particles;
    Render render; 


    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Render>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightOn()
    {

    }

    public void LightOff()
    {

    }


}
