using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_Text checkText;
    public TMP_InputField loginInput , passportInput;
    public TextMeshProUGUI button;
    public GameObject errorCanvas;
    bool creatNewPressed = false;


    //For Loading
    public GameObject loadingSceen;
    public Slider slider;
    public Text progressText;
    bool loadDone = false;
    public int sceneIndex;

    //Json
    [SerializeField] string filename;

  

    private void Awake()
    {
        checkText.gameObject.active = false;
        errorCanvas.active = false;

   
    }


    private void Update()
    {
       
        
            SaveSystem.setUserName( loginInput.text.ToString());
        
    }

    public void TryAgainButtonOnClick()
    {
        errorCanvas.active = false;
    }

    public void CreateNewUserOnClick()
    {
        creatNewPressed = true;
        checkText.gameObject.active = true;
        checkText.text = "Creat New Account";
        button.text = "Sign In";
        errorCanvas.active = false;
       

      
    }



    public  void OnLoginClicked()
    {
        if (!File.Exists(JsonHelper.GetPath(filename)))
        {
            SaveSystem.justCreatedNewAccount = true;

            List<PlayerAchivments> pA = new List<PlayerAchivments>();

         
            PlayerAchivments thisPlayer = new PlayerAchivments(loginInput.text.ToString(), 0, 0, 0);

            pA.Add(thisPlayer);
            JsonHelper.SaveToJSON<PlayerAchivments>(pA, filename);
        }

        SaveSystem.setUserName(loginInput.text);
        
        Debug.Log("################## UserName:" + loginInput.text);
        string path = Application.persistentDataPath + "/" + loginInput.text + "UserDataLib.save";
       


        if (File.Exists(path))
        {
           
            UserData data = SaveSystem.LoadUserData(loginInput.text);
     
        
                if (loginInput.text.ToString() == data.username.ToString() && passportInput.text.ToString() == data.passport.ToString())
                {
                    SaveSystem.UserName = data.username.ToString();
                    checkText.gameObject.active = true;
                    checkText.text = "Login Suckesfull " + loginInput.text.ToString();
                    Debug.Log("Login Suckcessfull " + loginInput.text);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>().SetMainMenuON();
                Load(sceneIndex);
                }
                else if (loginInput.text != data.username || passportInput.text != data.passport)
                {
                    checkText.gameObject.active = true;
                    checkText.text = "Wrong Passport try Again";
                }

            
        }
        else if (!File.Exists(path) && !creatNewPressed)
        {
            errorCanvas.active = true;
          
        }
        else if (!File.Exists(path) && creatNewPressed)
        {
            SaveSystem.setUserName(loginInput.text);
            SaveSystem.SaveUserData(loginInput.text, passportInput.text.ToString());

          

            checkText.text = loginInput.text + " Your Account Created ";
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>().SetMainMenuON();
            Load(sceneIndex);
            SaveSystem.justCreatedNewAccount = true;
            creatNewPressed = false;
        }

      
        
          
    }



    public void Load(int sceneIndex)
    {
        loadingSceen.active = true;
        loadDone = false;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIdnex)
    {
       
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdnex);
        loadingSceen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        if (operation.isDone)
        {
            SaveSystem.setUserName(loginInput.text);
            loadingSceen.SetActive(false);


            Time.timeScale = 1.0f;

        }

    }
}