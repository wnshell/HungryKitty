using UnityEngine;
using System.Collections;

public class CatNip : MonoBehaviour {

	public PlayerMovement S;

	void Start(){
		S = GameObject.Find ("Cat").GetComponent<PlayerMovement> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Cat") {
			S.catnipDown = false;
			Destroy (this.gameObject);
		}
	}
}
