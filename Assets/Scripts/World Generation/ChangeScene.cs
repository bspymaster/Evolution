using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToScene : MonoBehaviour {

	public void ChangeToSceneFunc (int sceneToChangeTo) {
        Application.LoadLevel(sceneToChangeTo);
	}
}
