using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
   private CharacterController controller;
   private Vector3 playerVelocity;
   [SerializeField]
   private float playerSpeed = 1.0f;
   [SerializeField]
   private float rotationSpeed = 20.0f;

    private void Start()
   {
       controller = gameObject.GetComponent<CharacterController>();
   }

    void Update()
   {
        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.W)){
            Vector3 parentAngle = transform.eulerAngles;
            Vector3 rotated = Quaternion.Euler(parentAngle) * new Vector3(0, 0, 1);
            transform.position += new Vector3(rotated.x, 0, rotated.z) * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)){
            Vector3 parentAngle = transform.eulerAngles;
            Vector3 rotated = Quaternion.Euler(parentAngle) * new Vector3(0, 0, -1);
            transform.position += new Vector3(rotated.x, 0, rotated.z) * playerSpeed * Time.deltaTime;
        }
   }
}
