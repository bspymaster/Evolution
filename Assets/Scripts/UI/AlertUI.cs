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
    private bool pleaseWait = false;

    private Alert blue;

    private Alert red;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector2(x, y1);
    }
	
    public void AddBlue()
    {
        Alerts.addAlert(new Alert("Ballad for a Crab on the Summer of Midnight", "It was by my own devices that it came to me upon that dreary midnight moon.  Seven crabs rode high and plain upon their golden steads of sand."));
    }

    public void AddRed()
    {
        Alerts.addAlert(new Alert("Stove by Day", "Stove by Night"));
        Alerts.addAlert(new Alert("Mungus by Day", "Mungus by Night"));
        Alerts.addAlert(new Alert("Jew by Day", "Jew by Night"));
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
                TitleText.text = currentAlert.getTitle();
                DescText.text = currentAlert.getDetails();


                this.transform.position = new Vector2(x, y2);
                yield return new WaitForSeconds(AlertDelay);
                this.transform.position = new Vector2(x, y1);

                currentAlert = null;
                pleaseWait = false;
            }
        }
        
    }
}

