using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimController : MonoBehaviour
{
    [SerializeField] private GameObject launcher;
    private StartLaunch _launcher;
    private Animator _anim;
    private bool isLaunched = false;

    // включить/отключить объект в зависимости от выбранного в меню
    // Start is called before the first frame update
    void Start()
    {
        _launcher = launcher.GetComponent<StartLaunch>();
        _anim = GetComponent<Animator>();
        _anim.SetBool("start", false);
    }

    // Update is called once per frame
    void Update()
    {
        isLaunched = _launcher.ReturnLaunchState();
        if( isLaunched == true)
        {
            _anim.SetBool("start", true);
        }
    }
}
