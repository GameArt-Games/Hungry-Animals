using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] Enums.LeanTweenAnmType _leanTweenAnmType;

    [SerializeField] LeanTweenType _leanTweenType;
    [SerializeField] float _duration;
    [SerializeField] float _delay;

    [SerializeField] bool _loop;
    [SerializeField] bool _pingpong;

    [SerializeField] bool _startPositionOffset;
    [SerializeField] Vector3 _from;
    [SerializeField] Vector3 _to;

    LTDescr _lTDescr;

    [SerializeField] bool _showOnEnable;
    [SerializeField] bool _workOnDisable;

    public GameObject go;


    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.position = new Vector3(-13, 14, 0);
        //LeanTween.moveY(gameObject, 0.5f, 1f).setEaseOutBounce().setOnComplete(NextStep);
        LeanTween.moveY(gameObject, 0.5f, 2f).setEaseOutBounce().setOnComplete(()=>
        {
            LeanTween.scale(gameObject, Vector3.one * 1.2f, 1F).setEaseInElastic();
            //LeanTween.alpha(gameObject, 0, 1).setDelay(1);
            //LeanTween.alpha
            //LeanTween.color(gameObject, Color.red, 2f).setDelay(1);
            //LeanTween.alpha(gameObject, 1f, 1f).setEase(LeanTweenType.easeInCirc);
            //LeanTween.alpha(gameObject, 1f, 1f).setDelay(1f);

        });

    }

    void LifeExtend()
    {
        //LeanTween.scale(gameObject, Vector3.one * 1.2f, 1F).setEaseInElastic();
        LeanTween.alpha(go, 0, 1).setDelay(1);
        //LeanTween.color(gameObject, Color.red, 2f).setDelay(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
