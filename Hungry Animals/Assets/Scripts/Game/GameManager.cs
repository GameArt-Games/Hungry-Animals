using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public GameObject player;

    public AudioManager audioManager;
    public UIAnimationManager uIAnimationManager;
    public SpawnManager spawnManager;

    [SerializeField] UIGameManager _uIGameManager;
    [SerializeField] PostProcessingManager _postProcessingManager;

    static int _score;
    static int _playerHealth;
    static int _villageHealth;

    public static int Score{

        get { return _score; }
        set { _score = value; }
    }
    public static int PlayerHealth{

        get { return _playerHealth; }
        set { _playerHealth = value <= 0 ? 0 : value; }
    }

    public static int VillageHealth{

        get { return _villageHealth; }
        set { _villageHealth = value <= 0 ? 0 : value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 3;
        VillageHealth = 100;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();       
    }

    public void ScoreCount(int val){

        if(!isGameOver){
            if(val == 1){
                Score ++;
            }
            else{
                Score --;
            }
            uIAnimationManager.AnimScore();
            _uIGameManager.scoreTxt.text = Score.ToString();
        }
    }

    public void VillageHealthCount(){

        if(VillageHealth>0){
            VillageHealth = VillageHealth-10;
            uIAnimationManager.AnimVillageHealth((100 - VillageHealth) / 100f);
            _uIGameManager.villageHealthTxt.text = VillageHealth.ToString()+"%";

            if(VillageHealth <= 0){
                StartCoroutine(GameOverMsg(0));         
            }
        }
    }

    public void PlayerHealthCount(){

        audioManager.Play("Player_Hit");
        CameraShaker.Instance.ShakeOnce(4,4,.1f,1);

    //CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
    // c.PositionInfluence = posInf;
    // c.RotationInfluence = rotInf;

        #if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android && PlayerPrefs.GetInt("isVibrating",1) == 1)
            {
                Handheld.Vibrate();
            }
#endif
        if (PlayerHealth <=3 && PlayerHealth >0)
        {
            PlayerHealth--;
            uIAnimationManager.AnimPlayerHealth(PlayerHealth);
        }
    }

    public IEnumerator GameOverMsg(int val){
        
        _postProcessingManager.SaturationHighLow(-100f);

        if ( val == 0 && !isGameOver){
            audioManager.Play("Player_Lost_Village");
            _uIGameManager.gameOverMsgTxt.text = "You Lost Your Village..!";
        }
        else if(val == 0 && isGameOver){
            _uIGameManager.gameOverMsgTxt.text = "You Lost..! \n And lost your village too..";
        }
        else if(val == 1 && !isGameOver){
            audioManager.Play("Player_Death");
            player.GetComponent<Animator>().Play("Death_01");
            _uIGameManager.gameOverMsgTxt.text = "You Lost..!";
        }

        yield return new WaitForSeconds(0.3f);
        _uIGameManager.panelGameUI.SetActive(false);
        _uIGameManager.gameOverScoreTxt.text = Score.ToString();
        _uIGameManager.gameOverMsgTxt.gameObject.SetActive(true);
        _uIGameManager.panelGameOver.SetActive(true);

        isGameOver = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void GamePause(){
        _uIGameManager.panelPauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

     public void GameResume(){
        Time.timeScale = 1f;
        _uIGameManager.panelPauseUI.SetActive(false);
    }

    public void GameRestart(){
        Score = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void GoMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  - 1);
    }
}
