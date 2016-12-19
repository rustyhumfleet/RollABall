﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {

    //// Rigidbody component of Player Prefab
    //public Rigidbody rb;


    public GameObject playerPrefab;                             // Player Prefab
    public float jump;
    public bool isGrounded;
    public float speed;

    //public GameObject playerClone;


    //private int count;
    //private bool winner;
    //public Text countText;
    //public Text winText;



    void Start () 
	{
        //rb = playerPrefab.GetComponent<Rigidbody>();
        jump = 100f;
        isGrounded = true;
        speed = 5;

        //SpawnPlayer();
        //count = 0;
        //winner = false;
        //SetCountText ();
        //SetWinText ("");
    }

    //// called once per frame
    //void Update () {
    //	winner = CheckWin ();

    //	if (winner) {
    //		//set text
    //		SetWinText("You Win!");
    //		// remove ground to go down
    //		GameObject removeGO = FindClosestGround ();
    //		removeGO.SetActive (false);


    //		ResetGame();
    //	}
    //}

    // physics code
    void FixedUpdate()
    {
        //Debug.Log("Player Fixed Update");
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            movement.y = jump;
            isGrounded = false;
        }
        //Debug.Log("movement x " + movement.x + " y " + movement.y + " z " + movement.z);
        //Debug.Log("speed " + speed);
        playerPrefab.GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            // set score somehow
            //count++;
            //SetCountText();
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    //bool CheckWin ()
    //{
    //	return (count >= 11);

    //	//if (winText.text == "You Win" && other.gameObject.CompareTag ("Ground")) {
    //	//	other.gameObject.SetActive (false);
    //	//
    //	//}
    //}

    //void ResetGame ()
    //{
    //	SetWinText ("");
    //	count = 0;
    //	winner = false;
    //}

    //void SetCountText () {
    //	countText.text = "Count: " + count.ToString ();
    //}

    //void SetWinText (string text)
    //{
    //	winText.text = text;
    //}

    //GameObject FindClosestGround() {
    //	GameObject[] gos;
    //	gos = GameObject.FindGameObjectsWithTag("Ground");
    //	GameObject closest = null;
    //	float distance = Mathf.Infinity;
    //	Vector3 position = transform.position;
    //	foreach (GameObject go in gos) {
    //		Vector3 diff = go.transform.position - position;
    //		float curDistance = diff.sqrMagnitude;
    //		if (curDistance < distance) {
    //			closest = go;
    //			distance = curDistance;
    //		}
    //	}
    //	return closest;
    //}

    public void SetupPlayer()
    {
        // maybe pass in an object to upgrade players abilities

        // instantiate player
        SpawnPlayer();

        //// set up the player
        //SetPlayerUp();


    }

    private void SetPlayerUp()
    {
        // pass in object to setup up the player?
    }

    private void SpawnPlayer()
    {
        
        //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current position cast it to GameObject.
        Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}

