using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    PLAYER1,
    PLAYER2
}

public enum InputType
{
    Joystick,
    Keyboard
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 10f;
    private Camera mainCamera = null;
    [SerializeField] private PlayerNumber playerNumber = PlayerNumber.PLAYER1;
    [SerializeField] private InputType inputType = InputType.Keyboard;
    private int playerNum = 1;

    [SerializeField] private GameObject bulletPrefab = null;

    [SerializeField] private float bulletForce = 20f;

    void Start()
    {
        mainCamera = Camera.main;
        if(playerNumber == PlayerNumber.PLAYER1)
        {
            playerNum = 1;
        }
        else
        {
            playerNum = 2;
        }
    }

    void Update()
    {
        Rotate();
        Move();

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Move()
    {
        if (Mathf.Abs(GetHorizontalInput()) > 0.9f)
        {
            if(inputType == InputType.Joystick)
            {
                transform.position += GetHorizontalInput() * transform.right * movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += GetHorizontalInput() * Vector3.right * movementSpeed * Time.deltaTime;
            }
        }
        if (Mathf.Abs(GetVerticalInput()) > 0.9f)
        {
            if(inputType == InputType.Joystick)
            {
                transform.position += GetVerticalInput() * transform.up * movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += GetVerticalInput() * Vector3.up * movementSpeed * Time.deltaTime;
            }
        }
    }

    float GetHorizontalInput()
    {
        float horizontalInput = Input.GetAxis("Player" + playerNum + "Horizontal" + inputType.ToString());
        
        return horizontalInput;
    }

    float GetVerticalInput()
    {
        float verticalInput = Input.GetAxis("Player" + playerNum + "Vertical" + inputType.ToString());
        return verticalInput;
    }

    void Rotate()
    {
        if(inputType == InputType.Keyboard)
        {
            //Vector2 directionToMouse = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //float angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
                                            mousePosition.y - transform.position.y);
            transform.up = direction;
        }
        else
        {
            float rotationInput = Input.GetAxis("Player" + playerNum + "Rotation" + "Joystick");
            if (Mathf.Abs(rotationInput) > 0.8f)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime * -rotationInput;
                transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + rotationAmount);
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }
}
