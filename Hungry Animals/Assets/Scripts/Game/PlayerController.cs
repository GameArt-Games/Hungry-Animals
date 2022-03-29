using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public DynamicJoystick dynamicJoystick;

    public GameObject[] projectilePrefabs;
    public GameObject gameManager;

    internal int testInt1 = 0 ;
    float movingSpeed = 12;

    float horizontalInput;
    float horizontalInputX;
    float horizontalInputY;

    public int boundryX = 10;
    public int boundryTop = 15;
    public int boundryBottom = -6;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor Hide
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.GetComponent<GameManager>().isGameOver){
            PlayerMovement();
            ProjecttileClicks();
        }
    }

    void PlayerMovement(){

        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        // rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        transform.Translate(Time.deltaTime * direction.x * movingSpeed,0,Time.deltaTime * direction.z * movingSpeed);
            // gameObject.GetComponent<Animator>().Play("Walk_Static");

        Debug.DrawLine(Vector3.zero,direction, Color.cyan);


        //Mouse Movement
        // horizontalInputX = Input.GetAxis("Mouse X");
        // horizontalInputY = Input.GetAxis("Mouse Y");

        // horizontalInputX = joystick.Horizontal;
        // horizontalInputY = joystick.Vertical;

        // transform.Translate(Time.deltaTime * horizontalInputX * movingSpeed,0,Time.deltaTime * horizontalInputY * movingSpeed);

        // horizontalInput = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * movingSpeed);

        // This run when hit the boundry
        if(transform.position.x < - boundryX){
            transform.position = new Vector3(-boundryX, transform.position.y, transform.position.z);
        }

        else if(transform.position.x > boundryX){
            transform.position = new Vector3(boundryX, transform.position.y, transform.position.z);
        }

        if(transform.position.z > boundryTop){
            transform.position = new Vector3(transform.position.x, transform.position.y, boundryTop);
        }
        else if(transform.position.z < boundryBottom){
            transform.position = new Vector3(transform.position.x, transform.position.y, boundryBottom);
        }
    }

    void ProjecttileClicks(){
        if (Input.GetKeyDown(KeyCode.Q)){
            ProjectileFood((int)Enums.Food.Banana);
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            ProjectileFood((int)Enums.Food.Apple);
        }
         else if (Input.GetKeyDown(KeyCode.Z)){
            ProjectileFood((int)Enums.Food.Meat);
        }
    }

    public void ProjectileFood(int val){
        // gameObject.GetComponent<Animator>().Play("GrenadeThrow");
            // gameObject.GetComponent<Animator>().Play("Run_Static");

       FindObjectOfType<AudioManager>().Play("Player_Projectile");
        gameObject.GetComponent<Animator>().Play("Crouch_Up");

        Instantiate(projectilePrefabs[val], new Vector3(transform.position.x,1.49f,transform.position.z+2), projectilePrefabs[val].transform.rotation);
    }
}
