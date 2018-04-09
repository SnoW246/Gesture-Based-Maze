using UnityEngine;
using System.Collections;

// Ball movement controlls with simple third-person camera
public class RollerBall : MonoBehaviour {
    // Declaration of public local class variables
	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

    // Declaration of private local class variables
	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

    // Run and get components
	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
	}// End of Start

    // Ball movement
	void FixedUpdate () {
		if (mRigidBody != null) {
            // Horizontal ball movement
			if (Input.GetButton ("Horizontal")) {
				mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);

                // Hand gesture for horizontal movements recognized will go here

			}// End of if

            // Vertical ball movement
			if (Input.GetButton ("Vertical")) {
				mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);

                // Hand gesture recognized will go here

            }// End of if

            // Jumping ball movement 
            if (Input.GetButtonDown("Jump")) {
                // Hand gesture recognized will go here

                if (mAudioSource != null && JumpSound != null){
                    // Play jumping sound
					mAudioSource.PlayOneShot(JumpSound);
				}// End of if

                // Apply physics
				mRigidBody.AddForce(Vector3.up*200);
			}// End of if
        }// End of if

        // Camera view
        if (ViewCamera != null) {
			Vector3 direction = (Vector3.up*2+Vector3.back)*2;
			RaycastHit hit;
            // Camera view debugging on the go
			Debug.DrawLine(transform.position,transform.position+direction,Color.red);

			if(Physics.Linecast(transform.position,transform.position+direction,out hit)){
				ViewCamera.transform.position = hit.point;
			}// End of if

            else {
				ViewCamera.transform.position = transform.position+direction;
			}// End of else
			ViewCamera.transform.LookAt(transform.position);
		}// End of if
    }// End of FixedUpdate

    // When collision happens
    void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}// End of if
		}// End of if

        else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}// End of if
		}// End of else
    }// OnCollisionEnter

    // When colision is over
    void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}// End of if
    }// End of OnCollisionExit

    // When collecting a coin
    void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}// End of if

            // Remove the coin from the scene
			Destroy(other.gameObject);
		}// End of if
    }// End of OnTriggerEnter
}// RollerBall