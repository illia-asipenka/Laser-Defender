using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float padding = 1f;
    [SerializeField] private float _hp = 200;

    [Header("Projectile")]
    [SerializeField] private float projectileFiringPeriod = 0.3f;
    [SerializeField] private float _maxHp = 200;
    
    
    [SerializeField]private AudioClip _shotSound;
    [SerializeField]private AudioClip _deathSound;
    [SerializeField][Range(0, 1)]private float _soundsVolume = 0.75f;


    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    public float shotSpeed;
    public float fireRate = 0.2f;
    public GameObject projectile;
    private Coroutine _firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //float distance = transform.position.z - Camera.main.transform.position.z;
        SetUpMoveBoundaries();
        
        ScoreKeeper.playerFinalPoints = 0;
        
        StartCoroutine(FireContinuosly());

    }
    
    void Update()
    {
        Move();
        
        //Fire();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate); 
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }*/
    }

    private void SetUpMoveBoundaries()
    {
        var gameCamera = Camera.main;
        
        var leftMost = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var rightMost = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
        yMin = leftMost.y + padding;
        yMax = rightMost.y - padding;
    }

    
    

    // Update is called once per frame
    

    private void Move()
    {
        //var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        //var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
        
        var position = transform.position;
        
        //var newPosX = Mathf.Clamp(position.x + deltaX, xMin, xMax);
        //var newPosY = Mathf.Clamp(position.y + deltaY, yMin, yMax);
        
        var newPosX = Mathf.Clamp(position.x, xMin, xMax);
        var newPosY = Mathf.Clamp(position.y, yMin, yMax);

        position = new Vector3(newPosX, newPosY);

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

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
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(FireContinuosly());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
    }

    public float GetHpForUI()
    {
        var amount = _hp / _maxHp;
        return amount;
    }
    
    IEnumerator FireContinuosly()
    {
        while (true)
        {
            var shot = Instantiate(projectile, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
            AudioSource.PlayClipAtPoint(_shotSound, Camera.main.transform.position, _soundsVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }
    }
    
    void Die()
    {
        var levelManager = FindObjectOfType<LevelManager>();
        Destroy(gameObject);       
        AudioSource.PlayClipAtPoint(_deathSound, Camera.main.transform.position, _soundsVolume);
        levelManager.LoadGameOver();
        ScoreKeeper.playerFinalPoints = ScoreKeeper.playerPoints;
    }
}
