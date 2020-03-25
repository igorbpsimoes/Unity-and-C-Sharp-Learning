using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

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
        if (input == "1") {
            level = 1;
            password = "book";
            StartTheGame();
        } else if (input == "2") {
            level = 2;
            password = "handcuffs";
            StartTheGame();
        }
    }
    private void StartTheGame() {
        currentScreen = Screen.Password;
        Terminal.WriteLine("You've chosen level " + level);
        Terminal.WriteLine("Please, enter you password");
    }

    void CheckPassword(string input) {
       if(input == password) {
            WinScreen();
       } else {
            Terminal.WriteLine("Wrong Password");
       }
    }

    private void WinScreen() {
        currentScreen = Screen.Win;
        Terminal.WriteLine("Right Password!");
    }
}
