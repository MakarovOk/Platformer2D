using TMPro;
using UnityEngine;

namespace CoinCollect
{
    public class CoinScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinScore;
        private int _countCollectedCoins = 0;

        public void IncreaseScore()
        {
            _countCollectedCoins++;
            UpdateTextScore();
        }

        private void UpdateTextScore() => _coinScore.text = _countCollectedCoins.ToString();
    }
}