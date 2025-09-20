using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWindowVisual : MonoBehaviour
{
    [SerializeField] DamageDealer _dealer;
    [SerializeField] SpriteRenderer _sr;

    void OnEnable()
    {
        _dealer.WindowOpened += Show; _dealer.WindowClosed += Hide;
        if (_dealer.WindowOpen)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    void OnDisable()
    {
        _dealer.WindowOpened -= Show;  _dealer.WindowClosed -= Hide;
    }

    void Show()
    {        
        _sr.enabled = true;  // (ou Animator.SetBool("Active", true))
    }
    void Hide()
    {
        _sr.enabled = false; // (ou Animator.SetBool("Active", false)) 
    }

}
