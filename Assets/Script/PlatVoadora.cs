using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatVoadora : MonoBehaviour
{
    public float tempoVoando;
    private TargetJoint2D target;
    private BoxCollider2D col;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Voando", tempoVoando);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void Voando()
    {
        target.enabled = false;
        col.isTrigger = true;
    }
}
