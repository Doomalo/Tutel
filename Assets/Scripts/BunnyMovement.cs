using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private Animator anim;
    private float stopSpeed = 0.0f;
    private bool stopBunny = false;
    private AudioSource audioSource;
    public AudioClip laugh;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(laugh);
        audioSource.Play();
    }
    void FixedUpdate()
    {
       // Debug.Log(stopBunny);
        if (stopBunny)
            transform.Translate(stopSpeed, 0, 0);
        else 
        {
            anim.SetBool("isMove", true);
            transform.Translate(speed / 100.0f, 0, 0);           //Заяц просто ходит с определённой скоростью, можно добавить проверку на финиш потом
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Fin"))
        {
            stopBunny = true;
            anim.SetInteger("win", 1);
        }
    }
}
