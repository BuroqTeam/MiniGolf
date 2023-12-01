using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GolfBall_Smooth
{
    public class FinishFlag : MonoBehaviour
    {
        public GameEvent FinishSO;
        public GameObject FinishParticle;
                
        private bool _isActive = true;
        public UnityEvent LoadSceneEvent;
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isActive)
            {
                StartCoroutine(FinishAction());
                other.gameObject.GetComponent<BallPhysics>().FinishChangeBallPhysics();
            }
        }


        IEnumerator FinishAction()
        {
            _isActive = false;
            FinishParticle.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            FinishSO.Raise();
            
            yield return new WaitForSeconds(1.6f);
            LoadSceneEvent.Invoke();
            Debug.Log("Finish Action is work.");
        }


        /*
            //GameObject particleObj = Instantiate(FinishParticle, _parentOfParticle.transform);
            //particleObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);            

            ParticleSystem particleSystem = FinishParticle.GetComponent<ParticleSystem>();
            var mainModule = particleSystem.main;
            mainModule.startSize = 0.25f;

            Instantiate(particleSystem, _parentOfParticle.transform);
            yield return new WaitForSeconds(3f);

            FinishSO.Raise();
            Debug.Log("Finish Action is work.");
        */
    }
}
