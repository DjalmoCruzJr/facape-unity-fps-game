using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour {
	
	public BulletBehaviour bullet;
	public Transform canoArma;
	public Animation animationGun;
	public AnimationClip reloadAnimation;
	public AnimationClip fireAnimation;
	public ParticleSystem fireParticle;
	public AudioClip shotSound;
	public AudioClip reloadSound;
	
	//Zoom
	public AnimationClip zoomIn;
	public AnimationClip zoomOut;
	public float zoomAim = 30;
	private float startZoom;
	private bool inZoom = false;
	///


	public float fireRate = 2;
	private float currentTimeToFire = 0;
	private bool canFire = true;
	public int amountBullets = 16;
	public int munition = 64;
	private int initBullets;
	
	// Use this for initialization
	protected void Start () {
		initBullets = amountBullets;
		startZoom = Camera.mainCamera.fieldOfView;
	}
	
	// Update is called once per frame
	protected void Update () {
		
		if(canFire == false){
			currentTimeToFire += Time.deltaTime;
			if(currentTimeToFire > fireRate){
				currentTimeToFire = 0;
				canFire = true;
			}
		}
		
		
		if(animationGun.isPlaying == true){
			canFire = false;
		}
		else{
			canFire = true;
		}
	
	}
	
	public void Shoot(){
		if(canFire == true && amountBullets > 0){
			animationGun.clip = fireAnimation;
			animationGun.Play();
			fireParticle.Emit(1);
			canFire = false;
			amountBullets--;
			audio.clip = shotSound;
			audio.Play();
			OnShoot();
		}
		else if(amountBullets == 0){
			ReloadWeapon();
		}
	}
	
	abstract public void OnShoot();

	
	public void ReloadWeapon(){
		if(amountBullets < initBullets){
			if(munition > 0){
				CancelZoom();
				
				if(amountBullets <= initBullets){
					int tempBullets = initBullets-amountBullets;
					if(tempBullets >= munition)
						tempBullets = munition;
					amountBullets += tempBullets;
					munition -= tempBullets;
				}
				
				animationGun.clip = reloadAnimation;
				animationGun.Play();
				
				audio.clip = reloadSound;
				audio.Play();
			}
		}
	}
	
	public void Zoom(){
		if(inZoom == false && !animation.isPlaying && !animationGun.isPlaying){
			UseZoom();
		}
		else if(inZoom == true && !animation.isPlaying && !animationGun.isPlaying){
			CancelZoom();
		}
	}
	
	private void UseZoom(){
		if(inZoom == false){
			animation.clip = zoomIn;
			animation.Play();
			inZoom = true;
			Camera.mainCamera.fieldOfView = zoomAim;
		}
	}
	
	private void CancelZoom(){
		
		if(inZoom == true){
			animation.clip = zoomOut;
			animation.Play();
			inZoom = false;
			Camera.mainCamera.fieldOfView = startZoom;
		}
		
	}
	
	void OnGUI() {
		if(!inZoom)
       	 GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100, 20), "+");
    }
}
