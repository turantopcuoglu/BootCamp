using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int simdiki = 0, SimdikiSkin = 0, SimdikiSira = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;

        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("SimdikiSkin")) {
            simdiki = PlayerPrefs.GetInt("simdiki");
            SimdikiSkin = PlayerPrefs.GetInt("SimdikiSkin");
            SimdikiSira = PlayerPrefs.GetInt("SimdikiSira");
        }
        else
        {
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("simdiki", simdiki);
        PlayerPrefs.SetInt("SimdikiSkin", SimdikiSkin);
        PlayerPrefs.SetInt("SimdikiSira", SimdikiSira);
    }
}
