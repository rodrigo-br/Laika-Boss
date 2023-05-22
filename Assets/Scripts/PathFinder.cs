using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner[] enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    Enemy enemy;
    int waypointIndex = 0;
    public int IdTracker
    {
        set
        {
            foreach (EnemySpawner es in enemySpawner)
            {
                if (es.IdTracker == value)
                {
                    waveConfig = es.CurrentWave;
                    break ;
                }
            }
            waypoints = waveConfig.GetWaypoints();
            transform.position = waypoints[waypointIndex].position;
        }
    }

    void Awake()
    {
        enemySpawner = FindObjectsOfType<EnemySpawner>();
        enemy = GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        if (enemy.isActive)
        {
            FollowPath();
        }
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
