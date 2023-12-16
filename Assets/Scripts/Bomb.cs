using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("[ Explosion Range ]"), SerializeField]
    private float _expRadius = 20f;

    [Header("[ Explosion Force ]"), SerializeField]
    private float _expforce = 500f;

    [Header("[ Explosion UpwardPower ]"), SerializeField]
    private float _upwardExpPower = 20f;

    [SerializeField]
    private GameObject _expEffect;

    [SerializeField]
    private LayerMask _enemyMask;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_expEffect, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _expRadius, _enemyMask);

        foreach(Collider coll in colliders)
        {
            var rb = coll.GetComponent<Rigidbody>();
            rb.mass = 5.0f;
            rb.AddExplosionForce(_expforce,transform.position,_expRadius, _upwardExpPower);
            Destroy(coll.gameObject, 0.5f);
        }

        Destroy(gameObject);
    }
}
