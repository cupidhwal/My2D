using UnityEngine;

namespace My2D
{
    public class EnemyControler : MonoBehaviour
    {
        // 필드
        #region Variables
        // 컴포넌트
        private Rigidbody2D rb2D;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        // 클래스 컴포넌트
        private TouchingDirections touchingDirections;
        private DetectionZone zone;

        // 이동 속도
        [SerializeField] private float runSpeed = 4f;
        // 이동 방향
        private Vector2 directionVector = Vector2.right;
        // 이동 상태
        public enum WalkableDirection { Left, Right }
        private WalkableDirection walkDirection = WalkableDirection.Right;

        // 공격 타겟 설정
        [SerializeField] private bool hasTarget = false;
        #endregion

        // 속성
        public WalkableDirection WalkDirection
        {
            get { return walkDirection; }
            private set { walkDirection = value; }
        }

        public bool HasTarget
        {
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.hasTarget, hasTarget);
            }
        }

        public bool CanMove
        {
            get { return !animator.GetBool(AnimationString.hasTarget); }
        }

        // 감속 계수
        private float stopRate = 0.1f;

        //라이프 사이클
        #region Life Cycle
        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            touchingDirections = GetComponent<TouchingDirections>();
            zone = GetComponentInChildren<DetectionZone>();

            animator.SetBool("isMove", true);
        }

        private void FixedUpdate()
        {
            // 지면에 있을 때 벽에 부딪히면 반전
            if (touchingDirections.IsWall && touchingDirections.IsGrounded)
                Flip();

            // 이동
            if (CanMove)
                rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);

            else rb2D.velocity = new Vector2(Mathf.Lerp(directionVector.x, 0, stopRate), rb2D.velocity.y);
        }

        private void Update()
        {
            HasTarget = zone.detectedList.Count > 0;
        }
        #endregion

        // 메서드
        #region Methods
        // 방향 전환 - 반전
        private void Flip()
        {
            if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
                spriteRenderer.flipX = false;
                runSpeed = -runSpeed;
            }

            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
                spriteRenderer.flipX = true;
                runSpeed = -runSpeed;
            }

            else
            {
                Debug.LogWarning("Error Flip Direction");
            }
        }
        #endregion
    }
}