using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;

    float lifetime = 3;
    float skinwith = .1f;
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinwith, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }

    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageale damagealeObject = hit.collider.GetComponent<IDamageale>();
        if (damagealeObject != null)
        {
            damagealeObject.TakeHit(damage, hit);
        }

        GameObject.Destroy(gameObject);
    }

    void OnHitObject(Collider c)
    {
        IDamageale damagealeObject = c.GetComponent<IDamageale>();
        if (damagealeObject != null)
        {
            damagealeObject.TakeDamage(damage);
        }

        GameObject.Destroy(gameObject);
    }

}
