using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float runningSpeed = 1.5f;

	public int enemyDamage = 10;

	Rigidbody2D enemyRigidbody;

	public bool facingRight = false;

	private Vector3 startPosition;

	void Awake() {

		enemyRigidbody = GetComponent<Rigidbody2D>();
		startPosition = this.transform.position;
		GetComponent<AudioSource>().Play();

	}


	// Use this for initialization
	void Start () {
		
		this.transform.position = startPosition;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		float currentRunningSpeed = runningSpeed;

		if(facingRight) {
			//Mirando hacia la derecha
			currentRunningSpeed = runningSpeed;
			this.transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else {
			//Mirando hacia la izquierda
			currentRunningSpeed = -runningSpeed;
			this.transform.eulerAngles = Vector3.zero;
		}

		if(GameManager.sharedInstance.currentGameState == GameState.inGame) {
			enemyRigidbody.velocity = new Vector2(currentRunningSpeed, enemyRigidbody.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {

		if(collision.tag == "Coin") {
			return;
		}
		
		if(collision.tag == "Player") {
			collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
			return;
		}

		//Si llegamos aqui, no hemos chocado ni con monedas, ni con players
		//Lo mas normal es que aqui haya otro enemigo o bien escenario
		//Vamos a hacer que el enemigo rote
		facingRight =! facingRight;

	}
}
