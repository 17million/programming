using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    public RigidBody2D _rb;

    //Player body
    public GameObject _player;
    //Dead player body
    public GameObject _deadBody;
    //Player weapon arms
    public GameObject _weaponArms;
    //Player weapon no arms
    public GameObject _noWeaponArms;

    //Health UI
    public Text _playerHealthDisp;
    //Player health
    public float _maxHealth = 100.0f;
    //Current Health
    float _currentHealth = 100.0f;
    //Player speed value
    [Range(1.0f, 10.0f)]
    public float _playerSpeed = 10.0f;

    //is player dead
    public bool _isDead = false;
    //Player death message
    public Text _deathMessage;

    //Weapons to equip
    public List<GameObject> _weaponPrefabs;
    //Weapon parent
    public GameObject _weaponParent;
    //Player is holding weapon?
    public bool _isHoldingWeapon = false;
    //was player last holding weapon
    private bool _lastHoldVal = false;
    //Currently equipped weapon
    private int _currentWeapon = -1;
    //weapon object in hand
    public GameObject _weaponObjInHand = null;
    //player attacking
    private bool _firing = false;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb == GetComponent<RigidBody2D>();
        }

        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Update weapon display
        UpdateWeaponDisplay();

        //Update health display
        UpdateHealthDisplay();

        //Check if dead
        CheckIfDead();

        //Do attack
        if (_firing)
        {
            FireWeapon();
        }

        //Do movement
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 movementDir = new Vector2(0.0f, 0.0f);

        if (input.GetKey(KeyCode.W))
        {
            movementDir += new Vector2(0.0f, 1.0f);
        }
        if (input.GetKey(keyCode.S))
        {
            movementDir += new Vector2(0.0f, -1.0f);
        }

        if (input.GetKey(KeyCode.A))
        {
            movementDir += new Vector2(-1.0f, 0.0f);
        }
        if (input.GetKey(KeyCode.D))
        {
            movementDir += new Vector2(1.0f, 0.0f);
        }

        _rb.velocity = movementDir * (_playerSpeed);
    }

    public void ResetHealth()
    {
        //Reset health value
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Inflict damage upon player
        _currentHealth -= damage;
    }

    public void HealDamage(float heal)
    {
        //heal palyer health
        _currentHealth += heal;

        //Check if overhealed
        if (_currentHealth >= _maxHealth)
        {
            //cap health at max
            _currentHealth = _maxHealth;
        }
    }

    private void UpdateWeaponDisplay()
    {
        //if last updated doesnt match current value
        if (_lastHoldVal != _isHoldingWeapon)
        {
            //if holding a weapon, weapon arms are active
            _weaponArms.SetActive(_isHoldingWeapon);
            //if not holding a weaopon, no weapon arms are active
            _noWeaponArms.SetActive(!_isHoldingWeapon);
        }

        //Update last value
        _lastHoldVal = _isHoldingWeapon;
    }

    private void UpdateHealthDisplay()
    {
        if (_playerHealthDisp != null)
        {
            //display current player health
            _playerHealthDisp.text = _currentHealth.ToString();
        }
    }

    public void CheckIfDead()
    {
        //Check if players health is less than or equal to 0
        //And check if player is not already dead
        if (_currentHealth <= 0.0f && !_isDead)
        {
            //Set health to 0
            _currentHealth = 0.0f;
            //Die
            Die();
        }
    }

    private void Die()
    {
        //Set is Dead
        _isDead = true;
        //Disable player body
        _player.SetActive(false);

        //Enable player dead body
        _deadBody.SetActive(true);

        //Enable death message
        _deathMessage.gameObject.SetActive(true);

        //Set timescale to 0 so everything stops
        Time.timeScale = 0.0f;
    }

    private void FireWeapon()
    {
        //is there a weapon in hand
        if (_weaponObjInHand != null)
        {
            //Attack
            //_weaponObjInHand.Attack();
        }
    }
}
