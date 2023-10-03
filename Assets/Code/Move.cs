using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cinemachine;

namespace Game
{
    public class LevelBounds : MonoBehaviour
    {
        public float moveDistance = 1.0f; // Distance to move
        public float moveInterval = 1.0f; // Time interval between move
        public float shakeDistance = 10f;
        public float lightShakeVelocity = 0.025f;
        public float mediumShakeVelocity = 0.08f;
        public float heavyShakeVelocity = 0.2f;


        private float timer = 0.0f;
        private CinemachineImpulseSource impulseSource;

        private void Start() {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        private void Update() {
            timer += Time.deltaTime;

            if (timer >= moveInterval) {
                // Reset the timer.
                timer -= moveInterval;

                // Move the object by the specified distance.
                transform.Translate(new Vector3(moveDistance, 0, 0));

            }
            distanceFromPlayer();
        }

        public void distanceFromPlayer() {
            //compare front of wall x pos to player x pos
            float distance = GameObject.Find("Player").transform.position.x - transform.position.x;
            if (distance <= shakeDistance && distance > shakeDistance/2) {
                //light shake
                impulseSource.m_DefaultVelocity.x = lightShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);
            }
            else if (distance <= shakeDistance/2 && distance >= 0) {
                //medium shake
                impulseSource.m_DefaultVelocity.x = mediumShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);
            }
            else if (distance < 0) {
                //heavy shake
                impulseSource.m_DefaultVelocity.x = heavyShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerController>()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


}