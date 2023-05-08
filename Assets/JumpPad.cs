using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce = 35f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("TWO");
                // Calculate the launch velocity based on the jump pad's up direction
                Vector3 launchVelocity = transform.up * launchForce;

                // Apply the launch velocity to the rigidbody
                rb.AddForce(launchVelocity, ForceMode.Impulse);
            }
        }
    }

}
