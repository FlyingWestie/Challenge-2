using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public AudioClip MusicClip;

    public AudioSource MusicSource;

    public Text countText;

    public Text winText;

    public Text livesText;

    private int count;

    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
        MusicSource.clip = MusicClip;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == 4)
        {
            transform.position = new Vector2(100.0f, 0.0f);
            lives = 3;
            SetLivesText();
        }

        if (count >= 8)
        {
            MusicSource.Play();
            winText.text = "You win! Game created by Steven Pineda!";
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            Destroy(this);
            winText.text = "You Lose, Try Again!";
        }
    }

}