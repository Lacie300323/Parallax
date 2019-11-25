using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingParallax : MonoBehaviour
{
    //Variables
    // Array to add the parallaxing objects. These only includes the elements which the parallaxing apply to.
    public Transform[] backgrounds;

   
    private float[] parallaxScales;   // This variable is going to automatically change the speed of the objects within the transform array.
    public float smoothing = 1f;  // Defines how smooth the parallaxing is.
    private Transform cam;  //Defines the main camera transform
    private Vector3 previousCamPos; //Defiens the position of the camera in the previous frame.

    // Void awake gets called before the Start() of the game. Since I'm going to play with the camera, this is very usefull.
    private void Awake()
    {
        cam = Camera.main.transform;  // Set up Camera reference. There's no need to assign the main camera manually.
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position; //Check for the previous position of the camera.
        parallaxScales = new float[backgrounds.Length]; //The array amount of floats needed to paralax,  is the same as the array amount of background objects.

        //Assigning correspodning paralaxscales to each background object
        for (int i = 0; i < backgrounds.Length; i ++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement, because the previous frame multiplied y the scale.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i]; // how much has it moved, and how much do I want the parallaxing to be.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax; // set a target x position which is the current position + the parallax

            // Create a target position which is the background's current position with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current position and the target position using Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previous Camera position to the camera's position at the end of the frame.
        previousCamPos = cam.position;
    }
}
