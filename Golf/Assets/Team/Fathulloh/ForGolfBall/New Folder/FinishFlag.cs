using System.Collections;
using UnityEngine;

namespace GolfBall_Smooth
{
    public class FinishFlag : MonoBehaviour
    {
        public GameEvent FinishSO;
        public GameObject FinishParticle;

        private Transform _currentTransform;
        private bool _isActive = true;
        [SerializeField] private GameObject _parentOfParticle;

        void Start()
        {            
            _currentTransform = gameObject.transform;
            _parentOfParticle = new GameObject("Parent Of Particle");
            _parentOfParticle.transform.position = _currentTransform.position;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_isActive)
            {
                StartCoroutine(FinishAction());
            }
        }


        IEnumerator FinishAction()
        {
            _isActive = false;
            //gameObject.GetComponent<Collider>().enabled = false;
            GameObject particleObj = Instantiate(FinishParticle, _parentOfParticle.transform);
            particleObj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            //_parentOfParticle.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            yield return new WaitForSeconds(3f);

            FinishSO.Raise();
            Debug.Log("Finish Action is work.");
        }

    }
}
