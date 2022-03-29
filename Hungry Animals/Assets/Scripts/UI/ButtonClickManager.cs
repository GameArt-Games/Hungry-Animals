using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickManager : MonoBehaviour
{
    UIMenuManager _uIMenuManager;

    Transform _defultPos;
    float _tweenTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        _uIMenuManager = GameObject.FindGameObjectWithTag("UI_Menu_Manager").GetComponent<UIMenuManager>();
        _defultPos = gameObject.transform;

        InvokeRepeating("BtnIdleAnim", 0.5f, Random.Range( 3f,5f));
    }

    public void ButtonClick()
    {
        BtnClickAnim();
        BtnClickSound();
    }

    void BtnIdleAnim()
    {
        LeanTween.cancel(gameObject);
        LeanTween.rotateZ(gameObject, -10, _tweenTime).setEaseShake();

        if (gameObject.name == "Button_Play")
        {
            LeanTween.scale(gameObject, Vector3.one * 2, _tweenTime).setEasePunch();
        }
    }

    void BtnClickAnim()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, _defultPos.localScale * 0.75f, 0.5f).setEasePunch();
    }

    void BtnClickSound()
    {
        if (_uIMenuManager.dontDistroyManager.isGameRunning)
        {
            _uIMenuManager.audioManager.Play("Button_Clicks");
        }
    }
}
