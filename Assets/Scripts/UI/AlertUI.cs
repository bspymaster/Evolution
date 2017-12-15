using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour {

    public AlertSystem Alerts;
    private Alert currentAlert;
    public Text TitleText;
    public Text DescText;
    public float AlertDelay;
    public float x;
    public float y1;
    public float y2;
    public float z;
    private bool pleaseWait = false;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(x, y1, z);
    }
	
    // Update is called once per frame
    void Update () {
        StartCoroutine(AlertDisplay());
    }

    IEnumerator AlertDisplay()
    {
        if (pleaseWait == false)
        {
            currentAlert = Alerts.getAlert();
            if (currentAlert != null)
            {
                pleaseWait = true;

                //Sets the UI to the current alert notification
                TitleText.text = currentAlert.getTitle();
                DescText.text = currentAlert.getDetails();

                //Moves UI message box onto screen
                this.transform.position = new Vector2(x, y2);
                yield return new WaitForSeconds(AlertDelay);
                this.transform.position = new Vector2(x, y1);

                currentAlert = null;
                pleaseWait = false;
            }
        }
        
    }
}

