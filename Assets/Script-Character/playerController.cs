using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    //Public lets you edit the variables inside Unity itself
    //Private keeps it behind the scenes

    /*
     
     
     Enemy Jump         -check
     High Jump          -no
     Double Jump        -check
     Gun                -check
     Strong Gun         -no
     Wall Jump          -check
     Ice Shot           -no
     Shoot through Wall -no
     Dash               -check
     Health Up          -no
     Hover/Slow decent  -no
     Infinite Jump      -no

     */

    // player variables
    public int health;
    public int maxHealth;
    public int rangeDamage = 5;
    public int DeathCount=0;
    private int defence = 10;
    //movement variables

    public float moveSpeed;
    private float knockBack = 1f;
    public float direction = 1f;
    
    //shoot variables
    private int bulletStart = 5;
    private int bulletMid = 10;
    //grounded variablers
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    //Jump variables

    public float jumpHeight;
    public Transform groundCheck;
    private bool jump1 = false;
    private bool jump2 = false;

    //For wall jumping
    private bool touchingWall = false;

    //Dash Variables
    public float Speed;
    private float dashSpeed;
    private float dashTime = 5.0f;
    private float dashCount = 3f;
    private bool hasDash = false;
    private bool finDash = false;
    public float timerCount = 0f;
    private bool isDashing = false;

    // Gravity variables
    public float gravity = -9.81f;
    public bool facingRight = true;

    //Power up variable check
    public bool hasGun;
    public bool doubleJumping;
    public bool wallJumping;
    public bool dashing;
    public bool pierceShot;
    public bool bossKey = false;

    //death timer for respawning

    public float hold = 0;
    public bool iFrame = false;
    public float invincible = 0;


    //Save zones

    public Vector2 saveZone;

    //Display variables
    public Text timerText;
    public float timer=0;
    public Text pointText;
    public int points = 0;
    public Text WinText;
    private bool win = false;
    public Text healthText;

    private float grav;

    //Win screen count
    private float winScreenCount;

    private void Start()
    {
        winScreenCount = 0;
        facingRight = true;
        direction = 1;
        maxHealth = health;
        //sets grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //makes dash speed twice the movement speed
        dashSpeed = moveSpeed * 2;
        Speed = moveSpeed;
        //turns off all powerups
        hasGun = doubleJumping = wallJumping = dashing = pierceShot = false;

        timer = 0;
        points = 0;
        this.SetTimerText();

        if (LoadStatic.LoadChar == true)
        {
            Game n = new Game();
            n.load(n);
        }

        grav = GetComponent<Rigidbody2D>().gravityScale;

    }

    //************************************************
    //Name: SetTimerText()
    //
    //Purpose: Sets Timer and Point text 
    //***********************************************

    void SetTimerText()
    {
        timerText.text = "Timer: " + timer.ToString();
        pointText.text = "Points: " + points.ToString();
        WinText.text = "";
        healthText.text = "Health " + health.ToString() + " / " + maxHealth.ToString();
        if (win == true)
        {
            WinText.text = "YOU WIN!" ;
        }
    }

    //************************************************
    //Name: SetWinText()
    //
    //Purpose: Sets win text 
    //***********************************************

    void SetWinText()
    {
        WinText.text = "YOU WIN".ToString();
    }
    private void Flip() {
        if (direction > 0&&facingRight==false)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if(direction<0&&facingRight==true)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    //*********************************
    //HURT THE PLAYER
    //
    //******************************************
    public void hurt(int damage)
    {
        if (iFrame==false) {
            health -= damage;
        }
        iFrame = true;

    }


    //***********
    //UPDATE
    //
    //***********

    private void Update()
    {
        this.SetTimerText();
        if (win == false) { 
            timer += Time.deltaTime;
            Flip();
        if (health<=0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            if (hold >= 10f) { 
                DeathCount++;
                health = maxHealth;
                transform.position = saveZone;
                hold = 0;
                GetComponent<Rigidbody2D>().gravityScale=grav;
            }
            hold += .1f;
        }
        else{
                if (iFrame == true) {

                    invincible += .1f;
                    if (invincible >= 10f) { iFrame = false; invincible = 0; }
                }

        if (finDash == true)
        {
            Speed = moveSpeed;
            finDash = false;
        }
        //dash when e is pressed
        if ((Input.GetKeyDown(KeyCode.E) && hasDash == false|| Input.GetKeyDown(KeyCode.E) && hasDash == false && Input.GetKeyDown(KeyCode.E) )&&dashing==true)
        {
            dash();
        }
        //move right by pressing D
        else if ((Input.GetKey(KeyCode.D) && isDashing == false/*&& Input.GetKeyDown(KeyCode.E)*/ || Input.GetKey(KeyCode.D) && isDashing == false))
        {

            direction = 1f; //sets direction to right
            move();
        }
        //move left by pressing A
        else if ((Input.GetKey(KeyCode.A)/*&& Input.GetKeyDown(KeyCode.E)*/&& isDashing == false) || Input.GetKey(KeyCode.A) && isDashing == false)
        {
            direction = -1f; //sets direction to right
            move();
        }
        //Jump when spacebar pressed
        if (Input.GetKeyDown(KeyCode.Space) && jump2 == false)
        {
            
            //Second Jump
            if (jump1 == true && doubleJumping == true) {
                jump();
                jump2 = true;
            }
            //First Jump
            if (jump1 == false)
            {
                jump();
                jump1 = true;
            }

        }
        //Stop Sliding
        if (Input.GetKeyUp(KeyCode.A) && grounded==true)
        {
            float temp = Speed;
            Speed /= 2;

            move();
            Speed = temp;

        }
        if (Input.GetKeyUp(KeyCode.D) && grounded == true)
        {
            float temp = Speed;
            Speed /= 2;
            move();
            Speed = temp;
        }
       }
      }
        else {
            if (winScreenCount > 10f)
            {
                SceneManager.LoadScene("WinScreen");
            }
            winScreenCount+=1f;
            SetWinText();
            Time.timeScale = 1f;
        }
    }
    /*void gravityEffect()
    {
        transform.position = transform.position + (new Vector3(0, 1, gravity) * Time.deltaTime);
    }*/
    //************************************************
    //Name: move()
    //
    //Purpose: adds new vector to rigid body to move character 
    //***********************************************

    void move()
    {
        //transform.position = transform.position + (new Vector3(1, 0, moveSpeed) * Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction, GetComponent<Rigidbody2D>().velocity.y);
    }
    //************************************************
    //Name: jump()
    //
    //Purpose: adds new vector to rigid body to lift character up 
    //***********************************************


    void jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    //************************************************
    //Name: dash()
    //
    //Purpose: doubles move speed to go forward 
    //***********************************************
    void dash()
    {
        timerCount = 0f;
        Speed = dashSpeed;
        while (timerCount <= dashCount)
        {
            isDashing = true;
            move();
            if (grounded == false) { hasDash = true; }
            timerCount += Time.deltaTime;
            if (timerCount > dashCount)
            {
                Speed = moveSpeed;
                finDash = true;

            }
        }
        isDashing = false;
    }
    void WallJump()
    {

        GetComponent<Rigidbody2D>().velocity=(new Vector2(10f, jumpHeight));
      }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
        if (col.gameObject.tag == "Wall")
        {
            touchingWall = false;
        }
    }
    //Collision enter ariables
    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        //Ground collision detection to reset jumps and dash
        if (coll.gameObject.tag == "Ground")
        {
            jump1 = false;
            jump2 = false;
            hasDash = false;
            grounded = true;
            gravity = -9.81f;
            //transform.parent = coll.transform
        }
        //Enemy collision detected
        if (coll.gameObject.tag == "Enemy")
        {
           
        }
        if (coll.gameObject.tag == "Ice")
        {
            jump1 = false;
            jump2 = false;
            hasDash = false;
            //no wall jump
            //Slides
        }
        if(coll.gameObject.tag == "Water")
        {
            //if can swim jump = false
            // jump power doubled
            // speed slowed by .75
        }
        if (coll.gameObject.tag == "Wall" && wallJumping == true)
        {
            touchingWall = true;
            jump1 = false;
            jump2 = false;
            hasDash = false;
            grounded = true;
            
        }
        if (coll.gameObject.tag == "Death")
        {
            DeathCount++;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (coll.gameObject.tag == "Enemy"||coll.gameObject.tag=="EnemyBullet") {
            health -= 10;
            Vector3 dir = (coll.gameObject.transform.position - gameObject.transform.position).normalized;

            if (dir.y > 0)
            {
                this.GetComponent<Rigidbody2D>().AddForce(dir * 4);// hit top
            }
            else if (dir.y < 0)
            {
                this.GetComponent<Rigidbody2D>().AddForce(dir * 4);// hit bottom
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Gets the area to respawn at
        if (coll.gameObject.tag == "Save Zone")
        {
            saveZone = coll.transform.position;
            Game n = new Game();
             n.save();
//            SaveLoad.Save();
        }
        if (coll.gameObject.tag == "WinZone")
        {
            saveZone = coll.transform.position;
            Game n = new Game();
            n.save();
            win = true;
            //death();
        }
    }
    public void death()
    {
        DeathCount++;

    }

}
