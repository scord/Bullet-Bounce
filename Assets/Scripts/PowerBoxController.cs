using UnityEngine;
using System.Collections;

public class PowerBoxController : MonoBehaviour {

    bool destroyed = false;
    public GameObject explosion;
    public GameObject smoke;
    public GameObject door;
    public Sprite broken;

	// Use this for initialization
	void Start () {
	
	}
  
	void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            Debug.Log("TEST");
            destroyed = true;

            explosion.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.1f);
            smoke.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.1f);
            Instantiate(explosion);
            Instantiate(smoke);
            gameObject.GetComponent<SpriteRenderer>().sprite = broken;
            door.gameObject.GetComponent<DoorController>().Open();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
