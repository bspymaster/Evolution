using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBox : MonoBehaviour {

    private bool clickedOn = false;

    private void OnGUI()
    {
        if(!clickedOn)
        {
            return;
        }

        Vector3 screenPos = transform.position;
        Rect menuRect = new Rect(screenPos.x, screenPos.y, 1, 1);

        GUI.Label(menuRect, "Fuck you.");
        clickedOn = false;
    }
    private void OnMouseDown()
    {
        clickedOn = true;
    }
}
