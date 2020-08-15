using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHP : MonoBehaviour
{
    [SerializeField] private float _shotHP;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        
        if (!damageDealer)
            return;
        
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _shotHP -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (_shotHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
