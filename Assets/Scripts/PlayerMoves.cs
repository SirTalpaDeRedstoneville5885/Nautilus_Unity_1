using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private float Speed, JumpForce;
    [SerializeField] GameObject PlayerObj, ali;
    [SerializeField] Transform Groundchek;
    [SerializeField] Transform[] WallCheck;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    Animator aliAnim;
    public static Animator Anim;
    public Rigidbody2D rb2d;
    public static bool isWalled, isSlimed = false, isDead = false;
    bool facingRight;
    public bool isGrounded = true;
    public static int JumpMax = 1;
    int CJ = 0;
    float horizontalMovement, Multiplier = 1f, FootstepVolume;
    void Awake()
    {
        JumpMax = 1;
        Anim = SpriteManager.ActiveSprite.GetComponent<Animator>();
        aliAnim = ali.GetComponent<Animator>();
    }
    void Start()
    {
        ali.SetActive(false);
        Anim = SpriteManager.ActiveSprite.GetComponent<Animator>();
        if (SpriteManager.Instance.SpriteIndex == 1) isSlimed = true;
    }
    void Update()
    {
        facingRight = (horizontalMovement > 0) ? true : false;
        // controlla se il player è sotto una certa altezza
        if (this.transform.position.y <= -20f)
        {
            isDead = true;
            Anim.SetTrigger("LostLife");
            this.transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            StartCoroutine(NonMuoversi());
        }
        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask);
        foreach (Transform t in WallCheck)
        {
            isWalled = Physics2D.OverlapCircle(t.position, GroundDistance, groundMask);
            if (isWalled) { break; }
        }
        Anim.SetBool("isGrounded", isGrounded);
        Anim.SetBool("Walled", (isSlimed && isWalled));
        if (isWalled) SpriteManager.ActiveSprite.transform.localRotation = (facingRight) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        if (Input.GetButtonDown("Jump") && (isGrounded || (isSlimed && isWalled)))
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && (!isGrounded || (isSlimed && isWalled)) && CJ < JumpMax)
        {
            Jump();
            CJ++;
            if (JumpMax > 1 && CJ > 1)
            {
                //ali.GetComponentInParent<Transform>().position += (facingRight) ? new Vector3(-.3f, -.3f, 0) : new Vector3(.3f, .3f, 0);
                StartCoroutine(ExtraJumpAnimation());
            }
        }
        if (isGrounded || (isSlimed && isWalled)) CJ = 0;
        horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement != 0)
        {
            if (isGrounded && !SpriteManager.Instance.FootstepsSound.isPlaying)
            {
                SpriteManager.Instance.FootstepsSound.pitch = Random.Range(1f, 1.2f);
                SpriteManager.Instance.FootstepsSound.volume = FootstepVolume;
                SpriteManager.Instance.FootstepsSound.Play();
            }
            SpriteManager.ActiveSprite.transform.localRotation = (facingRight) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            Anim.SetBool("IsWalking", true);
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }

    }
    // Update is called once every step (for physics)
    void FixedUpdate()
    {
        if (!isDead)
            rb2d.linearVelocity = new Vector2(horizontalMovement * Speed * Multiplier, rb2d.linearVelocity.y);
        Cresci(GameManager.Monete);
    }
    public IEnumerator NonMuoversi()
    {
        yield return new WaitForSeconds(Anim.GetCurrentAnimatorStateInfo(0).length);
        isDead = false;
    }
    IEnumerator ExtraJumpAnimation()
    {
        ali.SetActive(true);
        aliAnim.SetTrigger("JumpExtra");
        yield return new WaitForSeconds(aliAnim.GetCurrentAnimatorStateInfo(0).length); // aspetta la durata dell'animazione
        ali.SetActive(false);
    }
    void Cresci(int EXP)
    {
        if (EXP >= 27)
        {
            PlayerObj.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            Multiplier = 1.5f;
            rb2d.gravityScale = 2f;
            FootstepVolume = Random.Range(1f, 2f);
        }
        if (EXP >= 54)
        {
            PlayerObj.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
            Multiplier *= 1.5f;
            rb2d.gravityScale = 2.5f;
            FootstepVolume = Random.Range(1.2f, 1.4f);
        }
        if (EXP >= 81)
        {
            PlayerObj.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
            Multiplier *= 1.58f;
            rb2d.gravityScale = 4f;
            FootstepVolume = Random.Range(1.4f, 1.6f);
            Groundchek.position = new Vector3(0f, this.transform.position.y - 0.4f, 0f);
        }
        else FootstepVolume = Random.Range(0.8f, 1.5f);
    }
    public void Jump()
    {
        Anim.SetTrigger("Jumping");
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce * Multiplier, ForceMode2D.Impulse);
        AudioManager.Instance.AudioList[2].Play();
    }
}
//https://www.smbgames.be/super-mario-brothers.php
