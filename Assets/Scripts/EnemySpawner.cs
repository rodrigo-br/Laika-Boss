using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IDimensionTraveler
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float deltaTimeBeetweenWaves = 0.5f;
    [SerializeField] bool isMainDimension = false;
    public bool IsMainDimension { get => isMainDimension ;}
    WaveConfigSO currentWave;
    public WaveConfigSO CurrentWave => currentWave;
    DimensionManager dimensionManager;

    void Awake()
    {
        dimensionManager = FindObjectOfType<DimensionManager>();
    }

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
                    while (dimensionManager.mainDimension == isMainDimension)
                    {
                        yield return new WaitForSecondsRealtime(0.2f);
                    }
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

    public void DimensionChecker()
    {

    }
}
