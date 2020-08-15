using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemySpawner : MonoBehaviour
{

    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField]private int _startingWave = 0;
    [SerializeField] private bool looping = false;
    
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawAllWaves());
        } while (looping);
    }

    private IEnumerator SpawAllWaves()
    {
        for (var wave = _startingWave; wave < _waveConfigs.Count; wave++)
        {
            var currentWave = _waveConfigs[wave];

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        
        for (var enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), 
                waveConfig.GetWaypoints()[0].transform.position, 
                Quaternion.identity);
            
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }
    
}
