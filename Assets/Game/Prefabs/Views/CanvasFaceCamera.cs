using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Камера не найдена. Убедитесь, что в сцене есть камера с тегом MainCamera.");
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Поворачиваем канвас так, чтобы он смотрел на камеру
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
