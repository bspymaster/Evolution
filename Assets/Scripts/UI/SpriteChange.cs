using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour {

    public Sprite BlackCircle;
    public Sprite RedCircle;

    float timer = 1f;
    float delay = 1f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == BlackCircle)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = RedCircle;
                timer = delay;
                return;
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == RedCircle)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BlackCircle;
                timer = delay;
                return;
            }

        }
    }

}
