using Sirenix.OdinInspector;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class Grouping : MonoBehaviour
    {
        [HorizontalGroup("BaseGroup", Width = 150)]

        [Space]
        [VerticalGroup("BaseGroup/left")]
        [LabelWidth(90)]
        [HideLabel, Title("Enemy Name", Bold = false, HorizontalLine = false)]
        public string EnemyName;

        [VerticalGroup("BaseGroup/left/center")]
        [PreviewField(145)]
        [HideLabel]
        public Sprite Logo;



        [VerticalGroup("BaseGroup/right", 100)]
        [TextArea]
        public string Description;

        [HorizontalGroup("BaseGroup/right/lower")]

        [VerticalGroup("BaseGroup/right/lower/right")]
        [Range(0, 12)]
        public float Stat1;

        [VerticalGroup("BaseGroup/right/lower/right")]
        [Range(0, 12)]
        public float Stat2;

        [VerticalGroup("BaseGroup/right/lower/left")]
        [Range(0, 12)]
        public float Stat3;

        [VerticalGroup("BaseGroup/right/lower/left")]
        [Range(0, 12)]
        public float Stat4;
    }
}
