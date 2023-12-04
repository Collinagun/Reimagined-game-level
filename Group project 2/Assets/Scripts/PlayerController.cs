using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpPower;
    public float moveSpeed = 5f;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    // audio
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource bulletCollisionSound;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource checkpointSound;

    // Movement
    public PhysicsMaterial2D bounceMaterial, normalMaterial;
    float inputX, inputY;
    public LayerMask groundMask, wallMask;
    public float speed;
    
    // Mouse Position
    private Vector2 pointerInput;
    [SerializeField] private InputActionReference shoot, pointerPosition;

    public Vector2 Pointerposition { get; set; }
    public Vector2 direction { get; set; }
    Vector3 yAxisDirection;

    //weapon
    public Weapon weapon;
    Vector2 mousePosition;
    private WeaponParent weaponParent;

    // Jumping
    bool isJumping = false;
    public float jumpForce = 0.0f;
    bool jump = false;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
        backgroundMusic.Play();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        Vector2 looking = weaponParent.direction;

        //fire
        if (Input.GetMouseButtonDown(0))
        {
            if (looking.x < 0)
            {
                weapon.Fire();
            }
            else if (looking.x > 0)
            {
                weapon.Fire2();
            }

        }

        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon.Reload();
        }

        if (Input.GetKeyDown(KeyCode.Space)/* && !isJumping*/)
        {
            jumpSoundEffect.Play();
            //rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            //isJumping = true;
            rb.sharedMaterial = normalMaterial;
        }
        else
        {
            rb.sharedMaterial = bounceMaterial;
        }


        //

        //

       

        // Move left
        inputX = Input.GetAxis("Horizontal");
        
        // reflect = bounce.canBounce;

        // Checks if the player is grounded and will allow movement if so
        if (isGrounded == true && jump == false) {
            Move();
        }

        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(-2f, 2f);
            // transform.rotation = Quaternion.Euler(0, 0, 0); // Aim to the left
        }
        // Move right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(2f, 2f);
            // transform.rotation = Quaternion.Euler(0, 0, 0); // Aim to the right
        }
        // Checks if the player is grounded through an invisible collider that can help filter which gameobjects to collide with
        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.4f), 0f, groundMask); 

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector. transform.position.y);

        pointerInput = GetPointerInput();
        weaponParent.Pointerposition = pointerInput;
    }

    // Detect collision with ground



    private void FixedUpdate()
    {
        // if (isGrounded == true || reflec == false) {
        //     Move();
        // }

        if (jump) // if (jump == true)
        {
            Jump();
            jump = false;
        }

    }
    void Jump()
    {
        if (isGrounded){
        rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

   // public void Knockback(float amount)
   // {
     //   gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * amount);
  //  }
    
 

    // Detect collision with ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    // Detect collision with bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletCollisionSound.Play();
            weapon.AddAmmo(weapon.maxAmmoSize); // Add ammo
            Destroy(collision.gameObject); // Destroy bullet
        }

        if (collision.gameObject.CompareTag("FallDetector"))
        {
            // Move the player to the respawn point
            transform.position = respawnPoint;
        }

        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
            checkpointSound.Play();
        }

        else if (collision.gameObject.CompareTag("Ai"))
        {
            // Load the GameOver scene
            SceneManager.LoadScene("GameOver");
            
        }

        else if (collision.gameObject.CompareTag("Win"))
        {
            // Load the GameOver scene
            SceneManager.LoadScene("Win");

        }
    }
}
