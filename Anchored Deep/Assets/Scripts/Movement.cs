using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField]
   private float playerSpeed = 1.0f;
   [SerializeField]
   private float rotationSpeed = 20.0f;
   [SerializeField]
   Camera cam;

    private void Start()
   {

   }

    void Update()
   {
        if (Input.GetKey(KeyCode.D)){
            if(Input.GetKey(KeyCode.S))
            {
                //BoatLeft();
            } else 
            {
                BoatRight();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if(Input.GetKey(KeyCode.S))
            {
                //BoatRight();
            } else
            {
                BoatLeft();
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 parentAngle = transform.eulerAngles;
            Vector3 rotated = Quaternion.Euler(parentAngle) * new Vector3(0, 0, 1);
            transform.position += new Vector3(rotated.x, 0, rotated.z) * playerSpeed * Time.deltaTime;
        }

        // if (Input.GetKey(KeyCode.S))
        // {
        //     Vector3 parentAngle = transform.eulerAngles;
        //     Vector3 rotated = Quaternion.Euler(parentAngle) * new Vector3(0, 0, -1);
        //     transform.position += new Vector3(rotated.x, 0, rotated.z) * playerSpeed * Time.deltaTime;
        // }

        // if(Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.A))
        // {
        //     transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        // }
        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        //     transform.rotation = Quaternion.Euler(0, transform.rotation.y, 10);
        // }
   }

   public void BoatRight()
   {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.Self);
   }

   public void BoatLeft()
   {
        transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0), Space.Self);
   }
}
