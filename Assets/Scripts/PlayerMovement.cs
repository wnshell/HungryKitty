using UnityEngine;
using System.Collections;

public enum moveState{
	STOPPED,
	MOVING,
	LEAPING, 
	LOCKED
}

public class PlayerMovement : MonoBehaviour {

	public moveState ms;
	public float speed;
	public float minspeed;
	public float maxspeed;
	public float threshold;
	private Vector3 target;
	public float detectionradius;
	public float hopHeight = 1.25f;
	private bool hopping = false;

	public bool catnipDown;


	public GameObject catnip;



	void Start () {
		target = transform.position;
		ms = moveState.STOPPED;
		catnipDown = false;
	}

	void Update () {
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.y = 3.0f;
		Vector3 diff = target - transform.position;
		//Rotates toward the mouse
		float mag = diff.magnitude;
		if (mag <= 5.0f) {
			ms = moveState.STOPPED;
		}
		if (mag < threshold && ms == moveState.STOPPED) {
			mag = 0.0f;
		} else if (mag > threshold && ms == moveState.STOPPED) {
			ms = moveState.MOVING;
		} else if (mag < minspeed && ms == moveState.MOVING) {
			mag = minspeed;
		} else if (mag > maxspeed && ms == moveState.MOVING) {
			mag = maxspeed;
		}

		Collider[] colliders = Physics.OverlapSphere (transform.position, detectionradius);
		foreach (Collider hit in colliders) {
			Vector3 diffcoll = hit.transform.position - transform.position;
			float magcoll = diffcoll.magnitude;
			if (hit.gameObject.tag == "BadMouse") {
				ms = moveState.LOCKED;
				StartCoroutine (lockOnToMouse (hit.gameObject.transform.position, hit.gameObject));
			}
			else if (hit.gameObject.tag == "Catnip" && ms != moveState.LOCKED && magcoll <= 35.0f) {
				ms = moveState.LEAPING;
				StartCoroutine (leapToCatnip (hit.gameObject.transform.position, 0.6f));
			}
		}

		if (ms == moveState.MOVING) {
			GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0,Mathf.Atan2((target.x - transform.position.x), (target.z - transform.position.z))*Mathf.Rad2Deg - 90, 0);
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime * mag);
		}

		if (Input.GetKeyDown (KeyCode.Space) && mag >= threshold && !catnipDown && ms != moveState.LOCKED) {
			GameObject go;
			go = Instantiate(catnip, target, Quaternion.Euler(0, 0, 0)) as GameObject;
			catnipDown = true;
		}

	}

	IEnumerator leapToCatnip(Vector3 dest, float time){
		MeshRenderer mo = GameObject.Find ("!").GetComponent<MeshRenderer>();
		mo.enabled = true;	
		GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0,Mathf.Atan2((dest.x - transform.position.x), (dest.z - transform.position.z))*Mathf.Rad2Deg - 90, 0);

		yield return new WaitForSeconds (1);
		if (hopping) yield break;

		hopping = true;
		var startPos = transform.position;
		var timer = 0.0f;

		while (timer <= 1.0f) {
			var height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
			transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height; 

			timer += Time.deltaTime / time;
			yield return null;
		}
		hopping = false;
		ms = moveState.STOPPED;
		catnipDown = false;
		mo.enabled = false;

	}

	IEnumerator lockOnToMouse (Vector3 dest, GameObject go){
		MeshRenderer mo = GameObject.Find ("!").GetComponent<MeshRenderer>();
		mo.enabled = true;	
		GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(0,Mathf.Atan2((dest.x - transform.position.x), (dest.z - transform.position.z))*Mathf.Rad2Deg - 90, 0);

		yield return new WaitForSeconds (1);
		if (go.activeInHierarchy) {
			transform.position = Vector3.MoveTowards (transform.position, dest, speed * Time.deltaTime * 5);
		} else {
			mo.enabled = false;
			yield return new WaitForSeconds (1);
			ms = moveState.STOPPED;
		}
	}
}
