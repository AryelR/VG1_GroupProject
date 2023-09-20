using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LevelBounds : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        void Update()
        {
            transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
        }
    }


}