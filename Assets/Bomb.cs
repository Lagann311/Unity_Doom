using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float Delay;
    public float ExplosionRadius;
    public float ExplosionForce;
    public float UpForceModifier;
    public LayerMask InteractionLayer;
    public GameObject ExplosionPrefab;

    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Explode), Delay);
    }

    // Update is called once per frame
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius, InteractionLayer);
        foreach (Collider collider in hitColliders)
        {
            Rigidbody r = collider.GetComponent<Rigidbody>();
            r.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, UpForceModifier);
        }
        gameObject.GetComponent<AudioSource>().Play();
        explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        Invoke(nameof(Kill), 3);
    }

    void Kill()
    {
        Destroy(explosion);
        Destroy(gameObject);
    }
}
