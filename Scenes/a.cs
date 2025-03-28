using UnityEngine;

public class a : MonoBehaviour
{
    public float moveSpeed = 5f;           // Скорость движения
    public float lookSpeedX = 2f;         // Скорость поворота по оси X (вокруг оси Y)
    public float lookSpeedY = 2f;         // Скорость поворота по оси Y (вокруг оси X)
    public float upDownRange = 80f;       // Ограничение угла по оси X (вверх и вниз)

    private Camera playerCamera;
    private float rotationX = 0f;         // Переменная для контроля поворота камеры по оси X

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();  // Получаем камеру, которая находится внутри объекта игрока
        Cursor.lockState = CursorLockMode.Locked;          // Блокируем курсор в центре экрана
        Cursor.visible = false;                            // Прячем курсор
    }

    private void Update()
    {
        // Управление поворотом камеры по осям X и Y
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        // Управление движением игрока
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
