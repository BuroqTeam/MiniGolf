using Sirenix.OdinInspector;
using System;

namespace Learn_OdinInspector
{
    /// <summary>
    /// Path ko'rsatayotgan folderdagi prefablarni bitta listga yig'adi. 
    /// </summary>
    [IncludeMyAttributes]
    [AssetList(Path = "LearnAsset/OdinInspector/Prefabs")]
    public class PreFabAssetList : Attribute
    {

    }
}
