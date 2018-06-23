public class StartGameButton : ButtonBehaviour<MainMenuController> {
    protected override void OnClick() {
        Controller.OnStartGameClick();
    }
}
