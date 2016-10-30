using UnityEngine;
using System.Collections;

public class tail : MonoBehaviour {

	public Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		rb.AddForce(-Physics.gravity * rb.mass);
	}
}
