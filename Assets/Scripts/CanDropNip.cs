using UnityEngine;
using System.Collections;

public class CanDropNip : MonoBehaviour {

	public PlayerMovement S;


	void Start(){
		S = GameObject.Find ("Cat").GetComponent<PlayerMovement> ();
	}

	void OnMouseOver(){
		S.canDropNip = true;
	}
	void OnMouseExit(){
		S.canDropNip = false;
	}
}
