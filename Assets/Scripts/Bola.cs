using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject GoalPlayer1;
    public GameObject GoalPlayer2;
    public GameObject TamengP1;
    public GameObject TamengP2;

    int scoreP1;
    int scoreP2;

    public Text scoreUIP1;
    public Text scoreUIP2;

    new AudioSource audio;
    public AudioClip hitSound;
    public AudioClip goalSound;

    public float force;
    Rigidbody2D rigid;

    public GameObject panelSelesai;
    public Text txPemenang;

    public Text txtSpeed;

    public bool isComp;

    private float x = 2.0f;
    // Start is called before the first frame update
    void Start() {
        audio = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);
        if (panelSelesai != null) {
            panelSelesai.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (txtSpeed != null) {
            if (x < 7) {
                x += 0.001f;
                txtSpeed.text = "Speed : " + (x + force).ToString("F1");
            }
            else {
                txtSpeed.text = "MAX SPEED";
            }
        }
    }

    void ResetBall(bool BallToP1) {
        x = 2.0f;
        transform.localPosition = new Vector2(0, -1);
        rigid.velocity = new Vector2(0, 0);
        Vector2 arah;
        if (BallToP1) {
            arah = new Vector2(-2, 0).normalized;
        }
        else {
            arah = new Vector2(2, 0).normalized;
        }

        rigid.AddForce(arah * force);

        if ((scoreP2 == 5) || (scoreP1 == 5)) {
            panelSelesai.SetActive(true);
            if (BallToP1) {
                txPemenang.text = "Player 1 Menang!";
            }
            else {
                if (isComp) {
                    txPemenang.text = "Paman Pong Menang!";
                }
                else {
                    txPemenang.text = "Player 2 Menang!";
                }
                
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {

        if (GoalPlayer1 != null) {
            if ((coll.gameObject == GoalPlayer1) || (coll.gameObject == GoalPlayer2)) {
                audio.PlayOneShot(goalSound);
            }
            else {
                audio.PlayOneShot(hitSound);
            }

            if (coll.gameObject == player1 || coll.gameObject == player2) {
                float sudut = (transform.position.y - coll.transform.position.y) * 6f;
                Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
                rigid.velocity = new Vector2(0, 0);
                rigid.AddForce(arah * force * x);
            }

            if (coll.gameObject == GoalPlayer1) {
                scoreP2 += 1;
                TampilkanScore();
                if (scoreP2 >= 4) {
                    TamengP1.SetActive(true);
                }
                ResetBall(false);
            }

            if (coll.gameObject == GoalPlayer2) {
                scoreP1 += 1;
                TampilkanScore();
                if (scoreP1 >= 4) {
                    TamengP1.SetActive(true);
                }
                ResetBall(true);
            }
        }
    }

    void TampilkanScore() {
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }
}
