using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.spawnManager.isActivePowerup)
        {
            LeanTween.moveY(gameObject, 0.5f, 2f).setEaseOutBounce().setOnComplete(() =>
            {
                LeanTween.scale(gameObject, Vector3.one, 0.3F).setEaseInElastic().setOnComplete(() =>
                {
                    gameObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(PowerupEndAnim());
                    }

                });
            });
            _gameManager.spawnManager.isActivePowerup = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (tag)
            {
                case nameof(Enums.Powerup.LifeExtend):
                    other.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    LifeExtend();
                    break;
                case nameof(Enums.Powerup.PlayerShield):
                    PlayerShield();
                    break;
                case nameof(Enums.Powerup.VillageShield):
                    VillageShield();
                    break;
                case nameof(Enums.Powerup.SpeedDown):
                    SpeedDown();
                    break;
            }
        }
    }

    IEnumerator PowerupEndAnim()
    {
        yield return new WaitForSeconds(5);
        gameObject.transform.GetChild(1).GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(5);
        _gameManager.spawnManager.RandomPowerupsEnqueue(gameObject);

    }

    void LifeExtend()
    {
        if (GameManager.PlayerHealth < 3)
        {
            GameManager.PlayerHealth++;

            _gameManager.audioManager.Play("Life_Extender");
            _gameManager.uIAnimationManager.AnimPlayerHealth(GameManager.PlayerHealth);
            _gameManager.spawnManager.RandomPowerupsEnqueue(gameObject);
        }
    }

    void SpeedDown()
    {

    }

    void PlayerShield()
    {
        //GameManager.
    }

    void VillageShield()
    {

    }
}
