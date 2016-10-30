using UnityEngine;
using System.Collections;

public class tailstart : MonoBehaviour {

	public Rigidbody rb;

	public int force;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (new Vector3 (0, 0, 1) * rb.mass * force );
	}
}
