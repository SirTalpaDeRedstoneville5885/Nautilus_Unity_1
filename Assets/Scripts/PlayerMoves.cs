using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private float Speed, JumpForce;
    [SerializeField] Transform Groundchek, WallCheck;
    [SerializeField] float GroundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Rigidbody2D rb2d;
    public static bool isSlimed = false, isDead = false;
    bool facingRight, isWalled, isGrounded;
    public static int JumpToDo = 0;
    public static float Multiplier;
    int CJ = 0;
    [SerializeField] int JumpMax = 0;
    float horizontalMovement, FootstepVolume, WallOffset, Timer = 2f;
    AudioSource FootstepsSound;
    void Start()
    {
        // metto le local scale e il multiplier della velocià nel loro stato di default,
        // cosi ogni volta che viene caricata la scena (per esempio nel retry) sono in default
        transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        SpriteManager.Instance.ActiveSprite.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        Multiplier = 1f;
        WallOffset = .4f;
        // disattiva le ali e attiva lo sprite principale
        SpriteManager.Instance.AliBody.SetActive(false);
        SpriteManager.Instance.ActiveSprite.SetActive(true);
        // controlla che sprite e' attivo, e setta le variabili per cambiare lo stile di gameplay e i suoni dei passi 
        if (SpriteManager.Instance.ActiveSprite == SpriteManager.Instance.PlayerBody[1])
        {
            rb2d.sharedMaterial = null;
            isSlimed = true;
            FootstepsSound = AudioManager.Instance.AudioList[6];
        }
        else FootstepsSound = AudioManager.Instance.AudioList[3];
    }
    void Update()
    {
        facingRight = (horizontalMovement > 0) ? true : false; // in base a se la veloità di movimento del player e' positiva, setta il bool
        if (transform.position.y <= -20f && !isDead)
        // controlla se il player e' sotto una certa altezza, e in caso lo uccide, andando a chiamare una sola volta l'animazione di morte
        {
            isDead = true;
            transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            NonMuoversi();
            float SizeTimer = 2 * Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x + SizeTimer, transform.localScale.y + SizeTimer, transform.localScale.z + SizeTimer);
            SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetTrigger("LostLife");
        }
        isGrounded = Physics2D.OverlapCircle(Groundchek.position, GroundDistance, groundMask); // crea un cerchio di raggio GroundDistanca con centro Groundcheck.position e controlla se si interseca con un layer del tipo GroundMask
        isWalled = Physics2D.OverlapCircle(WallCheck.position, GroundDistance, groundMask); // come sopra, ma con wallcheck.position come come centro
        SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetBool("isGrounded", isGrounded); // Resetta il bool dell'animator che serve a mandarlo in idle
        SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetBool("Walled", isSlimed && isWalled); // avvia l'animazione di stare al muro solo se e' sia attaccato a un muro che isSlimed
        if (isGrounded || (isSlimed && isWalled)) CJ = 0; // se tocchi terra o un muro con isSlimed, azzera i salti effettuati
        if (!isDead)
        // Aggiorna la direzione degli sprite del player, riproduce i suoni dei passi, salta e controlla quando parte l'animazione di camminata e di morte
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump") && (isGrounded || (isSlimed && isWalled)))
            // salta se e' a terra o e' sia attaccato al muro che isSlimed
            {
                Jump();
            }
            else if (Input.GetButtonDown("Jump") && !isGrounded && CJ < JumpToDo)
            {
                // se si salta, non e' a terra e il numero di salti e' inferiore al JumpToDo, salta di nuovo
                // ma se salti più di quanto viene inizializzato il JumpToDo, avvia l'animazione di saltoExtra 
                Jump();
                CJ++;
                if (JumpToDo > JumpMax && CJ > JumpMax)
                {
                    StartCoroutine(ExtraJumpAnimation());
                }
            }
            if (horizontalMovement != 0)
            {
                SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetBool("IsWalking", true);
                if (isGrounded && !FootstepsSound.isPlaying && !isWalled)
                // Gestisce l'audio dei passi
                {
                    FootstepsSound.pitch = Random.Range(1f, 1.2f);
                    FootstepsSound.volume = FootstepVolume;
                    FootstepsSound.Play();
                }
                if (facingRight)
                {
                    SpriteManager.Instance.ActiveSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    WallCheck.position = new Vector3(transform.position.x + WallOffset, transform.position.y, transform.position.z);
                    SpriteManager.Instance.AliBody.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    SpriteManager.Instance.ActiveSprite.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    WallCheck.position = new Vector3(transform.position.x - WallOffset, transform.position.y, transform.position.z);
                    SpriteManager.Instance.AliBody.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetBool("IsWalking", false);
            }
        }
        else
        {
            Timer -= Time.deltaTime;
            // Cambia un float in base al tempo fino a quando non si azzera, e quindi lo resetta
            if (Timer < 0)
            {
                isDead = false;
                Timer = 2f;
            }
        }
    }
    void FixedUpdate()
    {
        // controlla se si e' morti, e in caso azzera la velocità o la rende pari al valore preso, la velocità e un moltiplicatore
        // gestito dalla funzione Cresci, che qua viene chiamata
        rb2d.linearVelocity = isDead ? NonMuoversi() : new Vector2(horizontalMovement * Speed * Multiplier, rb2d.linearVelocity.y);
    }
    void LateUpdate()
    {
        // 
        if (!isDead) Cresci(GameManager.Monete);
        SpriteManager.Instance.AliBody.transform.position = new Vector3(transform.position.x - 0.09375f, transform.position.y - 0.3f, transform.position.z);
        SpriteManager.Instance.ActiveSprite.transform.position = transform.position;
    }
    public Vector2 NonMuoversi()
    {
        // azzera la velocita' del rigidbody e cambia lo stato del bool isDead (per lo script kill.cs)
        isDead = true;
        return new Vector2(0, 0);
    }
    IEnumerator ExtraJumpAnimation()
    {
        // attiva le ali, avvia l'animazione e il suono, e quando finisce l'animazione disattiva le ali
        SpriteManager.Instance.AliBody.SetActive(true);
        SpriteManager.Instance.AliBody.GetComponent<Animator>().SetTrigger("JumpExtra");
        AudioManager.Instance.AudioList[7].Play();
        yield return new WaitForSeconds(SpriteManager.Instance.AliBody.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        SpriteManager.Instance.AliBody.SetActive(false);
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
                    SpriteManager.Instance.ActiveSprite.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
                    SpriteManager.Instance.AliBody.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
                    Multiplier = 5f;
                    rb2d.gravityScale = 6f;
                    FootstepVolume = Random.Range(1.4f, 1.6f);
                    Groundchek.position = (Groundchek.position == new Vector3(Groundchek.position.x, transform.position.y - 1.62f, Groundchek.position.z)) ? new Vector3(Groundchek.position.x, Groundchek.position.y, Groundchek.position.z) : new Vector3(Groundchek.position.x, transform.position.y - 1.62f, Groundchek.position.z);
                    SpriteManager.Instance.AliBody.transform.position = (SpriteManager.Instance.AliBody.transform.position == new Vector3(transform.position.x - 0.666664f, transform.position.y - 0.5f, SpriteManager.Instance.AliBody.transform.position.z)) ? new Vector3(transform.position.x - 0.666664f, transform.position.y - 0.5f, SpriteManager.Instance.AliBody.transform.position.z) : new Vector3(SpriteManager.Instance.AliBody.transform.position.x, SpriteManager.Instance.AliBody.transform.position.y, SpriteManager.Instance.AliBody.transform.position.z);
                    WallOffset = 1.44f;
                    break;
                }
            case >= 54:
                {
                    transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
                    SpriteManager.Instance.ActiveSprite.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
                    SpriteManager.Instance.AliBody.transform.localScale = new Vector3(2.7f, 2.7f, 2.7f);
                    Multiplier = 2.25f;
                    rb2d.gravityScale = 2.5f;
                    FootstepVolume = Random.Range(1.2f, 1.4f);
                    WallOffset = 1.08f;
                    break;
                }
            case >= 27:
                {
                    transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                    SpriteManager.Instance.ActiveSprite.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                    SpriteManager.Instance.AliBody.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                    Multiplier = 1.5f;
                    rb2d.gravityScale = 2f;
                    FootstepVolume = Random.Range(1f, 2f);
                    WallOffset = 0.71f;
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
        SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetTrigger("Jumping");
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0);
        rb2d.AddForce(Vector2.up * JumpForce * Multiplier, ForceMode2D.Impulse);
        AudioManager.Instance.AudioList[2].Play();
    }
}
//https://www.smbgames.be/super-mario-brothers.php
