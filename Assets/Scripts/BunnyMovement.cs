using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private Animator anim;
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
        if (speed > 0)
        {
            anim.SetBool("isMove", true);
            transform.Translate(speed / 100.0f, 0, 0);           //Заяц просто ходит с определённой скоростью, можно добавить проверку на финиш потом
        }
    }

    public void SetSpeed(float sp)
    {
        this.speed = sp;
    }
}
