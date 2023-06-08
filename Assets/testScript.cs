using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            // GetComponentInParent<Animator>().enabled = false;
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            AddForceAtAngle(800, 0);
            Invoke("StopForce", 1.37f);
        }
    }

    public void AddForceAtAngle(float force, float angle)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        transform.GetChild(0).GetComponent<Rigidbody>().AddForce(ycomponent, 0, xcomponent);
    }

    void StopForce()
    {
        transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
      

    }
}
