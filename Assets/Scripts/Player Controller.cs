using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnimator; // Controlar animações
    private Rigidbody2D playerRB; // Controlar movimentação
    private SpriteRenderer playerSprites;

    public Transform groundCheck; // obj para vereificar colisão com o chão
    public bool isGround = false;

    // Movimentação
    public float speed;
    public float run = 0.0f;

    // Pulo
    public bool jump = false;
    public float jumpForce;
    public int numberJumps = 0;
    public int maxJump = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerSprites = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Chao"));
        playerAnimator.SetBool("IsGrounded", isGround);

        run = Input.GetAxisRaw("Horizontal");        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerAnimator.SetBool("Jump", jump);
        }
        playerAnimator.SetBool("Jump", false);
        TrocaAnim();
    }

    private void FixedUpdate()
    {
        MovePlayer(run);
        if (jump)
        {
            PlayerJump();
        }
    }

    void MovePlayer(float movimentoH)
    {
        playerRB.linearVelocity = new Vector2(movimentoH * speed, playerRB.linearVelocity.y);
        if(run >= 0)
        {
            playerSprites.flipX = false;
        }
        else
        {
            playerSprites.flipX = true;
        }
    }

    void PlayerJump()
    {
        if (isGround)
        {
            playerRB.AddForce(new Vector2(0f, jumpForce));
            isGround = false;
        }
        jump = false;
    }

    void TrocaAnim()
    {
        playerAnimator.SetBool("Walk", playerRB.linearVelocity.x != 0);
        playerAnimator.SetBool("Jump", !isGround);
    }
}