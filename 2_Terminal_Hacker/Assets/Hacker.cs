using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game Configuration Data
    string[] level1Passwords = { "book", "shelf", "table", "cabin", "font"};
    string[] level2Passwords = { "policeman", "handcuffs", "vehicles", "arrested", "uniforms"};

    //Game State
    int level;
    string password;
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;

    // Start is called before the first frame update
    void Start() {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Enter your selection:");
    }
    void OnUserInput(string input) {
        if(input == "menu") {
            ShowMainMenu();
        } else if(currentScreen == Screen.MainMenu) {
            RunMainMenu(input);
        } else if(currentScreen == Screen.Password) {
            CheckPassword(input);
        } else if(currentScreen == Screen.Win) {

        }
    }

    void RunMainMenu(string input) {
        bool isValidLevel = (input == "1" || input == "2");
        if(isValidLevel) {
            level = int.Parse(input);
            StartTheGame();
        } else {
            Terminal.WriteLine("Please choose a valid level");
        }
    }
    private void StartTheGame() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        switch(level) {
            case 1:
                password = level1Passwords[0];
                break;
            case 2:
                password = level2Passwords[0];
                break;
            default:
                Debug.LogError("Invalid Level Number"); //Shouldn't reach this ever
                break;
        }
        Terminal.WriteLine("Please, enter you password:");
    }

    void CheckPassword(string input) {
       if(input == password) {
            Terminal.WriteLine("Right Password!");
            WinScreen();
       } else {
            Terminal.WriteLine("Wrong Password");
       }
    }

    private void WinScreen() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
    }
}
