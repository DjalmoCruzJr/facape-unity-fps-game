using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {
	
	public float speed = 10;
	public float timeToLive = 4;
	
	private float currentTimeToLive = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		currentTimeToLive += Time.deltaTime;
		
		if(currentTimeToLive > timeToLive)
			Destroy(gameObject);
		
		transform.Translate(Vector3.forward*speed);
	
	}
}
