public class HighScoresButton : ButtonBehaviour<MainMenuController> {
    protected override void OnClick() {
        Controller.OnHighScoresClick();
    }
}
