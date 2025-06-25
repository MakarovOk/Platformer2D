using System.Collections.Generic;
using UnityEngine;

namespace CoinCollect
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxSpawedPoints = 6;
    
        [Header("Dependencies")]
        [SerializeField] private CoinScore _coinScore;
        private bool _hasSpawnPoint => _spawnPoints.Count > 0;
        private void Start() => SpawnCoins();

        private void SpawnCoins()
        {
            for (int i = 0; i < _maxSpawedPoints; i++)
            {
                if (_hasSpawnPoint)
                {
                    int index = Random.Range(0, _spawnPoints.Count);
                    Transform spawnPoint = _spawnPoints[index];
                    _spawnPoints.Remove(spawnPoint);
                    var coin = Instantiate(_coinPrefab, spawnPoint.position, spawnPoint.rotation);
                    coin.OnCoinCollected += _coinScore.IncreaseScore;
                }
            }
        }
    }
}