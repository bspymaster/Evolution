using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutatePoints : MonoBehaviour {

    public Text textPrefab;

	// Renders the Mutation Points available on screen
	void Update () {
        textPrefab.text = "Mutation Points: " + Global.mutationPoints;
    }
}
