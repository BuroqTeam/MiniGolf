using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;


public class Seril : SerializedMonoBehaviour
{
    [NonSerialized, OdinSerialize]
    public NodeB Node;
   
    
}

[Serializable]
public class NodeA
{
    public NodeB nodeB;
}

[Serializable]
public class NodeB : NodeA
{
    public int id;
}
