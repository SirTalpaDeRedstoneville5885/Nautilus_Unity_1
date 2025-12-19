using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private float Speed, JumpForce;
    [SerializeField] GameObject ali;
    [SerializeField] Transform Groundchek, WallCheck;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    Animator aliAnim;
    public static Animator Anim;
    [SerializeField] Rigidbody2D rb2d;
    public static bool isSlimed = false, isDead = false;
    bool facingRight, isWalled, isGrounded, deadboll = false;
    public static int JumpMax = 1;
    int CJ = 0;
    float horizontalMovement, Multiplier = 1f, FootstepVolume, WallOffset, Timer = 1.5f;
    void Awake()
    {
        JumpMax = 1;
    }
    void Start()
    {
        Anim = SpriteManager.ActiveSprite.GetComponent<Animator>();
        ali.SetActive(false);
        aliAnim = ali.GetComponent<Animator>();
        Anim = SpriteManager.ActiveSprite.GetComponent<Animator>();
        if (SpriteManager.Instance.SpriteIndex == 1)
        {
            isSlimed = true;
            rb2d.sharedMaterial = null;
        }
        WallOffset = .4f;
    }
    void Update()
    {
        facingRight = (horizontalMovement > 0) ? true : false; // in base a se la veloità di movimento del player e' positiva, setta il bool
        if (transform.position.y <= -20f)
        // controlla se il player e' sotto una certa altezza, e in caso lo uccide, andando a chiamare una sola volta l'animazione di morte
        {
            transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            NonMuoversi();
            if (deadboll)
            {
                Anim.SetTrigger("LostLife");
                deadboll = false;
            }
        }
        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask); // crea un cerchio di raggio GroundDistanca con centro Groundcheck.position e controlla se si interseca con un layer del tipo GroundMask
        isWalled = Physics2D.OverlapCircle(WallCheck.position, GroundDistance, groundMask); // come sopra, ma con wallcheck.position come come centro
        Anim.SetBool("isGrounded", isGrounded); // Resetta il bool dell'animator che serve a mandarlo in idle
        Anim.SetBool("Walled", (isSlimed && isWalled)); // avvia l'animazione di stare al muro solo se e' sia attaccato a un muro che isSlimed
        if (Input.GetButtonDown("Jump") && (isGrounded || (isSlimed && isWalled)))
        // salta se e' a terra o e' sia attaccato al muro che isSlimed
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && CJ < JumpMax)
        {
            // se si salta, non e' a terra e il numero di salti e' inferiore al JumpMax, salta di nuovo
            // ma se salti più di quanto viene inizializzato il JumpMax, avvia l'animazione di saltoExtra 
            Jump();
            CJ++;
            if (JumpMax > 1 && CJ > 1)
            {
                StartCoroutine(ExtraJumpAnimation());
            }
        }
        if (isGrounded || (isSlimed && isWalled)) CJ = 0; // se tocchi terra o un muro con isSlimed, azzera i salti effettuati
        if (!isDead)
        // Aggiorna la direzione degli sprite del player, riproduce i passi, e controlla quando parte l'animazione di camminata e di morte
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            if (horizontalMovement != 0)
            {
                if (isGrounded && !SpriteManager.Instance.FootstepsSound.isPlaying && !isWalled)
                {
                    SpriteManager.Instance.FootstepsSound.pitch = Random.Range(1f, 1.2f);
                    SpriteManager.Instance.FootstepsSound.volume = FootstepVolume;
                    SpriteManager.Instance.FootstepsSound.Play();
                }
                if (facingRight)
                {
                    SpriteManager.ActiveSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    WallCheck.position = new Vector3(transform.position.x + WallOffset, transform.position.y, transform.position.z);
                    ali.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    SpriteManager.ActiveSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    WallCheck.position = new Vector3(transform.position.x - WallOffset, transform.position.y, transform.position.z);
                    ali.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                Anim.SetBool("IsWalking", true);
            }
            else
            {
                Anim.SetBool("IsWalking", false);
            }
        }
        // Cambia un float in base al tempo fino a quando non si azzera, e quindi lo resetta
        if (isDead) Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            isDead = false;
            Timer = 1.5f;
        }
    }
    void FixedUpdate()
    {
        // controlla se si e' morti, e in caso azzera la velocità o la rende pari al valore preso, la velocità e un moltiplicatore
        // gestito dalla funzione Cresci, che qua viene chiamata
        rb2d.linearVelocity = isDead ? NonMuoversi() : new Vector2(horizontalMovement * Speed * Multiplier, rb2d.linearVelocity.y);
        Cresci(GameManager.Monete);
    }
    /*void LateUpdate()
    {
        if (isDead) Debug.Log("Morto");
        else Debug.Log("Vivo");
    }*/
    public Vector2 NonMuoversi()
    {
        isDead = true;
        deadboll = true;
        return new Vector2(0, rb2d.linearVelocity.y);
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
        // crea uno switch che, in base all'intero dato, cambia dimensione, il moltiplicatore di velocità, la gravità, il volume dei passi
        // e se necessario la posizione di groundCheck, wallCheck e ali
        switch (EXP)
        {
            case >= 81:
                {
                    transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
                    Multiplier = 5f;
                    rb2d.gravityScale = 4f;
                    FootstepVolume = Random.Range(1.4f, 1.6f);
                    Groundchek.position = (Groundchek.position == new Vector3(Groundchek.position.x, transform.position.y - 1.62f, Groundchek.position.z)) ? new Vector3(Groundchek.position.x, Groundchek.position.y, Groundchek.position.z) : new Vector3(Groundchek.position.x, transform.position.y - 1.62f, Groundchek.position.z);
                    ali.transform.position = (ali.transform.position == new Vector3(transform.position.x - 0.666664f, transform.position.y - 0.5f, ali.transform.position.z)) ? new Vector3(transform.position.x - 0.666664f, transform.position.y - 0.5f, ali.transform.position.z) : new Vector3(ali.transform.position.x, ali.transform.position.y, ali.transform.position.z);
                    WallOffset = 1.44f;
                    Debug.Log("Max Size");
                    break;
                }
            case >= 54:
                {
                    transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
                    Multiplier = 2.25f;
                    rb2d.gravityScale = 2.5f;
                    FootstepVolume = Random.Range(1.2f, 1.4f);
                    WallOffset = 0.8f;
                    Debug.Log("Medium Size");
                    break;
                }
            case >= 27:
                {
                    transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                    Multiplier = 1.5f;
                    rb2d.gravityScale = 2f;
                    FootstepVolume = Random.Range(1f, 2f);
                    WallOffset = 0.6f;
                    Debug.Log("Mini Size");
                    break;
                }
            default:
                {
                    FootstepVolume = Random.Range(0.8f, 1.5f);
                    break;
                }
        }
    }
    public void Jump()
    {
        //azzera la forza di movimento verticale, e aggiunge una forza impulsiva
        Anim.SetTrigger("Jumping");
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce * Multiplier, ForceMode2D.Impulse);
        AudioManager.Instance.AudioList[2].Play();
    }
}
//https://www.smbgames.be/super-mario-brothers.php
