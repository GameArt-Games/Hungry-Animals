using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaving : MonoBehaviour
{
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerData();
    }

    void CreatePlayerData(){
        Debug.Log("CreatePlayerData");

        playerData = new PlayerData("Wee", 10f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    public void SaveData(){
        Debug.Log("SaveData " + playerData.name+" "+playerData.health);

        PlayerPrefs.SetString("Name", playerData.name);
        PlayerPrefs.SetFloat("Health", playerData.health);

    }
    public void LoadData(){
        Debug.Log("LoadData");

        playerData = new PlayerData(PlayerPrefs.GetString("Name"), PlayerPrefs.GetFloat("Health"));
        Debug.Log(playerData.ToString());
        Debug.Log("LoadData " + playerData.name);

    }
}
