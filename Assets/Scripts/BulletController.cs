using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public int speed;
    Collider2D coll;
    GameObject player;
    Vector3 dir;

    // Use this for initialization
    void Start () {
        coll = gameObject.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        gameObject.transform.Translate(dir * speed/10);    
    }

    void onTriggerEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            print("hit");
            dir = -dir;
            gameObject.transform.position = coll.transform.position;
        }
    }

}
