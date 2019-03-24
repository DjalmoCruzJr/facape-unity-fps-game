using UnityEngine;
using System.Collections;

public class CubeGunBehaviour : WeaponBase {
	
	
	// Use this for initialization
	void Start () {
		
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
		base.Update();
	
	}
	
	override public void OnShoot(){
		Instantiate(bullet, canoArma.position, transform.rotation);
	}
	
	
	
	
	
	
	
}
