using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

//test //test

	private Vector3 offset;
    //test
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame but guarantees to run at the end
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
