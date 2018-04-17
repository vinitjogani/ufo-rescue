using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed;
	public Text textBox; 
	public Text gameOverText;
	private int count;
	private bool gameOver;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		count = 0;
		gameOver = false;
		gameOverText.text = "";
	}

	void Update()
	{
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;
		}
		if (Input.GetKeyDown (KeyCode.R) || fingerCount > 1) {
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void FixedUpdate()
	{
		if (!gameOver) 
		{
			if (SystemInfo.deviceType == DeviceType.Handheld) {
				Vector2 dir = Vector2.zero;
				dir.x = Input.acceleration.x;
				dir.y = Input.acceleration.y;
				if (dir.sqrMagnitude > 1)
					dir.Normalize ();

				dir *= Time.deltaTime;
				transform.Translate (dir * speed);
			} else {
				float moveHorizontal = Input.GetAxis ("Horizontal");
				float moveVertical = Input.GetAxis ("Vertical");
				rb2d.velocity = new Vector2 (moveHorizontal * speed, moveVertical * speed); 
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive (false);
			count++;
			if (count == 20) {
				gameOver = true;
				gameOverText.text = "YOU WON";
				rb2d.velocity = new Vector2(0,0);
			}
			textBox.text = "SCORE: " + count.ToString ();
		}
		else if (other.gameObject.tag == "Rocket") 
		{
			gameOverText.text = "GAME OVER";
			rb2d.velocity = new Vector2(0,0);
			gameOver = true;
		}
	}
}
