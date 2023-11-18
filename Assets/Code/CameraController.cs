using Cinemachine;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraZoomMax = 7f;
    public float cameraZoomMin = 8f;
    public bool zooming = true;
    public bool active = true;
    public AnimationCurve zoomInCurve;
    public AnimationCurve zoomOutCurve;
    private CinemachineVirtualCamera camera;
    GameObject player;

    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        //confiner = GetComponent<CinemachineConfiner>();
        player = GameObject.Find("Player");
        if (active)
            camera.m_Lens.OrthographicSize = cameraZoomMax;
    }

    // Update is called once per frame
    void Update()
    {
        cameraZoom();   
    }
    void cameraZoom()
    {
        if (active)
        {
            if (zooming || player.GetComponent<PlayerController>().speed < 0.5)
            {
                if (camera.m_Lens.OrthographicSize > cameraZoomMax)
                {
                    float normalizedProgress = Mathf.InverseLerp(cameraZoomMin, cameraZoomMax, camera.m_Lens.OrthographicSize);
                    var easing = zoomInCurve.Evaluate(normalizedProgress);

                    // Use Mathf.Lerp to smoothly interpolate over time
                    camera.m_Lens.OrthographicSize = Mathf.Lerp(camera.m_Lens.OrthographicSize, cameraZoomMax, easing * Time.deltaTime);
                }
            }
            else
            {
                if (camera.m_Lens.OrthographicSize < cameraZoomMin)
                {
                    float normalizedProgress = Mathf.InverseLerp(cameraZoomMin, cameraZoomMax, camera.m_Lens.OrthographicSize);
                    var easing = zoomOutCurve.Evaluate(normalizedProgress);

                    // Use Mathf.Lerp to smoothly interpolate over time
                    camera.m_Lens.OrthographicSize = Mathf.Lerp(camera.m_Lens.OrthographicSize, cameraZoomMin, easing * Time.deltaTime);
                }
            }
        }
    }
}
