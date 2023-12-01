using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cinemachine;
using UnityEngine.TerrainUtils;

namespace Game
{
    public class LevelBounds : MonoBehaviour
    {
        //variables
        public float moveDistance = 1.0f; // Distance to move
        public float moveInterval = 1.0f; // Time interval between move
        public float shakeDistance = 10f;
        public float lightShakeVelocity = 0.025f;
        public float mediumShakeVelocity = 0.08f;
        public float heavyShakeVelocity = 0.2f;
        public float playerDamageTimer = 3.0f; //time behind the wall before player is damaged
        public double damage = 0.5;


        private float timer = 0.0f; 
        private CinemachineImpulseSource impulseSource;

        private float behindWallTimer = 0.0f; //current time behind wall

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
            //no shake
            if (GameObject.Find("Player").GetComponent<PlayerController>().isDead)
            {
                return;
            }
            //compare front of wall x pos to player x pos
            float distance = GameObject.Find("Player").transform.position.x - transform.position.x;
            if (distance <= shakeDistance && distance > shakeDistance/2) {
                //light shake
                impulseSource.m_DefaultVelocity.x = lightShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);

                //reset timer and heart flashing
                behindWallTimer = 0;
                GameObject.Find("HeartContainer").GetComponent<HealthController>().StopFlashing();
            }
            else if (distance <= shakeDistance/2 && distance >= 0) {
                //medium shake
                impulseSource.m_DefaultVelocity.x = mediumShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);

                //reset timer and heart flashing
                behindWallTimer = 0;
                GameObject.Find("HeartContainer").GetComponent<HealthController>().StopFlashing();
            }
            else if (distance < 0) {
                //heavy shake
                impulseSource.m_DefaultVelocity.x = heavyShakeVelocity;
                ScreenShake.instance.CameraShake(impulseSource);
                //3 second timer 
                behindWallTimer += Time.deltaTime;
                if(behindWallTimer >= playerDamageTimer)
                {
                    var player = GameObject.Find("Player").GetComponent<PlayerController>();
                    //hurt player
                    PlayerController.health -= damage;
                    player.Die();
                    //move wall back
                    transform.Translate(new Vector3(-9*moveDistance, 0, 0));
                }
                //flash hearts              
                GameObject.Find("HeartContainer").GetComponent<HealthController>().SetFlashing();
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerController>()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


}