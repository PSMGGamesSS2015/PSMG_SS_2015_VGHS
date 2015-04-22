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
        float horizontalInputMouse = Input.GetAxis("Mouse X");
        float verticalInputMouse = Input.GetAxis("Mouse Y");

        Move(verticalInput, horizontalInput);

        TurnMouse(verticalInputMouse, horizontalInputMouse);
	}

    private void TurnMouse(float verticalInputMouse, float horizontalInputMouse)
    {
        if (Input.GetAxis("Mouse X") != 0)
        {
            float angleX = rotationSpeed * horizontalInputMouse;
            gameObject.transform.Rotate(transform.up * angleX);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            float angleY = rotationSpeed * verticalInputMouse;
            //gameObject.transform.Rotate(transform.right * angleY);
        }
    }

    private void Move(float verticalInput, float horizontalInput)
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            Vector3 newPosition = transform.forward.normalized * verticalInput * rotationSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(transform.position + newPosition);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 newPosition = transform.right.normalized * horizontalInput * rotationSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(transform.position + newPosition);
        }

    }
}
