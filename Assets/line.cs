using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer; // Ссылка на компонент Line Renderer
    [SerializeField] private float lineLength = 10f; // Длина линии
    [SerializeField] private float lineWidth = 0.1f; // Ширина линии

    private bool isDrawing = false; // Флаг, указывающий на состояние рисования
    private List<Vector3> linePoints = new List<Vector3>(); // Список точек линии

    void Update()
    {
        // Проверяем состояние кнопки мыши
        if (Input.GetMouseButton(0))
        {
            // Если кнопка мыши нажата и не рисуется линия, начинаем рисование
            if (!isDrawing)
            {
                StartDrawing();
            }

            // Получаем координаты точки на земле
            Vector3 mousePos = GetMouseWorldPosition();

            // Добавляем координаты точки в список
            linePoints.Add(mousePos);

            // Обновляем Line Renderer с новыми точками
            lineRenderer.positionCount = linePoints.Count;
            lineRenderer.SetPositions(linePoints.ToArray());
        }
        else if (isDrawing)
        {
            // Если кнопка мыши отпущена и рисуется линия, заканчиваем рисование
            StopDrawing();
        }
    }

    // Метод для начала рисования линии
    private void StartDrawing()
    {
        // Очищаем список точек линии
        linePoints.Clear();

        // Включаем Line Renderer
        lineRenderer.enabled = true;

        // Устанавливаем параметры Line Renderer
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Устанавливаем длину дуги линии
        lineRenderer.positionCount = Mathf.FloorToInt(lineLength);

        // Обновляем состояние рисования
        isDrawing = true;
    }

    // Метод для окончания рисования линии
    private void StopDrawing()
    {
        // Очищаем список точек линии
        linePoints.Clear();

        // Отключаем Line Renderer
        lineRenderer.enabled = false;

        // Обновляем состояние рисования
        isDrawing = false;
    }

    // Метод для получения координат точки на земле
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
