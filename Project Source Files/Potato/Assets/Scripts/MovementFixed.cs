using UnityEngine;
using System.Collections;

public class MovementFixed : MonoBehaviour
{

	Animator anim;
	public float speed = 0.5f;
	public float jump = 1f;
	public float sidejump=1;
	private float swingforce;
	public float swingforcepower;
	public bool onGround;


	void start ()
	{
		onGround = true;
		anim.SetBool ("Moving", true);
		anim.SetBool ("Jumping", false);
		anim.SetBool ("Ground", false);
	}



	void Update ()
	{
		anim = gameObject.GetComponent<Animator> ();
		// anim.Setfloats are the isolated errors where its coming from.
		anim.SetBool ("Moving", false);

		movement ();

		if (Input.GetMouseButton(0)) {
			if (onGround == false) {
				swingforce = swingforcepower;
				if (Input.GetKey (KeyCode.D)) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (swingforce*(Time.deltaTime/6), 0), ForceMode2D.Impulse);

				}

				if (Input.GetKey (KeyCode.A)) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-swingforce*(Time.deltaTime/6), 0), ForceMode2D.Impulse);


				}

				anim.SetBool ("Moving", false);
			}
		}
		if (Input.GetKeyUp (KeyCode.E)) {
			swingforce = 0;
		}
	}


	void movement ()
	{
		if (Input.GetKey (KeyCode.A)) {
			if (onGround == true) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			transform.localRotation = Quaternion.Euler (0, 180, 0);

				anim.SetBool ("Moving", true);
			} 
			if (onGround == false) {
			}
		}
		if (Input.GetKey (KeyCode.D)) {
			if (onGround == true) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			transform.localRotation = Quaternion.Euler (0, 0, 0);

				anim.SetBool ("Moving", true);
			}
			if (onGround == false) {
			}
		}
		if (Input.GetKey (KeyCode.W)) {
			
			if (onGround == true) {
				
				if (Input.GetKey (KeyCode.D)) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (sidejump, 0), ForceMode2D.Impulse);
					anim.SetBool ("Jumping", true);
				}
				if (Input.GetKey (KeyCode.A)) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-sidejump, 0), ForceMode2D.Impulse);
					anim.SetBool ("Jumping", true);
				
				}
			

			}
		}
		if (Input.GetKey (KeyCode.W)) {
			if (onGround == true) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jump), ForceMode2D.Impulse);
				onGround = false;

			}
		}
		if (onGround == false) {
			anim.SetBool ("Moving", false);
			anim.SetBool ("Jumping", true);
		}
		/*if (Input.GetKey (KeyCode.S)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
			anim.SetBool ("Moving", true);
		}*/

	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "Ground") {
			anim.SetBool ("Jumping", false);
			anim.SetBool ("Ground", true);
			onGround =true;
		} 
	
	}
	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "Ground") {
			anim.SetBool ("Ground", false);
			onGround = false;
		}
	}

}
