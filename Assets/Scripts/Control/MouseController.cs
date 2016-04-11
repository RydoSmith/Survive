using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour
{

    Vector3 lastFramePosition;
    Vector3 currentFramePosition;

    Vector3 dragStartPosition;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        //Set camera start pos
        mainCamera.transform.position = new Vector3(300 / 2, 300 / 2, mainCamera.transform.position.z);
    }

    void Update()
    {
        currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentFramePosition.z = 0;

        UpdateCameraMovement();
 
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

    }

    void CheckCameraBounds()
    {
        float horzExtent = mainCamera.orthographicSize * Screen.width / Screen.height;

        if (mainCamera.transform.position.y < mainCamera.orthographicSize)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.orthographicSize, mainCamera.transform.position.z);
        }
        if (mainCamera.transform.position.y > 300f - mainCamera.orthographicSize)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 300f - mainCamera.orthographicSize, mainCamera.transform.position.z);
        }
        if (mainCamera.transform.position.x < horzExtent)
        {
            mainCamera.transform.position = new Vector3(horzExtent, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        if (mainCamera.transform.position.x > 300f - horzExtent)
        {
            mainCamera.transform.position = new Vector3(300f - horzExtent, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    void UpdateCameraMovement()
    {
        //Handle camera movement
        if (Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currentFramePosition;
            mainCamera.transform.Translate(diff);
        }

        //Zoom
        mainCamera.orthographicSize -= mainCamera.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 7f, 26f);

        CheckCameraBounds();
    }
}
