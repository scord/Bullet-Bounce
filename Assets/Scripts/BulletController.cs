﻿using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    const int MAX_DISTANCE = 20;
    public float speed;
    Collider2D coll;
    GameObject player;
    Vector3 dir;
    GameObject shield;
    EdgeCollider2D shieldEdge;
    PlayerController playerControl;
    Rigidbody2D playerRigid;
    // Use this for initialization
    void Start () {
        coll = gameObject.GetComponent<Collider2D>();

        player = GameObject.FindWithTag("Player");
        shield = GameObject.FindWithTag("Shield");

        if (player)
        {
            playerRigid = player.GetComponent<Rigidbody2D>();


            playerControl = player.GetComponent<PlayerController>();
            shieldEdge = shield.GetComponent<EdgeCollider2D>();
            dir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        }
        else
        {
            dir = Vector3.right;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {
        float dotp = Vector3.Dot(playerRigid.velocity, dir);
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, dir, speed*3);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Shield")
            {
                dir = Vector3.Reflect(dir, hit.normal);
                float angle = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg;
                print(angle);
                if (angle >= 315 || angle <= -45)
                    
                    playerRigid.velocity = (-dir * playerControl.jumpPower * 2f);
            }
            if (hit.collider.gameObject.tag == "Floor")
            {
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Power")
            {
                hit.collider.gameObject.GetComponent<PowerBoxController>().Destroy();
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerController>().Destroy();
                Destroy(gameObject);
            }
        }
        gameObject.transform.Translate(dir * speed);
        if (gameObject.transform.position.magnitude > MAX_DISTANCE)
        {
            Destroy(gameObject);
        }
    }

}
