using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TextMeshProUGUI _warning;
    [SerializeField] private Button _signInBut;

    private void Start()
    {
        _signInBut.onClick.AddListener(SignInBut);
    }


    public void SignInBut()
    {
        if (_name.text==""|| _password.text=="")
            _warning.text = "Все поля должны быть заполнены";
        else
            if (Account.SignIn(_name.text, _password.text))
                SceneManager.LoadScene(1);

    }


}
