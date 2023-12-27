using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace Learn_OdinInspector
{
    /// <summary>
    /// this script responsible create new window for SO.
    /// </summary>
    public class EnemyDataEditor : OdinMenuEditorWindow
    {
        //video url = https://www.youtube.com/watch?v=erWEG-6hx7g&list=PL_HIoK0xBTK4XLJ7aaZ8uNZi7xiPy7VIu&index=20

        [MenuItem("Tools/Enemy Data")]
        private static void OpenWindow()
        {
            GetWindow<EnemyDataEditor>().Show();
        }


        private CreateNewEnemyData createNewEnemyData;


        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (createNewEnemyData != null)
                DestroyImmediate(createNewEnemyData.enemyData);
        }


        /// <summary>
        /// Barcha SO larni va ulardagi malumotlarni EnemyDataEditor nomli oynada chiqaradi. 
        /// </summary>
        /// <returns></returns>
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            createNewEnemyData = new CreateNewEnemyData();
            tree.Add("Create New", createNewEnemyData);
            tree.AddAllAssetsAtPath("Enemy Data", "Assets/LearnAsset/OdinInspector/SO", typeof(EnemyData));

            return tree;
        }


        protected override void OnBeginDrawEditors()
        {
            // gets reference to currently selected item. 
            OdinMenuTreeSelection selected = this.MenuTree.Selection;

            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                if (SirenixEditorGUI.ToolbarButton("DeleteCurrent"))
                {
                    EnemyData asset = selected.SelectedValue as EnemyData;
                    string path = AssetDatabase.GetAssetPath(asset);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }


        public class CreateNewEnemyData
        {
            public CreateNewEnemyData()
            {
                enemyData = ScriptableObject.CreateInstance<EnemyData>();
                enemyData.enemyName = "New Enemy Data";
            }


            [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
            public EnemyData enemyData;


            [Button("Add New Enemy SO")]
            private void CreateNewData()
            {
                // birinchi argument object enemyData asseti. Ikkinchi qiymat assetning joylashuvi va nomini bildiradi.  
                AssetDatabase.CreateAsset(enemyData, "Assets/LearnAsset/OdinInspector/SO/" + enemyData.enemyName + ".asset");
                AssetDatabase.SaveAssets();   // Yangi assetni saqlaydi.

                // create new instance of the SO
                enemyData = ScriptableObject.CreateInstance<EnemyData>();
                enemyData.enemyName = "New Enemy Data";
            }

        }
        


    }
}
