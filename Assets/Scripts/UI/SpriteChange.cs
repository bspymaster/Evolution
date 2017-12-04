using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {

    public Button button;
    public Sprite Unadded;
    public Sprite Added;

    //    float timer = 1f;
    //    float delay = 1f;

    private void Start()
    {
        button.image.sprite = Unadded;
    }

    public void toggle2()
    {
        if(button.image.sprite == Unadded)
        {
            button.image.sprite = Added;
        }
        else
        {
            button.image.sprite = Unadded;
        }
        
    }



    /*



        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (this.gameObject.GetComponent<SpriteRenderer>().sprite == Unadded)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Added;
                    timer = delay;
                    return;
                }
                if (this.gameObject.GetComponent<SpriteRenderer>().sprite == Added)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Unadded;
                    timer = delay;
                    return;
                }

            }
        }
    */
}
