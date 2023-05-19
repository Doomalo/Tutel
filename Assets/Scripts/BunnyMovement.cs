using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float speed;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip run;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(run);
    }
    void FixedUpdate()
    {
        anim.SetBool("isMove", true);
       // audioSource.PlayOneShot(run);
        transform.Translate(speed / 100.0f, 0, 0);           //Заяц просто ходит с определённой скоростью, можно добавить проверку на финиш потом
    }
}
