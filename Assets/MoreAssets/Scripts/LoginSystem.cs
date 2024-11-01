using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text feedbackText;

    public TMP_InputField loginUsername;
    public TMP_InputField loginPassword;
    public TMP_Text loginFeedbackText;

    private string filepath;

    // Start is called before the first frame update
    void Start()
    {
        // define where user credentials are stored
        filepath = Application.persistentDataPath + "/LoginText.txt";
    }

    public void Signup()
    {
        string email = emailInput.text;
        string username = usernameInput.text;
        string password = passwordInput.text;

        if(!UserExists(username))
        {
            SaveUserData(email, username, password);
            Debug.Log("Success");
            feedbackText.text = "Account Creation Successful";
        }
        else
        {
            Debug.Log("Already exists");
            feedbackText.text = "Username Already Exists";
        }
    }

    private bool UserExists(string username)
    {
        if(File.Exists(filepath))
        {
            string[] users = File.ReadAllLines(filepath);
            foreach (string user in users)
            {
                string[] userDetails = user.Split(',');
                if (userDetails[1] == username)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SaveUserData(string email, string username, string password)
    {
        string userData = email + "," + username + "," + password;
        File.AppendAllText(filepath, userData + "\n");
    }

    public void Login()
    {
        string username = loginUsername.text;
        string password = loginPassword.text;

        if (CheckUserCredentials(username, password))
        {
            loginFeedbackText.text = "Login Successful";
            Debug.Log("Success");
            ChangeScene();
        }
        else
        {
            loginFeedbackText.text = "Invalid Credentials";
            Debug.Log("failed");
        }
    }   
    
    private bool CheckUserCredentials(string username, string password)
    {
        if (File.Exists(filepath))
        {
            string[] users = File.ReadAllLines(filepath);
            foreach (string user in users)
            {
                string[] userDetails = user.Split(',');
                if (userDetails[1] == username && userDetails[2] == password)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LogOut()
    {
        SceneManager.LoadScene(0);
    }

    public void TimerTool()
    {
        SceneManager.LoadScene(2);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(1);
    }

    public void RSP()
    {
        SceneManager.LoadScene(3);
    }

    public void SettingsPage()
    {
        SceneManager.LoadScene(4);
    }
    
}
