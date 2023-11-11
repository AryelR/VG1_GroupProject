using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraZoomMax = 7f;
    public float cameraZoomMin = 8f;
    public bool zooming = true;
    public bool active = true;
    private CinemachineVirtualCamera camera;
    private CinemachineConfiner confiner;

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
        if (active)
        {
            if (zooming)
            {
                StartCoroutine(zoomIn());
            }
            else
            {
                StartCoroutine(zoomOut());
            }
        }
        
    }

    //adapted from https://discussions.unity.com/t/zoom-out-depending-on-velocity-speed/233274
    IEnumerator zoomOut()     
    {
        float zoomStep = 0.002f;
        float smoothness = 1.5f;
        WaitForSeconds myWait = new WaitForSeconds(smoothness);

        while (camera.m_Lens.OrthographicSize < cameraZoomMin)
        {

            camera.m_Lens.OrthographicSize += zoomStep; 
            yield return myWait;
        }
    }

    IEnumerator zoomIn()
    {
        float zoomStep = 0.001f;
        float smoothness = 1f;
        WaitForSeconds myWait = new WaitForSeconds(smoothness);

        while (camera.m_Lens.OrthographicSize > cameraZoomMax)
        {
            camera.m_Lens.OrthographicSize -= zoomStep; 
            yield return myWait;
        }
    }
}
