using UnityEngine;
using System.Collections;
using Pose = Thalmic.Myo.Pose;
using VibrationType = Thalmic.Myo.VibrationType;
using Thalmic.Myo;
using System;
//using System.Drawing;

// Ball movement controlls with simple third-person camera
public class RollerBall : MonoBehaviour {
    // Declaration & initialization of public local class variables
	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

    public GameObject myo;
    public float speed;
    public float torque = 50;

    ThalmicMyo thalmicMyo;
    private Pose lastPose = Pose.Unknown;
    // Declaration & initialization of private local class variables
    private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
    private bool mFloorTouched = false;
    private UnityEngine.Vector3 movement;

    // Run and get components
    void Start () {
        MyoConnector myoConnect = new MyoConnector();
        myo = GameObject.FindWithTag("Myo");
        thalmicMyo = myo.GetComponent<ThalmicMyo>();

        mRigidBody = GetComponent<Rigidbody> ();
        mAudioSource = GetComponent<AudioSource> ();
    }// End of Start
  
    // Ball movement
    void FixedUpdate () {
        float moveHorizontal = thalmicMyo.accelerometer.x;
        float moveVertical = thalmicMyo.accelerometer.z;
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        movement = new UnityEngine.Vector3(moveHorizontal, 0.0f, moveVertical);


        // Myo armband ball movement
        // Comment out this if statement to not use
        if (mRigidBody != null)
        {
            if (thalmicMyo.pose == Pose.Fist)
            {
                print("FIST");
                mRigidBody.AddTorque(UnityEngine.Vector3.back * Input.GetAxis("Horizontal") * 10);
                mRigidBody.AddTorque(UnityEngine.Vector3.back * Input.GetAxis("Vertical") * 10);
                mRigidBody.AddForce(movement * speed);
            }

            else if(thalmicMyo.pose == Pose.FingersSpread && lastPose != Pose.FingersSpread)
            {
                print("FINGERS SPREAD");
                mRigidBody.AddForce(UnityEngine.Vector3.up * 10);
            }
        }

        // Regular WASD keyboard controls
        // Uncomment this if statement section to use
        //if (mRigidBody != null)
        //{
        //    // Horizontal ball movement
        //    if (Input.GetButton("Horizontal"))
        //    {
        //        mRigidBody.AddTorque(UnityEngine.Vector3.back * Input.GetAxis("Horizontal") * 10);
        //    }// End of if

        //    // Vertical ball movement
        //    if (Input.GetButton("Vertical"))
        //    {
        //        mRigidBody.AddTorque(UnityEngine.Vector3.right * Input.GetAxis("Vertical") * 10);
        //    }// End of if

        //    // Jumping ball movement 
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        if (mAudioSource != null && JumpSound != null)
        //        {
        //            // Play jumping sound
        //            mAudioSource.PlayOneShot(JumpSound);
        //        }// End of if

        //        // Apply physics
        //        mRigidBody.AddForce(UnityEngine.Vector3.up * 200);
        //    }// End of if
        //}// End of if

        // Camera view
        if (ViewCamera != null)
        {
            UnityEngine.Vector3 direction = (UnityEngine.Vector3.up * 2 + UnityEngine.Vector3.back) * 2;
            RaycastHit hit;
            // Camera view debugging on the go
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);

            if (Physics.Linecast(transform.position, transform.position + direction, out hit))
            {
                ViewCamera.transform.position = hit.point;
            }// End of if

            else
            {
                ViewCamera.transform.position = transform.position + direction;
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