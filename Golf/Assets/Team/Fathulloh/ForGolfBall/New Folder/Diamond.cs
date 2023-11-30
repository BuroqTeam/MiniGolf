using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Diamond : MonoBehaviour
    {
        public GameEvent DiamondSO;
        public GameObject DiamondParticle;

        bool _isActive = true;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive)
            {
                DiamondAction();
            }
        }


        void DiamondAction()
        {
            _isActive = false;
            //Instantiate(DiamondParticle);
            DiamondSO.Raise();            
            gameObject.SetActive(false);
            Debug.Log("Diamond collision");
            
        }


    }
}
