using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerControler : MonoBehaviour
    {
        // �ʵ�
        #region Variables
        // �ܼ� ����
        [SerializeField] private float walkSpeed = 4f;        // �ȱ� �ӵ�

        // ���� ����
        private Vector2 inputMove;

        // ������Ʈ
        private Rigidbody2D rb2D;
        #endregion

        // ������ ����Ŭ
        #region Life cycle
        private void FixedUpdate()
        {
            rb2D.velocity = new(inputMove.x * walkSpeed, rb2D.velocity.y);
        }

        private void Awake()
        {
            // ������Ʈ ����
            rb2D = GetComponent<Rigidbody2D>();
        }
        #endregion

        // �̺�Ʈ �ڵ鷯
        #region Event Handlers
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            Debug.Log(inputMove);
        }
        #endregion

        // �޼���
        #region Methods
        #endregion
    }
}