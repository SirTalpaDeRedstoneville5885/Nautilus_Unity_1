using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] Transform Groundchek;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float horizontalMovement;
    [SerializeField] Animator Anim;
    [SerializeField] GameObject PlayerBody;
    bool facingRight;

    int CJ = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = 5;
        JumpForce = 5;
    }
    void Update()
    {

        // controlla se il player è sotto una certa altezza
        if (transform.position.y <= -10f)
        {
            transform.position= Vector3.zero;
            GameManager.Vite--;
        }

        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask);
        Anim.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && isGrounded == false && CJ < 1)
        {
            Jump();
            CJ++;
        }
        if (isGrounded) CJ = 0;

        horizontalMovement = Input.GetAxis("Horizontal");
        facingRight = (horizontalMovement >= 0.01) ? true : false ;
        
        if (horizontalMovement != 0)
        {
            Anim.SetBool("IsWalking", true) ;
            PlayerBody.transform.localRotation = (facingRight)? Quaternion.Euler(0,0,0) : Quaternion.Euler(0,180,0);
        }
        else
        {
            Anim.SetBool("IsWalking", false) ;
        }

    }
    // Update is called once every step (for physics)
    void FixedUpdate()
    {
        // Speed ++;
        rb2d.linearVelocity = new Vector2(horizontalMovement * Speed, rb2d.linearVelocity.y);
    }
    public void Jump()
    {
        Anim.SetTrigger("Jumping");
    
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
    
}
