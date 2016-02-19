using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{

    Collider2D collider;
    
    public bool collision = false;
	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(gameObject.transform.rotation);	
	}

    void FixedUpdate()
    {
        if (!collider.IsTouchingLayers())
            collision = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        collision = true;
    }
}
