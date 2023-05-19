using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimController : MonoBehaviour
{
    public List<GameObject> platforms;
    private enum Platform
    {
        Sling = 0,
        Donkey = 1,
        Catapult = 2,
        Bear = 3,
        Geyser = 4,
        Cannon = 5
    }
    [SerializeField] private GameObject speedBar;
    private StartLaunch _launcher;
    private Animator _anim;
    private AudioSource audioSource;
    //private int platformNum = 0;
    private bool isLaunched = false;
    //private bool platformSet = false;

    // включить/отключить объект в зависимости от выбранного в меню
    // Start is called before the first frame update
    void Start()
    {
        _launcher = speedBar.GetComponent<StartLaunch>();
        //_anim = GetComponent<Animator>();
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActive(false);
        }
        int num = DataController.platformNum;
        platforms[num].SetActive(true);
        _anim = platforms[num].GetComponent<Animator>();
        _anim.SetBool("start", false);
        audioSource = platforms[num].GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (platformSet == true)
        //{
        //    platforms[platformNum].SetActive(true);
        //    platformSet = false;
        //}
        isLaunched = _launcher.ReturnLaunchState();
        if( isLaunched == true)
        {
            _anim.SetBool("start", true);
            audioSource.Play();
        }
    }

    //public void SetPlatform(int a)
    //{
    //    if (a >= 0 && a < 6)
    //    {
    //        platformNum = a;
    //    }
    //    else platformNum = 0;
    //    platformSet = true;
    //}
}
