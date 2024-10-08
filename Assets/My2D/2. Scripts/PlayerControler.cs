using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerControler : MonoBehaviour
    {
        // 필드
        #region Variables
        // 단순 변수
        [SerializeField] private float walkSpeed = 4f;        // 걷기 속도

        // 복합 변수
        private Vector2 inputMove;

        // 컴포넌트
        private Rigidbody2D rb2D;
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
        }
        #endregion

        // 이벤트 핸들러
        #region Event Handlers
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            Debug.Log(inputMove);
        }
        #endregion

        // 메서드
        #region Methods
        #endregion
    }
}