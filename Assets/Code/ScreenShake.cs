using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://youtu.be/CgyLIWyDXqo
public class ScreenShake : MonoBehaviour {

    public static ScreenShake instance;
    private float globalShakeForce = 1f;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource) {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}
