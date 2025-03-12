using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
   private CharacterController controller;
   private Vector3 playerVelocity;
   private bool groundedPlayer;
   [SerializeField]
   private float playerSpeed = 10.0f;
   private float jumpHeight = 1.0f;
   private float gravityValue = -9.81f;

    private void Start()
   {
       controller = gameObject.GetComponent<CharacterController>();
   }

    void Update()
   {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
   }
}
