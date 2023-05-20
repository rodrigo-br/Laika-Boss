using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float deltaTimeBeetweenWaves = 0.5f;
    WaveConfigSO currentWave;
    public WaveConfigSO CurrentWave => currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (true)
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.StartingWaypoint.position,
                                Quaternion.identity,
                                this.transform);
                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
            timeBetweenWaves = Mathf.Clamp(timeBetweenWaves - deltaTimeBeetweenWaves, 0f , float.MaxValue);
        }
    }
}
