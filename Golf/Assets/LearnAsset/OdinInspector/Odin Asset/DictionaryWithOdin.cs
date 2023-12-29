using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryWithOdin : SerializedMonoBehaviour
{
    public int[] SimpleNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    public string[] RomanNumbers = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

    public Dictionary<int, string> OdinNumber = new Dictionary<int, string>();


    public int[,] OdinMatrix = new int[2, 2] { { 35, 45 }, { 18, 37 } };

    public int[,] OdinMatrix2 = new int[5, 6];

    [Button("Make Dictionary")]
    void MakeDictionary()
    {
        for (int i = 0; i < SimpleNumbers.Length; i++)
        {
            OdinNumber.Add(SimpleNumbers[i], RomanNumbers[i]);
        }
    }


}
