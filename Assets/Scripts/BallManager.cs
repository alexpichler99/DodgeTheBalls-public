using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public int time = 0;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // Apply a force in the opposite direction with 10 times the strength
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
            rigidBody.AddForce(transform.forward * -10);
    }
}
