using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGolf
{
    [RequireComponent(typeof(AudioSource))]
    public class CollisionTrigger : MonoBehaviour
    {

        AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();


        }



    }

}

