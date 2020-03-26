using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game Configuration Data
    const string menuHint = "You may type 'menu' at  any time to return to it!";
    string[] level1Passwords = { "luke", "gear", "r2d2", "c-3po", "wing"};
    string[] level2Passwords = { "han solo", "computer", "lasergun", "arrested", "uniforms"};
    string[] level3Passwords = { "stormtroopers", "darth vader", "unlimited power", "rocket launcher", "galactic empire"};

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
        Terminal.WriteLine("Press 1 for Luke's X-Wing");
        Terminal.WriteLine("Press 2 for Millennium Falcon");
        Terminal.WriteLine("Press 3 for DEATH STAR");
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
        bool isValidLevel = (input == "1" || input == "2" || input == "3");
        if(isValidLevel) {
            level = int.Parse(input);
            AskForPassword();
        } else {
            Terminal.WriteLine("Please choose a valid level");
        }
    }
    private void AskForPassword() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Enter you password: (hint: '" + password.Anagram() + "')");
    }

    private void SetRandomPassword() {
        switch (level) {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid Level Number"); //Shouldn't reach this ever
                break;
        }
    }

    void CheckPassword(string input) {
       if(input == password) {
            DisplayWinScreen();
       } else {
            Terminal.WriteLine("Wrong Password");
       }
    }

    void DisplayWinScreen() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine(menuHint);
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward() {
        switch(level) {
            case 1:
                Terminal.WriteLine("Luke's X-Wing is yours...");
                Terminal.WriteLine(@"
--)-----------|____________|
                                              ,'       ,'
                -)------========            ,'  ____ ,'
                         `.    `.         ,'  ,'__ ,'
                           `.    `.     ,'       ,'
                             `.    `._,'_______,'__
                               [._ _| ^--      || |
                       ____,...-----|__________ll_|\
      ,.,..-------'''''     '----'                 ||
  .-''  |=========================== ______________ |
   '-...l_______________________    |  |'      || |_]
                                [`-.|__________ll_|      
                              ,'    ,' `.        `.      
                            ,'    ,'     `.    ____`.    
                -)---------========        `.  `.____`.
                                             `.        `.
                                               `.________`.
                               --)-------------|___________|
                ");
                break;
            case 2:
                Terminal.WriteLine("Millenium Falcon is yours...");
                Terminal.WriteLine(@"
              c==o
              _/____\_
       _.,--'' ||^ || '`z._
      /_/^ ___\||  || _/o\ '`-._
    _/  ]. L_| || .||  \_/_  . _`--._
   /_~7  _ . ' ||. || /] \ ]. (_)  . '`--.
  |__7~.(_)_ []|+--+|/____T_____________L|
  |__|  _^(_) /^   __\____ _   _|
  |__| (_){_) J ]K{__ L___ _   _]
  |__| . _(_) \v     /__________|________
  l__l_ (_). []|+-+-<\^   L  . _   - ---L|
   \__\    __. ||^l  \Y] /_]  (_) .  _,--'
     \~_]  L_| || .\ .\\/~.    _,--''
      \_\ . __/||  |\  \`-+-<''
        '`---._|J__L|X o~~|[\\ 
               \____/ \___|[
                `--'   `--+-'
                ");
                break;
            case 3:
                Terminal.WriteLine("The Death Star is yours...");
                Terminal.WriteLine(@"
          .          .
 .          .                  .          .              .
       +.           _____  .        .        + .                    .
   .        .   ,-~'     '~-.                                +
              ,^ ___         ^. +                  .    .       .
             / .^   ^.         \         .      _ .
            Y  l  o  !          Y  .         __CL\H--.
    .       l_ `.___.'        _,[           L__/_\H' \\--_-          +
            |^~''-----------''~ ^|       +    __L_(=): ]-_ _-- -
  +       . !                   !     .     T__\ /H. //---- -       .
         .   \                 /               ~^-H--'
              ^.             .^            .      '      +.
                '-.._____.,-' .                    .
         +           .                .   +                       .
  +          .             +                                  .
                ");
                break;
            default:
                Debug.LogError("Invalid Level Reached");
                break;
        }
    }
}
