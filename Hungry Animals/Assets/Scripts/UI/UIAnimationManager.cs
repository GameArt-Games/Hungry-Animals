using UnityEngine;

public class UIAnimationManager : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] UIGameManager _uIGameManager;
    [SerializeField] UIMenuManager _uIMenuManager;

    float _tweenTime = 1f;

    float _prevVillageHealth = 0.1f;
    float _prevPlayerHealth = 1f; 

    void Start()
    {
    }

    #region Main Menu UI Animations

    #endregion


    #region Game UI Animations

    public void AnimScore()
    {
        LeanTween.cancel(_uIGameManager.scoreTxt.gameObject);
        _uIGameManager.scoreTxt.transform.localScale = Vector3.one;
        LeanTween.scale(_uIGameManager.scoreTxt.gameObject, Vector3.one * 2, 0.5f).setEasePunch();

        //_uIGameManager.scoreTxt.GetComponent<RectTransform>().localScale = _prevScorePos;
        //_uIGameManager.scoreTxt.GetComponent<Transform>().localPosition = Vector3.one;
        //_uIGameManager.scoreTxt.GetComponent<RectTransform>().localScale = Vector3.one;
        //LeanTween.scale(_uIGameManager.scoreTxt.gameObject, Vector3.one * 2, 0.5f).setEaseInBounce();
        //LeanTween.scale(_uIGameManager.scoreTxt.gameObject, Vector3.one * 2, 0.5f).setEaseShake();
    }

    public void AnimVillageHealth(float villageHealth)
    {   
        LeanTween.value(_uIGameManager.villageHealthImg.gameObject, _prevVillageHealth, villageHealth, _tweenTime)
            .setEasePunch()
            .setOnUpdate( (value) =>
            {
                _uIGameManager.villageHealthImg.fillAmount = villageHealth == 1 ? 1 : value;
                _prevVillageHealth = villageHealth;
                //_uIGameManager.GetComponent<UIManager>().villageHealthImg.color = Color.Lerp(Color.green, Color.red, value);
            });
    }

    public void AnimPlayerHealth(int playerHealth)
    {
        float fromValue = 1;
        if (playerHealth >= 0 && !gameManager.isGameOver)
        {
            switch (playerHealth)
            {
                case 2:
                    fromValue = 0.66f;
                    break;
                case 1:
                    fromValue = 0.33f;
                    break;
                case 0:
                    fromValue = 0f;
                    StartCoroutine(gameManager.GameOverMsg(1));
                    break;
            }
        }

        LeanTween.value(_uIGameManager.playerHealthImg.gameObject, fromValue, _prevPlayerHealth, _tweenTime)
            .setEasePunch()
            .setOnUpdate((value) =>
            {
                _uIGameManager.playerHealthImg.fillAmount = playerHealth == 0 ? 0 : value;
                _prevPlayerHealth = playerHealth;
            });
    }

    #endregion
}
