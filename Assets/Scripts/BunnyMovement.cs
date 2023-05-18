using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float speed;
    private Animator anim;
    public bool win = true;
    private SpriteRenderer spriteRenderer;
    public Sprite[] rabbitSkins;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rabbitSkins[PlayerPrefs.GetInt("Level", 1)];               /// ѕолучить номер установленного скина
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        anim.SetBool("isMove", true);
        transform.Translate(speed / 100.0f, 0, 0);           //«а€ц просто ходит с определЄнной скоростью, можно добавить проверку на финиш потом
        Finished();
    }
    void Finished()
    {
        if (win == true)
            anim.SetBool("isWin", true);
        else anim.SetBool("isWin", false);
    }
}
