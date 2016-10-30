using UnityEngine;
using System.Collections;

public class run : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (GoAway ());
	}

	IEnumerator GoAway(){
		yield return new WaitForSeconds (4);
		this.gameObject.SetActive (false);
	}
}
