using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : Waypoint
{
    [Header("Enemies")]
    [SerializeField] GameObject enemyHolder;

    EnemyController[] enemies;
    int killedEnemies = 0;

    protected override void Start()
    {
        base.Start();
        InitializeEnemies();
    }

    void OnDisable()
    {
        foreach (EnemyController enemy in enemies)
        {
            if (enemy != null)
                enemy.OnKilled -= HandleEnemyKilled;
        }
    }

    void InitializeEnemies()
    {
        enemies = enemyHolder.GetComponentsInChildren<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.OnKilled += HandleEnemyKilled;
        }
        if (enemies.Length == 0)
            Complete();
    }

    void HandleEnemyKilled(EnemyController enemy)
    {
        killedEnemies++;
        if (killedEnemies > enemies.Length - 1)
        {
            // Complete the waypoint when all enemies are dead
            Complete();
        }
    }
}
