public class BackButton : ButtonBehaviour<HighScoresController> {
    protected override void OnClick() {
        Controller.OnBackClick();    // HighScroesControllerbe tovább hívunk
    }
}
