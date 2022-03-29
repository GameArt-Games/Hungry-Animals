using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public bool isActivePowerup = true;

    Queue<GameObject> _animalPrefapsQueue;

    Queue<GameObject> _powerupsLifeExtendQueue;
    Queue<GameObject> _powerupsSpeedDownQueue;
    Queue<GameObject> _powerupsPlayerShieldQueue;
    Queue<GameObject> _powerupsVillageFieldQueue;


    [SerializeField] GameObject[] _animalPrefaps;
    [SerializeField] GameObject[] _powerupsPrefabs;

    [SerializeField] PlayerController _player;

    int _maxAnimal = 10;
    int _maxPowerups = 3;

    float _startDelay = 4;
    float _spawnInterval = 1.5f;

    int boundry;

    GameObject _powerup;

    // Start is called before the first frame update
    void Start()
    {
        if ( SceneManager.GetActiveScene().buildIndex == 0)
        {
            _startDelay = 2.5f;
            InvokeRepeating("RandomAnimal", _startDelay, _spawnInterval);
        }
        else{
            boundry = _player.boundryX;

            _powerupsLifeExtendQueue = new Queue<GameObject>();
            _powerupsSpeedDownQueue = new Queue<GameObject>();
            _powerupsPlayerShieldQueue = new Queue<GameObject>();
            _powerupsVillageFieldQueue = new Queue<GameObject>();

            for (int i = 0; i < _maxPowerups; i++)
            {
                GameObject tmpLifeExtend = Instantiate(_powerupsPrefabs[0], new Vector3(100, 15.03f, 0), _powerupsPrefabs[0].transform.rotation);
                _powerupsLifeExtendQueue.Enqueue(tmpLifeExtend);

                GameObject tmpSpeedDown = Instantiate(_powerupsPrefabs[1], new Vector3(100, 15.03f, 0), _powerupsPrefabs[1].transform.rotation);
                _powerupsSpeedDownQueue.Enqueue(tmpSpeedDown);

                GameObject tmpPlayerShield = Instantiate(_powerupsPrefabs[2], new Vector3(100, 15.03f, 0), _powerupsPrefabs[2].transform.rotation);
                _powerupsPlayerShieldQueue.Enqueue(tmpPlayerShield);

                GameObject tmpVillageField = Instantiate(_powerupsPrefabs[3], new Vector3(100, 15.03f, 0), _powerupsPrefabs[3].transform.rotation);
                _powerupsVillageFieldQueue.Enqueue(tmpVillageField);
            }

            InvokeRepeating("RandomAnimal", _startDelay, _spawnInterval);
            InvokeRepeating("RandomPowerupsDequeue", _startDelay + 5, _spawnInterval + 15);
        }
    }

    void RandomAnimal(){

        int animalIndex = Random.Range(0, _animalPrefaps.Length);
        int animalPosition = Random.Range(-boundry,boundry);

        Instantiate(_animalPrefaps[animalIndex], new Vector3(animalPosition,0,30), _animalPrefaps[animalIndex].transform.rotation);
    }

    public void RandomPowerupsEnqueue(GameObject go)
    {
        switch (go.tag)
        {
            case nameof(Enums.Powerup.LifeExtend):
                go.name = "Powerup_LifeExtend";
                go.transform.GetChild(0).GetComponent<Animator>().enabled = false;
                go.transform.GetChild(1).gameObject.SetActive(false);
                go.SetActive(false);
                _powerupsLifeExtendQueue.Enqueue(go);
                break;
            case nameof(Enums.Powerup.SpeedDown):
                GameObject tmpSpeedDown = go;
                tmpSpeedDown.name = "Powerup_SpeedDown";
                tmpSpeedDown.SetActive(false);
                _powerupsSpeedDownQueue.Enqueue(tmpSpeedDown);
                break;
            case nameof(Enums.Powerup.PlayerShield):
                GameObject tmpPlayerShield = go;
                tmpPlayerShield.name = "Powerup_PlayerShield";
                tmpPlayerShield.SetActive(false);
                _powerupsPlayerShieldQueue.Enqueue(tmpPlayerShield);
                break;
            case nameof(Enums.Powerup.VillageShield):
                GameObject tmpVillageField = go;
                tmpVillageField.name = "Powerup_VillageShield";
                tmpVillageField.SetActive(false);
                _powerupsVillageFieldQueue.Enqueue(tmpVillageField);
                break;
        }
    }

    void RandomPowerupsDequeue()
    {
        int powerupIndex = 0;
        //int powerupIndex = Random.Range(0, _powerupsPrefabs.Length);

        switch (powerupIndex)
        {
            case (int)Enums.Powerup.LifeExtend:
                _powerup = _powerupsLifeExtendQueue.Dequeue();
                break;
            case (int)Enums.Powerup.SpeedDown:
                _powerup = _powerupsSpeedDownQueue.Dequeue();
                break;
            case (int)Enums.Powerup.PlayerShield:
                _powerup = _powerupsPlayerShieldQueue.Dequeue();
                break;
            case (int)Enums.Powerup.VillageShield:
                _powerup = _powerupsVillageFieldQueue.Dequeue();
                break;
        }
        _powerup.transform.position = new Vector3(Random.Range(-boundry, boundry), 15.03f, Random.Range(_player.boundryBottom, _player.boundryTop));
        _powerup.transform.localScale = Vector3.one * 0.5f;
        _powerup.SetActive(true);
        isActivePowerup = true;
    }
}
