using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class PlayerActions : MonoBehaviour
{
    public GameObject BombPrefab;
    public Transform cameraTransform;
    public float ThrowForce;

    [Header("Dash Settings")]
    public float dashForce = 10f;
    public float dashDuration = 2f;
    public float dashCooldown = 2f;

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrowBomb(KeyCode.Mouse0);
        HandleDash();
        Dash(KeyCode.F);
    }

    private void ThrowBomb(KeyCode key)
    {
        if (!Input.GetKeyDown(key)) return;

        Vector3 bombPosition = cameraTransform.position + cameraTransform.forward * 2f;

        GameObject bomb = Instantiate(BombPrefab, bombPosition, cameraTransform.rotation);
        Rigidbody rigidBody = bomb.GetComponent<Rigidbody>();
        rigidBody.AddForce(cameraTransform.forward * ThrowForce);
    }

    private void Dash(KeyCode key)
    {
        if (!Input.GetKeyDown(key)) return;
        StartCoroutine(DashCoroutine());

        IEnumerator DashCoroutine()
        {
            float startTime = Time.time; // need to remember this to know how long to dash
            while (Time.time < startTime + dashDuration)
            {
                transform.Translate(cameraTransform.forward * dashForce * Time.deltaTime);
                yield return null;
            }
        }
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
}
