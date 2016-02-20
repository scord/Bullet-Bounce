using UnityEngine;
using System.Collections;

public class PowerBoxController : MonoBehaviour {

    bool destroyed = false;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
  
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy();    
        }
    }

    public void Destroy()
    {
        Debug.Log("TEST");
        destroyed = true;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        explosion.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z -0.1f);

        GameObject e = Instantiate(explosion);
  
   
    }

	// Update is called once per frame
	void Update () {
	
	}
}
