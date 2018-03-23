using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;                             // Player Prefab
    public GameObject spherePrefab;
    public GameObject groundPrefab;
    public float jump;
    public bool isGrounded;
    public float speed;


    //private int count;
    //private bool winner;
    //public Text countText;
    //public Text winText;

    void Start()
    {
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


        //// shrink sphere
        float targetScale = 0.1f;
        float shrinkSpeed = 0.01f;

        spherePrefab.transform.localScale = Vector3.Lerp(
            spherePrefab.transform.localScale,
            new Vector3(targetScale, targetScale, targetScale),
            Time.deltaTime * shrinkSpeed);

        //float yHeight = 2.0f;

        //groundPrefab.GetComponent<Rigidbody>().transform.Translate(Vector3.up * Time.deltaTime * yHeight, Space.World);

        ////groundPrefab.GetComponent<Rigidbody>().transform.position = new Vector3(
        //    groundPrefab.transform.position.x,
        //    groundPrefab.transform.position.y + yHeight,
        //    groundPrefab.transform.position.z);



        //// raise floor
        //float yHeight = 0.1f;

        //groundPrefab.transform.localPosition = Vector3.Lerp(
        //    groundPrefab.transform.localPosition,
        //    new Vector3(targetScale, targetScale * spherePrefab.transform.localPosition.y, targetScale),
        //    Time.deltaTime * yHeight);


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

        if (other.gameObject.CompareTag("KillingFloor"))
        {
            Destroy(gameObject);
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
}

