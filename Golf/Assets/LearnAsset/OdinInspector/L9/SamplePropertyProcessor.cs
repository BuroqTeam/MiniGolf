using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Learn_OdinInspector
{
    /// <summary>
    /// Help us working third party codes which we can not allow. Example:
    ///  * Reorder some property.
    ///  * Add Button.
    ///  * Set value enum.
    ///  * Remove value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SamplePropertyProcessor<T> : OdinPropertyProcessor<T> where T : SampleClass 
    {
        public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
        {
            // Reorder
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                if (propertyInfos[i].PropertyName == "Middle") 
                {
                    propertyInfos.Insert(0, propertyInfos[i]); // Middleni orderini 0 ga o'zgartiradi. 

                    // 0 orderga yangi property qo'shildi. Bir xil nomda ikta property bor shuning uchun qo'shilgan propertyning aslini o'chirish kerak bo'ladi.
                    // Yangi propert qo'shilgani uhcun i indeks dagi property 1 taga suriladi va i + 1 bo'ladi. Tepadagi kod to'g'ri ishlashi uchun yozilishi kerak.
                    propertyInfos.RemoveAt(i + 1);              
                }
            }


            // Add Button
            propertyInfos.AddDelegate("Print Hello", () => Debug.Log("Work"), new BoxGroupAttribute("injected"));


            // Add value 
            propertyInfos.AddValue("Injected Property",
                (ref SampleClass s) => s.value1 + s.value2 + s.value3,
                (ref SampleClass s, int sum) => { }, new BoxGroupAttribute("injected"));


            // Set Value enum
            propertyInfos.AddValue("Injected Enum",
                (ref SampleClass s) => (MyEnum)s.value1,
                (ref SampleClass s, MyEnum myEnum) => s.value1 = (int)myEnum,
                new EnumToggleButtonsAttribute(),
                new BoxGroupAttribute("injected"));

            
            // Remove Value
            //propertyInfos.Remove("value1");

        }
    }
}
