using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 bulletPosition = cameraTransform.position + cameraTransform.forward * 2f;

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, cameraTransform.rotation);
            Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
            rigidBody.AddForce(cameraTransform.forward * bulletSpeed);

            //bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
    }
}
