using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target = null;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(target.transform.position.x, target.transform.position.y + 30.0f, target.transform.position.z + -30.0f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = target.transform.position;
	    transform.LookAt(target.transform.position);

    }
}
