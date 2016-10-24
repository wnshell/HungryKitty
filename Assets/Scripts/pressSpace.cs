using UnityEngine;
using System.Collections;

public class pressSpace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (GoAway ());
	}
	
	IEnumerator GoAway(){
		yield return new WaitForSeconds (10);
		this.gameObject.SetActive (false);
	}
}
