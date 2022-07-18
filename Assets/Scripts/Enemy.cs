using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefabs;
    public Transform randomLocation;
    public Transform firePoint;
    
    [Header("Patrolling Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _startWaitTime;
    [SerializeField] private float _maximumX;
    [SerializeField] private float _minimumX;
    [SerializeField] private float _maximumZ;
    [SerializeField] private float _minimumZ;
    
    [Space]
    [Header("States Settings")]
    [SerializeField] private float _chaseDistance;
    [SerializeField] private float _attackDistance;
    [Header("Bullet Settings")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireDelay;

    private float waitTime;
    private float _distance;
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState;
    void Start()
    {
        currentState = EnemyState.Patrol;

        randomLocation.position =  new Vector3(Random.Range(_minimumX, _maximumX), 0f, Random.Range(_minimumZ, _maximumZ));
        waitTime = _startWaitTime;
        _fireDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameStates.InGame)
        {
            switch (currentState)
            {
                case EnemyState.Patrol:
                    Patrolling();
                    break;
                case EnemyState.Chase:
                    Chasing();
                    break;
                case EnemyState.Attack:
                    Attacking();
                    break;
            }
        }

        if(player != null)
        _distance = Vector3.Distance(transform.position, player.transform.position);
    }

    public void Patrolling()
    {
        var lookDirection = randomLocation.transform.position + new Vector3(0f, 1f, -1f);
        transform.position = Vector3.MoveTowards(transform.position, randomLocation.position, _speed * Time.deltaTime);
        transform.LookAt(lookDirection);

        if (Vector3.Distance(transform.position, randomLocation.position) <= 1f)
        {
            if(waitTime <= 0)
            {
                randomLocation.position = new Vector3(Random.Range(_minimumX, _maximumX), 0, Random.Range(_minimumZ, _maximumZ));
                waitTime = _startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            
        }
       
        if (_distance <= _chaseDistance)
        {
            currentState = EnemyState.Chase;
        }
    }

    public void Chasing()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
        
        if (_distance >= _chaseDistance)
        {
            currentState = EnemyState.Patrol;
        }
        else if(_distance <= _attackDistance)
        {
            currentState = EnemyState.Attack;
        }
        
    }

    public void Attacking()
    {
        var fireDirection = firePoint.transform.position - transform.position;
        transform.LookAt(player.transform.position);

        if(player == null)
        {
            Debug.Log("Player yok!");
        }

        if (_fireDelay <= 0 && player != null)
        {
            var bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullet.AddForce(fireDirection * _bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            _fireDelay = 2f;
        }
        else
        {
            _fireDelay -= Time.deltaTime;
        }
        
        if (_distance >= _attackDistance)
            currentState = EnemyState.Chase;
    }

}
