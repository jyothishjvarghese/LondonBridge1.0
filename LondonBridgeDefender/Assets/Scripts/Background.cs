using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public Transform[] backgrounds;         //Array of all the backgrounds to be parallaxed.
    public float parallaxScale;             //proportion of the camera's movement to move the backgrounds by.
    public float parallaxReductionFactor;  // How much less each successive layer should parallax.
    public float smoothing;                 //How smooth the parallax effect should be.

    private Transform cam;                  //shorter reference to the main camera's transform.
    private Vector3 previousCamPos;         // The position of the camera in the previous frame.

    private void Awake()
    {
        //Setting up the reference shortcut.
        cam = Camera.main.transform;
    }

    // The 'previous frame' had the current frame's camera position.
    void Start ()
    {
        previousCamPos = cam.position;
	}
	
	
	void Update ()
    {
        //The parallax is the oppositive of the camera movement since the previous frame multiplied by the scale. 
        float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;

        for(int i = 0; i < backgrounds.Length; i++ )
        {
            //... set a target x position which is their current position plus the parallax multiplied by the reduction
            float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);

            // Create a target position which is the background's current position but with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //Lerp the background's position between itself and it's target position.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //Set the previousCamPos to the camera's position at the end of this frame.
        previousCamPos = cam.position;
	}
}
