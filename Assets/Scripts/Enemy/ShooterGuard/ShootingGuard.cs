using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGuard : StateManager
{
    [SerializeField] private float playerInView;
    [SerializeField] private float playerInRange;
    [SerializeField] private float speed;
    [SerializeField] private EnemyGun gunPrefab;
    private Player player;
    private float playerDistance;
    private EnemyGun gun;

    public void Start()
    {
        player = GameManager.Instance.player;
        CurrentState = new IdleGuardState(this);
        CurrentState.Start();
        
    }

    public bool isPlayerNearby()
    {
        playerDistance = player.transform.position.x - transform.position.x;
        print("Checking if player is nearby");
        return (Mathf.Abs(playerDistance) < playerInView);
    }

    public bool isNextToPlayer()
    {
        playerDistance = player.transform.position.x - transform.position.x;
        return (Mathf.Abs(playerDistance) < playerInRange);
    }

    public void moveToPlayer()
    {
        playerDistance = player.transform.position.x - transform.position.x;
        float direction = Mathf.Sign(playerDistance);
        transform.Translate(Vector3.right * (direction * speed * Time.deltaTime));
    }

    public void SpawnGun()
    {
        gun = Instantiate(gunPrefab, transform.position, Quaternion.identity);
    }

    public void Shoot()
    {
        gun.Shoot();
    }
}
