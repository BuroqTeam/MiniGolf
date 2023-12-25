using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Learn_OdinInspector
{
    public class LearnDataTypes : MonoBehaviour
    {
        public static GameState gameState;

        public static int turnsRemaining = 3;

        //UI elements
        public Canvas startButton;
        public Canvas pauseMenu;
        public Canvas HUD;

        //Background music;
        public AudioSource musicSource;
        private AudioClip currentMusicClip;
        public List<AudioClip> musicList;

        //sfx
        public AudioSource sfxSource;
        public AudioClip uiClick;
        public AudioClip weaponShoot;
        public AudioClip weaponHit;
        public AudioClip enemySpawn;


        //enemies
        public GameObject enemyPrefab;
        public List<EnemyData> enemyList;

        //spawn points
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

        private void SelectCanvas(Canvas _object)
        {
            if (_object)
                UnityEditor.Selection.activeObject = _object.gameObject;
        }


    }
}
