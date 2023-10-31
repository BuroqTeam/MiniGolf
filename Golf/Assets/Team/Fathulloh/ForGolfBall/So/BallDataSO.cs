using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Data", menuName = "ScriptableObjects/Ball Data Container")]
public class BallDataSO : ScriptableObject
{
    /// <summary>
    /// AddForce metodi chaqirilganda ko'paytirish uchun ishlatiladi.
    /// </summary>
    public float ForceMultiplier; // 50      500 drag 0.5f, mass 0.5f
    /// <summary>
    /// Eng kichik tezlik. Shu tezlikka yetgach dragging qiymati oshib boradi va ball tezroq to'xtaydi.
    /// </summary>
    public float MinimalSpeed;   // 0.12f
    /// <summary>
    /// Har bir kvadratning o'lchami. Bu UI da qancha masofa bosganini ko'rsatish uchun kerak.
    /// </summary>
    public float SizeEachCell;   // 0.25f

}
