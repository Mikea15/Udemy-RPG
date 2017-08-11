using System.Collections;
using System.Collections.Generic;

using System.Runtime.CompilerServices;

using UnityEngine;

public class SpinMe : MonoBehaviour
{

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;

    void Update ()
    {
        // Time.deltaTime.
        // xDegreesPFrame = Time.DeltaTime, 60, 360, rotationPerMinute
        // degrees frame^-1  = seconds frame^-1, seconds p minute ^-1, 
        // degrees / frame * something else

        
        float xDegreesPerFrame = xRotationsPerMinute * 360 / 60 * Time.deltaTime; // TODO COMPLETE ME
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = yRotationsPerMinute * 360 / 60 * Time.deltaTime; ; // TODO COMPLETE ME
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = yRotationsPerMinute * 360 / 60 * Time.deltaTime; ; // TODO COMPLETE ME
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
