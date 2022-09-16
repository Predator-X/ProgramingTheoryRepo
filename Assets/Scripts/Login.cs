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



    //  public Button button;
    // Start is called before the first frame update

    private void Awake()
    {
        checkText.gameObject.active = false;
        errorCanvas.active = false;
    }
    private void Update()
    {
        /*
        if(loginInput.text != null){
            string path = Application.persistentDataPath + "/" + loginInput.text + ".save";
            if (File.Exists(path))
            {
                //  checkText.text = "Welcome " + loginInput.text;
                // button.GetComponent<Text>().text = "Login";
                // Debug.Log("Path exists");
                button.text = "Login";
            }else if (!File.Exists(path))
            {
                button.text = "Sign IN ";
            }
        }
        */
       // checkText.text = "Welcome " + loginInput.text;
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
        string path = Application.persistentDataPath + "/" + loginInput.text + ".save";
        if (File.Exists(path))
        {
            UserData data = SaveSystem.LoadUserData(loginInput.text);

            if (loginInput.text.ToString() == data.username.ToString() && passportInput.text.ToString() == data.passport.ToString())
            {
                checkText.gameObject.active = true;
                checkText.text = "Login Suckesfull " + loginInput.text.ToString();
                Debug.Log("Login Suckcessfull " + loginInput.text);

                Load(sceneIndex);
            }
            else if (loginInput.text != data.username || passportInput.text != data.passport)
            {
                checkText.gameObject.active = true;
                checkText.text="Wrong Passport try Again";
            }
            
        }
        else if (!File.Exists(path) && !creatNewPressed)
        {
            errorCanvas.active = true;
            //checkText.gameObject.active = true;
            //checkText.text = loginInput.text+" Your Account Created ";
            //SaveSystem.SaveUserData(loginInput.text, passportInput.text);
        }
        else if(!File.Exists(path)&& creatNewPressed)
        {
            SaveSystem.SaveUserData(loginInput.text, passportInput.text);
            checkText.text = loginInput.text + " Your Account Created ";
            Load(sceneIndex);
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
        //Time.timeScale = 0f;
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
            
            loadingSceen.SetActive(false);


            Time.timeScale = 1.0f;

        }

    }
}
