using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Hand;
    public GameObject BombPrefab;
    public float JumpForce;
    public float ThrowForce;
    public LayerMask InteractionLayer;
    public Camera Cam;
    public float MaxDistance;
    public float GroundDistance = 5.5f;

    [Header("Dash Settings")]
    public float dashForce = 10f;
    public float dashDuration = 2f;
    public float dashCooldown = 2f;

    private CharacterController controller;
    private GameObject objInHand;
    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PickUpObject     (KeyCode.Mouse1);
        ThrowBomb        (KeyCode.Mouse0);
        HandleDash();
        Dash             (KeyCode.F);

    }

    private void HandleDash()
    {
        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                cooldownTimer = dashCooldown;
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //    onGround = true;
        //else
        //    onGround = false;
    }


    #region User Actions

    private void Dash(KeyCode key)
    {
        if (!Input.GetKeyDown(key)) return;
        StartCoroutine(DashCoroutine());

        IEnumerator DashCoroutine()
        {
            float startTime = Time.time; // need to remember this to know how long to dash
            while (Time.time < startTime + dashDuration)
            {
                controller.Move(transform.forward * dashForce * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void ThrowBomb(KeyCode key)
    {
        if (!Input.GetKeyDown(key)) return;

        if (objInHand is null)
        {
            GameObject bomb = Instantiate(BombPrefab, Hand.transform.position, Quaternion.identity);
            Rigidbody rigidBody = bomb.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * ThrowForce);
        }
    }

    private void PickUpObject(KeyCode key)
    {
        if (!Input.GetKey(key)) return;

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

    #endregion


}
