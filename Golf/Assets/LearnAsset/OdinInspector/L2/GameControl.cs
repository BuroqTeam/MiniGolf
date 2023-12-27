using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class GameControl : MonoBehaviour
    {   // url = https://www.youtube.com/watch?v=UXYCTHf6MKY&list=PL_HIoK0xBTK4XLJ7aaZ8uNZi7xiPy7VIu&index=23

        [BoxGroup("Game State Info")] // BoxGroup yasash va bir nechta obyektni bitta box groupga jamlash uchun ishlatiladi. 
        [EnumToggleButtons]  // Enumdagi qiymatlarni yonma yon toogle holatida chiqarib beradi
        [OnValueChanged("StateChange")]   // Qiymat o'zgarganda shu metodni chaqiruvchi attribute.
        [ShowInInspector] // Static qiymatni Inspector panelida ko'rsatadi. 
        public static GameState gameState;

        [BoxGroup("Game State Info")]
        [ShowInInspector]
        public static int turnsRemaining = 3;

        //UI elements
        [TabGroup("UI")]  // Bir xil vazifadagi obyektlarni bitta TabGroup("nomi") ostida birlashtirish.
        [SceneObjectsOnly]
        [Required]
        [InlineButton("SelectCanvas", "Select")] // InlineButton["Metod_nomi", "button_nomi"]
        public Canvas startButton;
        [TabGroup("UI")] // UI nomli TabGroup
        [SceneObjectsOnly, Required]
        [InlineButton("SelectCanvas", "Tanlash")]
        public Canvas pauseMenu;
        [TabGroup("UI")]
        [SceneObjectsOnly, Required]
        [InlineButton("SelectCanvas", "Select")]
        public Canvas HUD;

        //Background music;
        [TabGroup("Music")]
        public AudioSource musicSource;

        [Space] // shunchaki Space uchun
        [ShowInInspector]
        [ValueDropdown("musicList")]
        [TabGroup("Music")]
        [InlineButton("PlayMusic")]
        private AudioClip currentMusicClip;
        [TabGroup("Music")]
        [InlineEditor(InlineEditorModes.SmallPreview)] // Tovush chastotasini ko'rsatadi. 
        public List<AudioClip> musicList;

        //sfx
        [TabGroup("SFX")]
        public AudioSource sfxSource;
        [TabGroup("SFX")]
        [InlineButton("PlaySFX", "Test")]
        public AudioClip uiClick;
        [TabGroup("SFX")]
        [InlineButton("PlaySFX", "Test")]
        public AudioClip weaponShoot;
        [TabGroup("SFX")]
        public AudioClip weaponHit;
        [TabGroup("SFX")]
        public AudioClip enemySpawn;

        //enemies
        [TabGroup("Enemies", "Enemy Data")] // [TabGroup("TabGroup_Name", "Enemy Data")]
        [AssetsOnly] // Faqat asset bo'lsin.
        public GameObject enemyPrefab;
        [TabGroup("Enemies", "Enemy Data")] // [TabGroup("TabGroup_Name", "Enemy Data")]
        [InlineEditor(InlineEditorModes.GUIOnly)] // Inspector panelida SO datani to'liq ko'rsatish.
        [AssetsOnly] // Faqat asset bo'lsin.
        public List<EnemyData> enemyList;

        //spawn points
        [TabGroup("Enemies", "SpawnPoints")]
        [SceneObjectsOnly] // shu scenedagi obyekt bo'lsin, asset bo'lmasin. 
        public List<Transform> spawnPoints;

        private void PlaySFX(AudioClip sfx)
        {
            if (sfxSource != null && !sfxSource.isPlaying)
                sfxSource.PlayOneShot(sfx);
        }

        public void PlayMusic(AudioClip music)
        {
            if (musicSource != null && music != null)
            {
                musicSource.clip = music;
                musicSource.Play();
            }
        }

        [Button(ButtonSizes.Small)]
        [TabGroup("Enemies", "Enemy Data")] // TabGroup nomi, Tab ning nomi
        [GUIColor(0.6f, 1f, 0.6f)]
        public void SpawnRandomEnemy()
        {
            if (enemyList.Count == 0 || spawnPoints.Count == 0)
                return;

            GameObject enemyToSpawn = Instantiate(enemyPrefab);

            //inject data
            EnemyData data = enemyList[Random.Range(0, enemyList.Count)];
            enemyToSpawn.GetComponent<EnemyControl>().SetEnemyData(data);

            //set location
            enemyToSpawn.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;


        }


        public void StateChange()
        {
            switch (gameState)
            {
                case GameState.startScene:
                    break;
                case GameState.gamePlay:
                    break;
                case GameState.paused:
                    break;
                case GameState.complete:
                    break;
                default:
                    break;
            }
        }


        private void SelectCanvas(Canvas _object) // Shu obyektning datasini Inspector panelida chiqaradi. 
        {
            if (_object)
                UnityEditor.Selection.activeObject = _object.gameObject;
        }


    }


    public enum GameState
    {
        startScene,
        gamePlay,
        paused,
        complete
    }


    //internal class EnemyControl
    //{
    //    internal void SetEnemyData(EnemyData data)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

}
