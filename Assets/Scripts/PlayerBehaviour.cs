using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	
	private CharacterMotor motor;
	
	private CubeGunBehaviour currentGun;
	
	// Use this for initialization
	void Awake () {
		motor = GetComponent<CharacterMotor>();
		currentGun = GetComponentInChildren<CubeGunBehaviour>();
	}
	

	
	// Update is called once per frame
	void Update () {
		// Get the input vector from kayboard or analog stick
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			var directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
		
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = transform.rotation * directionVector;
		motor.inputJump = Input.GetButton("Jump");
		
		if(Input.GetButtonDown("Fire1")){
			currentGun.Shoot();
		}
		
		if(Input.GetButtonDown("Fire2")){
			currentGun.Zoom();
		}
		
		if(Input.GetKeyDown(KeyCode.R)){
			currentGun.ReloadWeapon();
		}
	
	}
	
	void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "MunitionCubeGun"){
			currentGun.munition += other.GetComponent<MunitionCubeGunBehaviour>().munition;
			if(currentGun.amountBullets == 0)
				currentGun.ReloadWeapon();
			Destroy(other.gameObject);
		}
    }
}
