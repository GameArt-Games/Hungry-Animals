using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DetectCollisions : MonoBehaviour
{
    public GameObject scoreTxt;

    GameObject _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_gameManager.GetComponent<GameManager>().isGameOver){
            if(other.tag == "Player"){
                _gameManager.GetComponent<GameManager>().PlayerHealthCount();
            }
            else if(other.tag == "Deer" && this.tag =="Pear"){   
                StartCoroutine(ScoreIncrease(other));
            }
            else if(other.tag == "Deer" && this.tag !="Pear" && this.tag !="Player"){
                ScoreDecrease(other);
            }
            else if(other.tag == "Fox" && this.tag =="Meat" ){
                StartCoroutine(ScoreIncrease(other));
            }
            else if(other.tag == "Fox" && this.tag !="Meat"  && this.tag !="Player"){
                ScoreDecrease(other);
            }
            else if(other.tag == "Moose" && this.tag =="Banana" ){
                StartCoroutine(ScoreIncrease(other));
            }
            else if(other.tag == "Moose" && this.tag !="Banana"  && this.tag !="Player"){
                ScoreDecrease(other);
            }
        }
    }

    public IEnumerator ScoreIncrease(Collider other){

        GameObject particle = other.transform.GetChild(1).gameObject;
        particle.SetActive(true);

        other.GetComponent<Animator>().Play("Eat");

        ScoreTxtInstantiate("+1", other);

        
        other.GetComponent<MoveForward>().enabled = false;
        other.GetComponent<BoxCollider>().enabled = false;


        if(!other.gameObject.GetComponent<MoveForward>().enabled){
            // gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MoveForward>().enabled = false;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 0 ,gameObject.transform.localPosition.z);
            gameObject.GetComponent<DetectCollisions>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<DestroyOutOfBounds>().enabled = false;
        }

        _gameManager.GetComponent<GameManager>().ScoreCount(1);

        yield return new WaitForSeconds(2f);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    void ScoreDecrease(Collider other){

        ScoreTxtInstantiate("-1", other);

        _gameManager.GetComponent<GameManager>().ScoreCount(-1);
        Destroy(gameObject);
    }

    void ScoreTxtInstantiate(string score, Collider other){

        if(score == "+1"){
            scoreTxt.GetComponent<TextMesh>().color = Color.green;
            scoreTxt.GetComponent<TextMesh>().text = score;
        }else{
            scoreTxt.GetComponent<TextMesh>().color = Color.red;
            scoreTxt.GetComponent<TextMesh>().text = score;
        }

        GameObject go = other.transform.GetChild(0).gameObject;

        Instantiate(scoreTxt, go.transform.position, go.transform.rotation, other.transform);
    }
}
