using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGolf
{
    public class OnTrigger : MonoBehaviour
    {
        public GameEvent CoinSO;

        private Vector3 previousPosition;

        private void Start()
        {
            previousPosition = transform.position;
        }

        private void Update()
        {
            Vector3 direction = transform.position - previousPosition;
            float distance = direction.magnitude;

            RaycastHit hit;
            if (Physics.Raycast(previousPosition, direction, out hit, distance))
            {
                // Check if the ray hit an object you want to interact with.
                // Handle the collision here.
                if (hit.collider.gameObject.CompareTag("Coin"))
                {
                    CoinSO.Raise();
                    hit.collider.gameObject.SetActive(false);
                }
            }

            previousPosition = transform.position;
        }
    }

}

