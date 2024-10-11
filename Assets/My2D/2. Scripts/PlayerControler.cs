using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerControler : MonoBehaviour
    {
        // 필드
        #region Variables
        // 단순 변수
        private float walkSpeed = 4f;        // 걷기 속도

        // 불리언 변수
        [SerializeField] private bool isMove = false;
        [SerializeField] private bool isRun = false;
        [SerializeField] private bool isFacing = true;

        // 복합 변수
        private Vector2 inputMove;

        // 컴포넌트
        private Rigidbody2D rb2D;
        private Animator animator;
        #endregion

        // 속성
        #region Properties
        public bool IsMove
        {
            get { return isMove; }
            set
            {
                isMove = value;
                animator.SetBool(AnimationString.isMove, value);
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            set
            {
                isRun = value;
                animator.SetBool(AnimationString.isRun, value);
            }
        }

        public bool IsFacing
        {
            get { return isFacing; }
            set
            {
                //if (isFacing != value)
                    transform.localScale *= new Vector2(-1, 1);

                isFacing = value;
            }
        }
        #endregion

        // 라이프 사이클
        #region Life cycle
        private void FixedUpdate()
        {
            rb2D.velocity = new(inputMove.x * walkSpeed, rb2D.velocity.y);
        }

        private void Awake()
        {
            // 컴포넌트 참조
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        #endregion

        // 이벤트 핸들러
        #region Event Handlers
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            IsMove = inputMove != Vector2.zero;

            // 방향 전환
            SetFacingDirection(inputMove);
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            // 누르기 시작하는 순간
            if (context.started)
            {
                IsRun = true;
            }

            // 떼는 순간
            else if (context.canceled)
            {
                IsRun = false;
            }
        }
        #endregion

        // 메서드
        #region Methods
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x >= 0 && IsFacing == false)
            {
                IsFacing = true;
            }

            else if (moveInput.x < 0 && IsFacing == true)
            {
                IsFacing = false;
            }
        }
        #endregion
    }
}