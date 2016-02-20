using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

    public GameObject gun;
	// Use this for initialization
	void Start () {
        gun = transform.Find("Gun").gameObject;
        
       // gun.GetComponent<Animator>().Play("Idle");
	}

    void Shoot ()
    {
        gun.GetComponent<Animator>().Play("Turret Animation");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
