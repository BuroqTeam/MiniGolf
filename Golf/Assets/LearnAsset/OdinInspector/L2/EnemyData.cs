using Sirenix.OdinInspector;
using UnityEngine;

namespace Learn_OdinInspector
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "ScriptableObjects/EnemyDataSO", order = 20)]
    [InlineEditor]
    public class EnemyData : ScriptableObject
    {
        [BoxGroup("BasicInfo")]  // Variableni BasicInfo nomli qutiga joylaydi. 
        [LabelWidth(100)]        // Inspector panelida variablega qancha joy ajratilganini belgilaydi. 
        public string enemyName;
        [BoxGroup("BasicInfo")]
        [TextArea]               // Text yozish uchun alohida joy ajratadi. 
        public string description;

        [HorizontalGroup("Game Data", 75)]
        [PreviewField(75)]
        [HideLabel]
        public GameObject enemyModel;


        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(20, 100)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public int health = 20;

        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(0.5f, 5)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;

        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(5, 30)]
        [GUIColor(1f, 0.75f, 0.5f)]
        public float detectRange = 10f;

        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(1, 10)]
        [GUIColor(0.8f, 0.2f, 0.4f)]
        public int damage = 1;

    }
}
