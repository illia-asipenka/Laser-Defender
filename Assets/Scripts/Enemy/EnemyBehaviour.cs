using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _hp = 150;
    [SerializeField]private int _pointsForDefeating = 50;
    [SerializeField] private GameObject _explosion;


    [Header("Shooting")]
    private float _shotCounter;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shotSpeed = 1f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _appearingSound;
    [SerializeField] [Range(0, 1)] private float _soundsVolume = 0.75f;
    

    public float shotPerSecond = 0.5f;


    private void Start()
    {
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }


    private void Update()
    {
        CountDownAndShoot();
        
        float probability = Time.deltaTime * shotPerSecond;

        if (Random.value < probability)
        {
            Fire();
        }
    }

    private void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;

        if (_shotCounter <= 0)
        {
            Fire();
            _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        
        if (!damageDealer)
            return;
        
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _hp -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (_hp <= 0)
        {
            Die();
        }
    }

    void Fire()
    {
        GameObject shot = Instantiate(
            projectile, 
            transform.position /*+ new Vector3(0, -0.5f, 0)*/, 
            Quaternion.identity) as GameObject;
        
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);
        
        AudioSource.PlayClipAtPoint(_shotSound, transform.position);
    }

    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(_appearingSound, transform.position);
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddScore(_pointsForDefeating);
        
        ScoreKeeper.AddPoints(_pointsForDefeating);
        
        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position, _soundsVolume);
    }

}
