using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float panSpeed = 20f;
    public Vector2 panLimit;
    public float panMinY = 0f;
    public float panMaxY = 0f;
    public float panMinX = 0f;
    public float panMaxX = 0f;
    public float minY = 400f;
    public float maxY = 800f;

	// Update is called once per frame
	void Update () {

        // Checks if camera can be freely moved.
        if(Global.cameraLock == false)
        {
            Vector3 pos = transform.position;

            // Gets input to pan camera in four directions
            if (Input.GetKey("w"))
            {
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                pos.x += panSpeed * Time.deltaTime;
            }

            // Zooms camera in and out
            pos.x = Mathf.Clamp(pos.x, panMinX, panMaxX);
            pos.y = Mathf.Clamp(pos.y, panMinY, panMaxY);

            if (Input.GetKey("q"))
            {
                Camera.main.orthographicSize += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("e"))
            {
                Camera.main.orthographicSize -= panSpeed * Time.deltaTime;
            }

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minY, maxY);

            // Sets the new position per update
            transform.position = pos;
        }

        
	}
}
