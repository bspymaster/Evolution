using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float panSpeed = 20f;
    public Vector2 panLimit;
    public float minY = 400f;
    public float maxY = 800f;

	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

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

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        if (Input.GetKey("q"))
        {
            Camera.main.orthographicSize += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            Camera.main.orthographicSize -= panSpeed * Time.deltaTime;
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minY, maxY);

        transform.position = pos;
	}
}
