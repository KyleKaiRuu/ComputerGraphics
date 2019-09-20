using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttoons : MonoBehaviour
{
    //The current animation state of the animator
    public string currentAniState;

    //Determines whether or not the object being animated is in an idle state
    public bool idle;

    //The cube object in the scene
    public GameObject cube;
    //The mesh renderer on the cube
    MeshRenderer cubeRenderer;

    //The button gameobject that lands the dragon
    public GameObject landButton;
    //The button gameobject that allows the dragon to take off
    public GameObject takeOffButton;
    //the component for the takeOffButton
    Button takeOffComponent;
    //the component for the landButton
    Button landComponent;

    //The Dragon animator controller
    public Animator dragonController;

    //whether or not the dragon is grounded
    public bool dragonGrounded = false;

    //the Button that controls changing the cube to the hole material
    public GameObject holeButton;
    //The Button that controls changing the cube to the lightning material
    public GameObject lightningButton;
    //the component for the takeOffButton
    Button holeButtonComponent;
    //the component for the landButton
    Button lightningButtonComponent;

    //The particle system in the scene
    public ParticleSystem particle;

    //Used for initialization
    private void Start()
    {
        //Initialize the takeOff component
        takeOffComponent = takeOffButton.GetComponent<Button>();
        //Initialize the land component
        landComponent = landButton.GetComponent<Button>();

        //Initialize the hole Component
        holeButtonComponent = holeButton.GetComponent<Button>();
        //Initialize the lightning component
        lightningButtonComponent = lightningButton.GetComponent<Button>();

        //Initialize the Renderer on the cube
        cubeRenderer = cube.GetComponent<MeshRenderer>();
    }

    //happens every frame
    private void Update()
    {
        //Check if animation is finished based on the currentAniState
        if (dragonController.GetCurrentAnimatorStateInfo(0).IsName(currentAniState) && dragonController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            //If the animation is finished, set Idle to true
            idle = true;
        }
    }

    //Change the animation, called on a button
    public void ChangeAnimation(string animationState)
    {
        //Set the currentAniState to the animation state given in the function
        currentAniState = animationState;
        //Set Idle to false
        idle = false;
        //if the animation state is "Bite"
        if (animationState == "Bite")
        {
            //If the dragon is grounded
            if (dragonGrounded)
            {
                //Change the animation state to GroundBite instead of Bite
                animationState = "GroundBite";
            }
            //If the dragon is not grounded
            else
            {
                //Change the animation state to FlyBite instead of Bite
                animationState = "FlyBite";
            }
        }

        //If the animation state is "Fire"
        if(animationState == "Fire")
        {
            //If the dragon is grounded
            if (dragonGrounded)
            {
                
                //Set the animation state to FireGround
                animationState = "FireGround";
                //Set the currentAniState to the animation state
                currentAniState = animationState;
            }
            //If the dragon is not grounded
            else
            {
                //Set the animation state to FireFly
                animationState = "FireFly";
                //Set the currentAniState to the animation state
                currentAniState = animationState;
            }
        }
        
        //If the animation state is TakeOff
        if (animationState == "TakeOff")
        {
            //The dragon is no longer grounded, set it to false
            dragonGrounded = false;
            //Disable interaction with the take off button
            takeOffComponent.interactable = false;
            //if the land button is not interatable
            if (!landComponent.interactable)
            {
                //Make it interactable
                landComponent.interactable = true;
            }
        }

        //If the animation state is land
        if (animationState == "Land")
        {
            //The dragon is now grounded, set it to true
            dragonGrounded = true;
            //Disable interaction with the land button
            landComponent.interactable = false;
            // if the takeoff button is not interactable
            if (!takeOffComponent.interactable)
            {
                //Make it interactable
                takeOffComponent.interactable = true;
            }
        }

        //If idle is true
        if (idle)
        {
            //and the dragon is grounded
            if (dragonGrounded)
            {
                //Set the animation state to IdleGround
                animationState = "IdleGround";
                //Set the currentAniState to the animation state
                currentAniState = animationState;
            }
            //otherwise
            else
            {
                //Set the animation state to IdleFly
                animationState = "IdleFly";
                //and set tje currentAniState to the animation state
                currentAniState = animationState;
            }
        }
        //Play the animation state on the animator controller
        dragonController.Play(animationState);
    }

    //Change the scene if needed or quits out of the program, called on a button
    public void ChangeScene(string scene)
    {
        //If loading a scene
        if (scene != "Quit")
        {
            //Load it
            SceneManager.LoadScene(scene);
        }
        //Otherwise
        else
        {
            //Quit the application
            Application.Quit();
        }
    }

    //Changes the material on the cube, called on a button
    public void ChangeMaterial(Material material)
    {
        //Set the cube material to the material given by the button
        cubeRenderer.material = material;

        //If the material is the HoleCube material
        if (cubeRenderer.material.name == "HoleCube (Instance)")
        {
            //Disable interaction for the holeButton
            holeButtonComponent.interactable = false;
            //Enable interaction for the Lightning button
            lightningButtonComponent.interactable = true;
        }

        //If the material is the LighningCube material
        if(cubeRenderer.material.name == "LightningCube (Instance)")
        {
            //Disable interaction for the lighning button
            lightningButtonComponent.interactable = false;
            //Enable interaction for the holeButton;
            holeButtonComponent.interactable = true;
        }
    }
}
