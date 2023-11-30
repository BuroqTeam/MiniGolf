using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class FinishFlag : MonoBehaviour
    {
        public GameEvent FinishSO;
        public GameObject FinishParticle;

        bool _isActive = true;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive)
            {
                FinishAction();

            }           
        }


        void FinishAction()
        {
            _isActive = false;
            //gameObject.GetComponent<Collider>().enabled = false;
            Instantiate(FinishParticle);
            FinishSO.Raise();

            Debug.Log("Finish Action is work.");
        }

    }
}
