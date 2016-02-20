using UnityEngine;
using System.Collections;

public class FlickerEffect : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    float interval = 1.0f;
    // Use this for initialization

    float timer = 0;
    float timer2 = 0;
    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

 
    void Update()
    {
        
    }


    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        timer2 += Time.fixedDeltaTime;
        if (timer > interval)
        {
            interval = Random.Range(0, 0.5f);
            timer = 0;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f + interval);
            timer2 = 0;
        }

        if (timer2 > 0.05f)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            timer2 = 0;
        }
    }
}
