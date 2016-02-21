using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{

    Collider2D collider;
    EdgeCollider2D edgeColl;
    GameObject player;
    Rigidbody2D playerRigid;
    PlayerController playerControl;
    
    public bool collision = false;
	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<Collider2D>();
        edgeColl = gameObject.GetComponent<EdgeCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerControl = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(gameObject.transform.rotation);	
	}
    public int numRays = 10000;

    void FixedUpdate()
    {
        if (!collider.IsTouchingLayers())
            collision = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
            collision = true;
    }
}
