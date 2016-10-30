using UnityEngine;
using System.Collections;

public class Yarn : MonoBehaviour {

	public PlayerMovement S;

	void Start(){
		S = GameObject.Find ("Cat").GetComponent<PlayerMovement> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Cat") {
			S.yarnDown = false;
			Destroy (this.gameObject);
		}
	}
}
