using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contr : MonoBehaviour
{
    public float moveSpeed = 5f;          // Скорость движения игрока
    public float lookSpeedX = 2f;        // Скорость поворота по оси X (вокруг оси Y)
    public float lookSpeedY = 2f;        // Скорость поворота по оси Y (вокруг оси X)
    public float upDownRange = 80f;      // Ограничение угла по оси X (вверх/вниз)

    private Camera playerCamera;         // Камера игрока
    private float rotationX = 0f;        // Поворот по оси X для камеры
    private Rigidbody rb;                // Rigidbody для движения игрока

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();  // Получаем камеру, которая находится внутри объекта игрока
        rb = GetComponent<Rigidbody>();                    // Получаем Rigidbody для работы с физикой

        Cursor.lockState = CursorLockMode.Locked;          // Блокируем курсор в центре экрана
        Cursor.visible = false;                            // Прячем курсор
    }

    // Update is called once per frame
    void Update()
    {
        // Получаем данные с мыши для вращения камеры
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Поворот по оси X (вверх/вниз)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        
        // Поворот по оси Y (вокруг персонажа)
        transform.Rotate(Vector3.up * mouseX);

        // Движение игрока
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Применяем движение с учетом времени
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }
}
