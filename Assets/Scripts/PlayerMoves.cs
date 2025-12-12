using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
public class PlayerMoves : MonoBehaviour
{
    public static PlayerMoves Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] GameObject PlayerObj, PlayerBody, ali;
    [SerializeField] public Rigidbody2D rb2d;
    [SerializeField] Transform Groundchek;
    [SerializeField] Transform[] WallCheck;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] public Animator Anim, aliAnim;
    public static bool isGrounded, isWalled, isSlimed = false;
    bool facingRight;
    public static int JumpMax = 1;
    int CJ = 0;
    float horizontalMovement, multiplier = 1f;


    void Update()
    {
        // controlla se il player è sotto una certa altezza
        if (transform.position.y <= -10f)
        {
            Anim.SetTrigger("LostLife");
            transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
        }
        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask);
        foreach (Transform t in WallCheck)
        {
            isWalled = Physics2D.OverlapCircle(t.position, GroundDistance, groundMask);
            if (isWalled) { break; }
        }
        Anim.SetBool("isGrounded", isGrounded);
        if (Input.GetButtonDown("Jump") && (isGrounded || isWalled))
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && (!isGrounded || isWalled) && CJ < JumpMax)
        {
            Jump();
            CJ++;
            if (JumpMax > 1 && CJ > 1)
                StartCoroutine(ExtraJumpAnimation());
        }
        if (isGrounded) CJ = 0;
        horizontalMovement = Input.GetAxis("Horizontal");
        facingRight = (horizontalMovement > 0) ? true : false;
        if (horizontalMovement != 0)
        {
            if (isGrounded && !AudioManager.Instance.AudioList[3].isPlaying)
            {
                AudioManager.Instance.AudioList[3].pitch = Random.Range(1f, 1.2f);
                AudioManager.Instance.AudioList[3].volume = Random.Range(0.8f, 1.5f);
                AudioManager.Instance.AudioList[3].Play();
            }
            PlayerBody.transform.localRotation = (facingRight) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            Anim.SetBool("IsWalking", true);
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }
        if (GameManager.Monete >= 27)
        {
            PlayerObj.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            multiplier = 1.5f;
            rb2d.gravityScale = 1f;
        }
        if (GameManager.Monete >= 54)
        {
            PlayerObj.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
            multiplier *= 2f;
            rb2d.gravityScale = 1.5f;
        }
        if (GameManager.Monete >= 81)
        {
            PlayerObj.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
            multiplier *= 3f;
            rb2d.gravityScale = 2f;
        }
    }
    // Update is called once every step (for physics)
    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(horizontalMovement * Speed * multiplier, rb2d.linearVelocity.y);
    }
    void Start()
    {
        ali.SetActive(false);
    }
    IEnumerator ExtraJumpAnimation()
    {
        ali.SetActive(true);
        aliAnim.SetTrigger("JumpExtra");
        yield return new WaitForSeconds(aliAnim.GetCurrentAnimatorStateInfo(0).length); // aspetta la durata dell'animazione
        ali.SetActive(false);
    }

    public void Jump()
    {
        Anim.SetTrigger("Jumping");
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce * multiplier, ForceMode2D.Impulse);
        AudioManager.Instance.AudioList[2].Play();
    }
}
//https://www.smbgames.be/super-mario-brothers.php
