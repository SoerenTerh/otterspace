﻿using UnityEngine;
using System.Collections;


public class Minigame_SI_OtterShip : MonoBehaviour {

    public float SIlive = 3.0f;

	private float speed = 10f;
    public float minX = -7.7f;
	public float maxX = 8.0f;
    public GameObject bullet;
    public GameObject firingPoint;

    // fire cooldown timer
    public float fireCooldownTime = 1.0f;
    float _fireCooldown = 0.0f;

    // Use this for initialization
    void Start () {
        //Assets/Minigames/Minigame_SpaceInvaders bullet =(GameObject)Resources.Load("../Sprites/bullet");
    }

    // Update is called once per frame

    void Update() {

		if (Application.isEditor) {
			Vector3 newPosition = transform.position;
			newPosition.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			transform.position = newPosition;

		
		if (transform.position.x < minX) {
			transform.position = new Vector3(minX, transform.position.y, transform.position.z);
		} else if (transform.position.x > maxX) {
			transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
		}
		} else {
			Vector3 dir = Vector3.zero;
			dir.x = Input.acceleration.x;
			// dir.z = Input.acceleration.x;
			if (dir.sqrMagnitude > 1)
				dir.Normalize ();
			
			dir *= Time.deltaTime;
			transform.Translate (dir * speed);

		
		if (transform.position.x < minX) {
			transform.position = new Vector3(minX, transform.position.y, transform.position.z);}
                else if (transform.position.x > maxX) {
			transform.position = new Vector3(maxX, transform.position.y, transform.position.z);}
		}

    
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
            Instantiate(bullet, firingPoint.transform.position, Quaternion.identity);
            _fireCooldown = fireCooldownTime; ;
            }
        }
   





}



    


 