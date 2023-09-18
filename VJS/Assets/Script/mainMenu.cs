using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData { 
    public  LevelData(string LevelName)
    {
        string data = PlayerPrefs.GetString(LevelName);
        if (data == "")
            return;
        string[] AllData = data.Split('&');
        BestTime = float.Parse(AllData[0]);
        SilverTime = float.Parse(AllData[1]);
        GoldTime = float.Parse(AllData[2]);

    }
    public float BestTime { set; get; }
    public float SilverTime { set; get; }
    public float GoldTime { set; get; }
}


public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject LButton, LPanel, SButton, SPanel;
    [SerializeField] private Material playerMaterial;

    private Transform cameraTransform, cameraLook;
    private const float CAMERA_DONUS = 3.0f;

    private int[] ucretler = {0,150,150,150,
                             300,300,300,300,
                             500,500,500,500,
                             750,750,1000,1250};

    [SerializeField] private Text Maliyet;


    void Start()
    {
        Time.timeScale = 1;
        Maliyet.text = "Ücret:" + GameManager.Instance.simdiki.ToString();


        cameraTransform = Camera.main.transform;
        Sprite[] kResim = Resources.LoadAll<Sprite>("Levels");

        List<GameObject> cisimler = new List<GameObject>();

        foreach (Sprite Resimler in kResim)
        {
            GameObject cisim = Instantiate(LButton) as GameObject;
            cisim.GetComponent<Image>().sprite = Resimler;
            cisim.transform.SetParent(LPanel.transform, false);
            cisim.name = Resimler.name;
            cisimler.Add(cisim);
            LevelData level = new LevelData(Resimler.name);
            cisim.transform.GetChild(0).GetComponent<Text>().text = (level.BestTime != 0.0f) ? level.BestTime.ToString("f") : ("X");
            string sceneName = Resimler.name;
            cisim.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));

        }
        for (int i = 1; i < cisimler.Count; i++)
        {
            string test = PlayerPrefs.GetString(cisimler[i - 1].name);
            if (test == "0" || test == "")
            {
                cisimler[i].GetComponent<Button>().interactable = false;
            }
        }
        int TextureIndex = 0;
        Sprite[] sResim = Resources.LoadAll<Sprite>("Player");

        foreach (Sprite Resimler in sResim)
        {
            GameObject cisim = Instantiate(SButton) as GameObject;
            cisim.GetComponent<Image>().sprite = Resimler;
            cisim.transform.SetParent(SPanel.transform, false);

            int index = TextureIndex;
            cisim.GetComponent<Button>().onClick.AddListener(() => PlayerSkins(index));
            cisim.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ucretler[index].ToString();

            if ((GameManager.Instance.SimdikiSira & 1 << index) == 1 << index)
            {
                cisim.transform.GetChild(0).gameObject.SetActive(false);
            }           
            TextureIndex++;

        }
        PlayerSkins(GameManager.Instance.SimdikiSkin);
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if(cameraLook != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraLook.rotation, CAMERA_DONUS * Time.deltaTime);
        }
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraLook = menuTransform;
    }

    private void PlayerSkins(int index)
    {
        if ((GameManager.Instance.SimdikiSira & 1 << index) == 1 << index)
        {
            float x = (index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;

            if (y == 0)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.50f;
            else if (y == 0.50f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0;

            playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
            GameManager.Instance.SimdikiSkin = index;
            GameManager.Instance.Save();
        }
        else
        {
            int Ucret = ucretler[index];
            if(GameManager.Instance.simdiki >= Ucret)
            {
                GameManager.Instance.simdiki -= Ucret;
                GameManager.Instance.SimdikiSira += 1 << index;
                Maliyet.text = "Ücret:" + GameManager.Instance.simdiki.ToString();
                SPanel.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                GameManager.Instance.Save();
                PlayerSkins(index);
            }
        }
    }
}
