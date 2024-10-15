using UnityEngine;

namespace My2D
{
    // 바닥이나 벽면을 체크하기 위한 클래스
    public class TouchingDirections : MonoBehaviour
    {
        // 필드
        #region Variables
        private bool isGrounded;
        private bool isCeiling;
        private bool isWall;

        private Vector2 WalkDirection => (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

        private Animator animator;
        private CapsuleCollider2D touchingCollider;
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.2f;

        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];
        #endregion

        // 속성
        #region Properties
        public bool IsGrounded
        {
            get { return isGrounded; }
            private set
            {
                isGrounded = value;
                animator.SetBool(AnimationString.isGrounded, isGrounded);
            }
        }

        public bool IsCeiling
        {
            get { return isCeiling; }
            private set
            {
                isCeiling = value;
                animator.SetBool(AnimationString.isCeiling, isCeiling);
            }
        }

        public bool IsWall
        {
            get { return isWall; }
            private set
            {
                isWall = value;
            }
        }
        #endregion

        // 라이프 사이클
        #region Life Cycle
        void FixedUpdate()
        {
            IsGrounded = (touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0);
            IsCeiling = (touchingCollider.Cast(Vector2.up, contactFilter, groundHits, ceilingDistance) > 0);
            IsWall = touchingCollider.Cast(WalkDirection, contactFilter, wallHits, wallDistance) > 0;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            touchingCollider = GetComponent<CapsuleCollider2D>();
        }
        #endregion
        }
}