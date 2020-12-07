using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hit;
    public GameObject muzzle;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType().ToString() != "UnityEngine.SphereCollider")
        {
            Debug.Log(other);
            // other.contactOffset
            // }
            // private void OnCollisionEnter(Collision other)
            // {
            // ContactPoint c = other.contacts[0];
            // Quaternion r = Quaternion.FromToRotation(Vector3.up, c.normal);
            // Vector3 p = c.point;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            var h = Instantiate(hit, transform.position, transform.rotation);
            var psHit = h.GetComponent<ParticleSystem>();

            if (psHit != null) Destroy(h, psHit.main.duration);
            else
            {
                var psChid = h.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(h, psChid.main.duration);
            }

            Destroy(gameObject);
        }
    }
}
