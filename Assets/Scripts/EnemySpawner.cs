using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IDimensionTraveler
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float deltaTimeBeetweenWaves = 0.5f;
    [SerializeField] bool isMainDimension = false;
    [SerializeField] int idTracker;
    [SerializeField] float delayBeforeStart = 0;
    public int IdTracker { get { return idTracker; } }
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
        yield return new WaitForSeconds(delayBeforeStart);
        while (true)
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    yield return new WaitUntil(() => dimensionManager.mainDimension == isMainDimension);
                    GameObject instance = Instantiate(currentWave.GetEnemyPrefab(i),
                                currentWave.StartingWaypoint.position,
                                Quaternion.identity,
                                this.transform);
                    instance.GetComponent<PathFinder>().IdTracker = idTracker;
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            timeBetweenWaves = Mathf.Clamp(timeBetweenWaves - deltaTimeBeetweenWaves, 0f , float.MaxValue);
        }
    }



    public void DimensionChecker()
    {
        //
    }
}
