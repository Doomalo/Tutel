using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRandomizer : MonoBehaviour
{

    private SpriteRenderer sr;
    public Sprite[] sprites;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        int id = Random.Range(0, sprites.Length);
        sr.sprite = sprites[id];
    }

}
