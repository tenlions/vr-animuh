using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // the renderer holding the material to be changed
    private Renderer renderer_self;
   
    // sets the '_RGB' and '_Alpha' values of the material to the given rgb color and alpha
    public void SetRGBA(Color rgb, float alpha)
    {
        renderer_self = GetComponent<Renderer>();
        renderer_self.material.SetColor("_RGB", rgb);
        renderer_self.material.SetFloat("_Alpha", alpha);
    }

    // sets the '_RGB' value of the material to the given rgb color
    public void SetRGB(Color rgb)
    {
        renderer_self = GetComponent<Renderer>();
        renderer_self.material.SetColor("_RGB", rgb);
    }

    // sets the '_Alpha' value of the material to the given alpha
    public void SetAlpha(float alpha)
    {
        renderer_self = GetComponent<Renderer>();
        renderer_self.material.SetFloat("_Alpha", alpha);
    }

    public Renderer GetRenderer()
    {
        return renderer_self;
    }
}
