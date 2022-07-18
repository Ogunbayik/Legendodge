using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public JoystickController joystickController;
    public Rigidbody playerRb;
    public int currentHealth;

    [SerializeField] private float _speed;
    [SerializeField] private int _maximumHealth;
    [SerializeField] private int _bulletDamage;
    
    void Start()
    {
        currentHealth = _maximumHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Dead();
    }

    private void Movement()
    {
        if (joystickController.joystickVec.y != 0)
        {
            playerRb.velocity = new Vector3(joystickController.joystickVec.x * _speed, 0, joystickController.joystickVec.y * _speed);
        }
        else
        {
            playerRb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(_bulletDamage);
            Destroy(other.gameObject);
            Debug.Log(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Dead()
    {
        if(currentHealth <= 0)
        {
            GameManager.Instance.currentState = GameManager.GameStates.GameOver;
            Destroy(gameObject);
        }
    }
}
