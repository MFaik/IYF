using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitbox : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("amogus")) {
            RandomGezenAmogus randomGezme = other.gameObject.GetComponent<RandomGezenAmogus>();
            randomGezme.ResetVelocity();
            randomGezme.UpdateMoveStop();
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("amogus")) {
            RandomGezenAmogus randomGezme = other.gameObject.GetComponent<RandomGezenAmogus>();
            randomGezme.UpdateMoveStop();
        }
    }    
}
