using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pemukul : MonoBehaviour {
    public float batasAtas;
    public float batasBawah;
    public float kecepatan;
    public string axis;

    public Transform target;
    public Camera cam;
    void Start() {
        if (target != null) {
            kecepatan = 4.7f;
        }
    }
    void Update() {
        float gerak = Input.GetAxis(axis) * kecepatan * Time.deltaTime;
        float nextPos = transform.position.y + gerak;

        if(target != null) {
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            if (screenPos.x > (cam.pixelWidth / 2)) {
                if (!melebihiPembatas(nextPos)) {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.position.y), kecepatan * Time.deltaTime);
                }
                else {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.position.y), 0 * Time.deltaTime);
                }

                if (Vector3.Distance(transform.position, target.position) < 0.001f) {
                    target.position *= -1.0f;
                }
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, (batasAtas+batasBawah)/2), kecepatan * Time.deltaTime);
            }
        }
        else {
            if (!melebihiPembatas(nextPos)) {
                transform.Translate(0, gerak, 0);
            }
        }
    }

    bool melebihiPembatas(float nextPos) {
        if (nextPos >= batasAtas) {
            return true;
        }
        if (nextPos <= batasBawah) {
            return true;
        }
        return false;
    }
}
