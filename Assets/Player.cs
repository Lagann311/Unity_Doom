using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject BoxPrefab;
    public GameObject Hand;
    public GameObject BallPrefab;
    public GameObject BombPrefab;
    public float ThrowForce;
    public LayerMask InteractionLayer;
    public Camera Cam;
    public float MaxDistance;

    private GameObject objInHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 1F)
            {
                Time.timeScale = 0.5F;
            }
            else
            {
                Time.timeScale = 1F;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(BoxPrefab, Hand.transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject ball = Instantiate(BallPrefab, Hand.transform.position, Quaternion.identity);
            Rigidbody rigidBody = ball.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * ThrowForce);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Ray ray = new Ray(Cam.transform.position, Cam.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(MaxDistance));


            if (objInHand is null)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, MaxDistance, InteractionLayer))
                {
                    objInHand = hit.transform.gameObject;
                    objInHand.transform.position = Hand.transform.position;
                    objInHand.GetComponent<Rigidbody>().isKinematic = true;
                    objInHand.transform.parent = Hand.transform;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objInHand is not null)
            {
                objInHand.transform.parent = null;
                objInHand.GetComponent<Rigidbody>().isKinematic = false;
                objInHand.GetComponent<Rigidbody>().AddForce(Cam.transform.forward * ThrowForce);
                objInHand = null;
            }
            else
            {
                GameObject bomb = Instantiate(BombPrefab, Hand.transform.position, Quaternion.identity);
                Rigidbody rigidBody = bomb.GetComponent<Rigidbody>();
                rigidBody.AddForce(transform.forward * ThrowForce);
            }
        }
    }
}
