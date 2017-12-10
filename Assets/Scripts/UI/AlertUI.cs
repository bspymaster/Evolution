using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour {

    public AlertSystem Alerts;
    private Alert currentAlert;
    public Text TitleText;
    public Text DescText;
    public float speed;
    public float top;
    public float bot;

    private Alert blue;

    private Alert red;

	// Use this for initialization
	void Start () {
		
	}
	
    public void AddBlue()
    {
        Alerts.addAlert(blue);
    }

    public void AddRed()
    {
        Alerts.addAlert(red);
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(AlertDisplay());
    }

    IEnumerator AlertDisplay()
    {
        
        currentAlert = Alerts.getAlert();
        if(currentAlert != null)
        {
            TitleText.text = currentAlert.getTitle();
            DescText.text = currentAlert.getDetails();

            while (transform.position.y < top)
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }

            yield return new WaitForSeconds(5f);

            while (transform.position.y > bot)
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }

            currentAlert = null;
        }       
    }
}

