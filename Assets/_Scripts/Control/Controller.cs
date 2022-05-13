using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Aircraft.Managers;

namespace Aircraft.Control


    // scriptible obje ile uçağa has tüm değerleri variable olarak al uçak ivmesi, dönüş hızı manevra kabiliyeti vs hepsini ordan alsın; birden fazla uçak yap


{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float accelerationMultiplier;

        [SerializeField] private Slider accelerator;

        [SerializeField] private ParticleSystem leftEngineParticle;
        [SerializeField] private ParticleSystem rightEngineParticle;

        private ParticleSystem.MainModule _leftEngine;
        private ParticleSystem.MainModule _rightEngine;

        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Vector3 _rotateVector;
        private Vector3 _forwardSpeed;

        private bool _isInSafeZone = true;

        private float _throttle;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<PlayerInput>();

            _leftEngine = leftEngineParticle.main;
            _rightEngine = rightEngineParticle.main;

        }

        private void Update()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            AssignPositionRotation();
        }
        private void FixedUpdate()
        {
            if (GameManager.instance.CurrentState != GameState.GameStarted) return;
            SetControl();
        }

        private void SetControl()
        {
            //ivme kazanma ve kaybetme değerleri için branch at

            _leftEngine.startLifetime = accelerator.value;
            _rightEngine.startLifetime = accelerator.value;

            _throttle = Mathf.Lerp(_throttle, accelerator.value, Time.deltaTime * 0.2f);

            _forwardSpeed = _throttle * accelerationMultiplier * Time.deltaTime * transform.forward;
            _rigidbody.drag = accelerator.value * 5;
            _rotateVector = _input.actions["Rotate"].ReadValue<Vector2>();
        }

        private void AssignPositionRotation()
        {
            _rigidbody.AddRelativeTorque(new Vector3(-_rotateVector.y, _rotateVector.x, _rotateVector.x));
            if (_rotateVector == Vector3.zero)
            {
                _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(transform.forward, Vector3.up), 100 * Time.deltaTime * 1));
            }

            _rigidbody.MovePosition(transform.position + _forwardSpeed);

        }
        private void OnCollisionEnter(Collision collision)
        {
            //if (!collision.gameObject.CompareTag("Player")) return;
            if (_isInSafeZone && GameManager.instance.AllObjectivesComplete)
            {
                if(accelerator.value > 0.4f)
                {
                    CrashLanding();
                }
                else
                {
                    PerfectLanding();
                }

            }
            if (!_isInSafeZone)
            {
                Crash();
            }
        }
        private void CrashLanding()
        {
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0;
            Debug.Log("YOU WON. CRASH LANDING; DEDUCT POINTS");
            GameManager.instance.ChangeState(GameState.GameWon);
        }
        private void PerfectLanding()
        {
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0;
            GameManager.instance.ChangeState(GameState.GameWon);
            Debug.Log("AMAZING, MISSION COMPLETE! THAT RIGHT THERE IS WHY YOU'RE THE BEST BOSS. ONE AND ONLY.");
        }
        private void Crash()
        {
            _rigidbody.drag = 0;
            _rigidbody.angularDrag = 0;
            GameManager.instance.ChangeState(GameState.GameLost);
        }
        public void IsItInSafeZone(bool inZone)
        {
            _isInSafeZone = inZone;
        }
    }
}
