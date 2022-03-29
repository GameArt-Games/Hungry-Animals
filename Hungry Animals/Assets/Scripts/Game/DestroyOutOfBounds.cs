using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOutOfBounds : MonoBehaviour
{
    GameObject _gameManager;
    float _topBoundry = 50;
    float _bottomBoundry = -10;

    // Start is called before the first frame update
    void Start()
    {
        if ( SceneManager.GetActiveScene().buildIndex == 1)
        {
             _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > _topBoundry){
            Destroy(gameObject);
        }
        else if(transform.position.z < _bottomBoundry){
            if ( SceneManager.GetActiveScene().buildIndex == 1)
            {
                _gameManager.GetComponent<GameManager>().VillageHealthCount();
            }
            Destroy(gameObject);
        }
    }
}
