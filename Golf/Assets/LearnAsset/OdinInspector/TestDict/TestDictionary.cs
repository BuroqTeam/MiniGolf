using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Learn_OdinInspector
{
    /// <summary>
    /// Dictionary and Matrix
    /// </summary>
    public class TestDictionary : SerializedMonoBehaviour
    {
        public int[] SimpleNumbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        public string[] RomanNumbers = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
                
        public Dictionary<int, string> DNumbers = new Dictionary<int, string>();

        public int[,] Matrix2D = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        //[TableMatrix(HorizontalTitle = "TextureMatrix", SquareCells = true)]
        //public Texture[,] TextureMatrix = new Texture[4, 4];


        //[TableMatrix(HorizontalTitle = "SpriteMatrix", SquareCells = true)]
        //public Sprite[,] SpriteMatrix = new Sprite[2, 3];


        void Start()
        {
            InitialTasks();
        }


        [Button("Initial Task"), GUIColor(0.15f, 0.52f, 0.75f)]
        void InitialTasks()
        {
            DNumbers = MakeDictionary(SimpleNumbers, RomanNumbers);

            DebugMatrix();
        }


        Dictionary<int, string> MakeDictionary(int[] numbers1, string[] numbers2)
        {
            Dictionary<int, string> newDictionary = new Dictionary<int, string>();

            for (int i = 0; i < numbers1.Length; i++)
            {
                newDictionary.Add(numbers1[i], numbers2[i]);
            }

            return newDictionary;
        }


        
        void DebugMatrix()
        {
            
            for (int i = 0; i < Matrix2D.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix2D.GetLength(1); j++)
                {
                    //Matrix2D[i,j] = num;
                    //Debug.Log("i, j = " + Matrix2D[i, j]);
                    
                }
            }
        }


    }
}

