using UnityEngine;

namespace GolfBall_Smooth
{
    /// <summary>
    /// Camera ichidagi childga qo'shiladi va ball harakatsiz turganda rotate animatsiyasini ishlatuvchi script.
    /// </summary>
    public class CameraCircle : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _ball;

        private SpriteRenderer _spriteRenderer;
        private BallMovement _ballMovement;

        //public bool IsBallMoving;
        //public bool IsBallClicked;
        //public bool IsBallOut;

        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _ballMovement = _ball.GetComponent<BallMovement>();
        }

        
        void Update()
        {            
            transform.Rotate(_rotation * Time.deltaTime);

            if (!_ballMovement.IsBallMoving && !_ballMovement.IsBallClicked && !_ballMovement.IsBallOut)
            {
                _spriteRenderer.enabled = true;
                //Debug.Log("Is work");
            }
            else /*if (_ballMovement.IsBallOut || _ballMovement.IsBallMoving || _ballMovement.IsBallClicked)*/
            {
                _spriteRenderer.enabled = false;
                //Debug.Log("Is not work");
            }
        }


        //!_ball.GetComponent<BallMovement>().IsBallMoving && !_ball.GetComponent<BallMovement>().IsBallClicked

    }
}