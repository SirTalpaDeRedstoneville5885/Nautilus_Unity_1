using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private Rigidbody2D rb2d; 

    [SerializeField] Transform Groundchek;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    public bool  isGrounded;
    public int CJ=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = 5;
        JumpForce = 5;
    }
    void Update(){

        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }else if (Input.GetButtonDown("Jump") && isGrounded == false && CJ < 1)
        {
            Jump();
            CJ++;
        }
        if (isGrounded) CJ=0;
    }
    // Update is called once every step (for physics)
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        // Speed ++;
        rb2d.linearVelocity = new Vector2(horizontalMovement*Speed, rb2d.linearVelocity.y);
    }
    public void Jump()
    {
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
}
