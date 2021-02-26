using UnityEngine;
using UnityEngine.UI;

namespace NeuroDerby.UI
{
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField] 
        private Text numText;
        [SerializeField] 
        private Text nameText;
        [SerializeField] 
        private Text scoreText;

        public void Init(int num, string playerName, double score)
        {
            numText.text = num.ToString();
            nameText.text = playerName;
            scoreText.text = score.ToString("N0");
        }
    }
}