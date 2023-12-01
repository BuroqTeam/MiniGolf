using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class Diamond : MonoBehaviour
    {
        public GameEvent DiamondSO;
        private GameObject DiamondParticle_1;
        private GameObject DiamondParticle_2;
        private GameObject DiamondParticle_3;
        bool _isActive = true;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive)
            {
                DiamondParticle_1 = gameObject.transform.GetChild(0).gameObject;
                DiamondParticle_2 = gameObject.transform.GetChild(1).gameObject;
                DiamondParticle_3 = gameObject.transform.GetChild(2).gameObject;
                StartCoroutine(DiamondAction());
            }
        }


        IEnumerator DiamondAction()
        {
            _isActive = false;
            //GameObject obj = Instantiate(DiamondParticle, gameObject.transform);

            DiamondSO.Raise();
            DiamondParticle_1.SetActive(true);
            DiamondParticle_2.SetActive(true);
            DiamondParticle_3.SetActive(true);

            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(2.5f);

            gameObject.SetActive(false);
            Debug.Log("Diamond collision");
        }


    }
}
