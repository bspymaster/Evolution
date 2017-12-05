using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutatePoints : MonoBehaviour {

    public Text textPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        textPrefab.text = "Mutation Points: " + Global.mutationPoints;
    }
}
