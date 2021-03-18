using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Joystick,
    Keyboard
}

public enum PlayerColor
{
    Red,
    Blue
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 10f;
    private Camera mainCamera = null;
    [SerializeField] private InputType inputType = InputType.Keyboard;
    [SerializeField] private PlayerColor playerColor = PlayerColor.Blue;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Transform bulletSpawnPos;

    void Start()
    {
        mainCamera = Camera.main;

        if (Input.GetJoystickNames().Length > 0)
        {
            foreach (string joystick in Input.GetJoystickNames())
            {
                Debug.Log(joystick);
            }
        }
    }

    void Update()
    {
        Rotate();
        Move();

        if (Input.GetButtonDown("Player" + "Shooting" + inputType.ToString()))
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
                transform.position += GetVerticalInput() * transform.forward * movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += GetVerticalInput() * Vector3.forward * movementSpeed * Time.deltaTime;
            }
        }
    }

    float GetHorizontalInput()
    {
        float horizontalInput = Input.GetAxis("Player" + "Horizontal" + inputType.ToString());
        
        return horizontalInput;
    }

    float GetVerticalInput()
    {
        float verticalInput = Input.GetAxis("Player" + "Vertical" + inputType.ToString());
        return verticalInput;
    }

    bool GetShootingInput()
    {
        if(Input.GetButtonDown("Player" + "Shooting" + inputType.ToString()))
        {
            return true;
        }
        return false;
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

            Vector3 direction = new Vector3(mousePosition.x - transform.position.x, transform.position.y, mousePosition.z - transform.position.z);
            transform.forward = direction;
        }
        else
        {
            float rotationInput = Input.GetAxis("Player" +  "Rotation" + "Joystick");
            if (Mathf.Abs(rotationInput) > 0.8f)
            {
                float rotationAmount = rotationSpeed * Time.deltaTime * rotationInput;
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + rotationAmount, 0f);
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
        if(playerColor == PlayerColor.Blue)
        {
            bullet.tag = "BlueBullet";
            bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            bullet.tag = "RedBullet";
            bullet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        Vector3 shootDir = ((transform.position + transform.forward) - transform.position).normalized;
        bullet.GetComponent<Bullet>().Setup(shootDir);
    }
}
