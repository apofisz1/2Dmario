public class HighScoresButton : ButtonBehaviour<MainMenuController> {
    protected override void OnClick() {
        Controller.OnHighScoresClick();    // MainMenuControllerbe (generikus paraméter) továbbítjuk a hívást
    }
}