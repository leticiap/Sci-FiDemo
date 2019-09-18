using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    private float _sensitivity = 3f;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x -= _mouseY * _sensitivity;
        if (newRotation.x > 180.0f && newRotation.x < 270.0f)
            newRotation.x = 270.0f;
        else if (newRotation.x < 180.0f && newRotation.x > 90.0f)
            newRotation.x = 90.0f;
        transform.localEulerAngles = newRotation;


    }
}
