using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public EdgeCollider2D[] boundaryColliders;

    public GameObject arrowPrefab;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            arrowPrefab.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }
}