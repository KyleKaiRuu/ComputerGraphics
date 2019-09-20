using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    //The gameobject that controls the postProcessing
    public GameObject postProcessing;
    //The cube object from the scene
    public GameObject cube;

    //The sliders that control the individual colors
    public Slider red;
    public Slider green;
    public Slider blue;

    //Changes the bloom in the scene using a slider
    public void UpdateBloom(float value)
    {
        //Gets the bloom component and increased the intensity based on the value given by the slider
        postProcessing.GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>().intensity.Override(value);
    }

    //Changes the color of the emission of the cube
    public void ChangeColor(float value)
    {
        //Sets the cubes emission color to the values of the three color sliders
        cube.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Vector4(red.value, green.value, blue.value));
    }
}
