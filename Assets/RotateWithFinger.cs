using UnityEngine;
using UnityEngine.EventSystems; // Для интерфейсов событий

public class RotateWithFinger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false; // Флаг для отслеживания удержания
    private Camera mainCamera;
    private float offset = 90f;

    [SerializeField] private AlarmController _alarmController;

    // Если Canvas настроен как Screen Space - Overlay, Camera не требуется
    // Если Screen Space - Camera или World Space, необходимо указать основную камеру
    void Start()
    {
        // Определяем, нужна ли камера для преобразования координат
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            mainCamera = canvas.worldCamera;
        }
        else
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            RotateHandTowardsPointer();
        }
    }

    // Обработка события нажатия указателя на элемент
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    // Обработка события отпускания указателя
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    void RotateHandTowardsPointer()
    {
        Vector3 pointerPosition;

        // Обработка касаний на мобильных устройствах
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            pointerPosition = touch.position;
        }
        else
        {
            // Обработка мыши для ПК
            pointerPosition = Input.mousePosition;
        }

        // Преобразование позиции указателя в мировые координаты
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(pointerPosition);

        // Если Canvas в Screen Space - Overlay, Z-координата может быть игнорирована
        worldPoint.z = transform.position.z;

        // Вычисляем направление от центра стрелки до указателя
        Vector3 direction = worldPoint - transform.position;

        // Вычисляем угол в градусах
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Применяем вращение с учетом смещения
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - offset));
        
        _alarmController.ChangeArrow();
    }
}
