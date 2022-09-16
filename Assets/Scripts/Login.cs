using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_Text checkText;
    public TMP_InputField loginInput , passportInput;
    public TextMeshProUGUI button;
    //  public Button button;
    // Start is called before the first frame update

    private void Awake()
    {
        checkText.gameObject.active = false;
    }
    private void Update()
    {
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
        
       // checkText.text = "Welcome " + loginInput.text;
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
            }
            else if (loginInput.text != data.username && passportInput.text != data.passport)
            {
                checkText.gameObject.active = true;
                checkText.text="Wrong Passport";
            }
            
        }
        else if (!File.Exists(path))
        {
            checkText.gameObject.active = true;
            checkText.text = loginInput.text+" Your Account Created ";
            SaveSystem.SaveUserData(loginInput.text, passportInput.text);
        }
    }
}
