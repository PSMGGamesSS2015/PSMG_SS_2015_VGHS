using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


    public float movementSpeed = 2f;
    public float rotationSpeed = 4f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Move(verticalInput);

        Turn(horizontalInput);
	}

    private void Turn(float horizontalInput)
    {
        Debug.Log("Turn(): " + horizontalInput);
        float angle = rotationSpeed * horizontalInput;
        gameObject.transform.Rotate(transform.up * angle);
    }

    private void Move(float verticalInput)
    {
        Debug.Log("Move(): " + verticalInput);
        Vector3 newPosition = transform.forward.normalized * verticalInput * rotationSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + newPosition);

    }
}
