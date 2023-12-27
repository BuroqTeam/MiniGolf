using System.Collections.Generic;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class GameManager_Odin : MonoBehaviour
    {
        [PropertyOnly]
        public EnemyData EnemyDataState;


        [PreFabAssetList]
        public List<GameObject> PrefabList;
        
    }
    
}


