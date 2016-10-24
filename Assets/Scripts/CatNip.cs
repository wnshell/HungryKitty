using UnityEngine;
using System.Collections;

public class CatNip : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Cat") {
			Destroy (this.gameObject);
		}
	}
}
