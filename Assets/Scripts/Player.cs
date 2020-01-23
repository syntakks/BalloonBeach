using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject sceneManager; 
    Rigidbody rb;
    public float playerSpeed = 1500;
    public float directionalSpeed = 20;
    public AudioClip score;
    public AudioClip damage; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckSteer(); 
    }

    private void CheckSteer()
    {
        if (sceneManager.GetComponent<App_Initialize>().isPaused)
        {
            return; 
        }
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 newPosition = new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), gameObject.transform.position.y, gameObject.transform.position.z);
        transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, directionalSpeed * Time.deltaTime);
#endif

        // Mobile Controls
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        if (Input.touchCount > 0)
        {
            transform.position = new Vector3(touch.x, transform.position.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * playerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * GetComponent<Rigidbody>().velocity.z / 2); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "score")
        {
            GetComponent<AudioSource>().PlayOneShot(score, 1.0f); 
        }

        if (other.gameObject.tag == "triangle")
        {
            GetComponent<AudioSource>().PlayOneShot(damage, 1.0f);
            sceneManager.GetComponent<App_Initialize>().GameOver(); 
        }
    }
}
