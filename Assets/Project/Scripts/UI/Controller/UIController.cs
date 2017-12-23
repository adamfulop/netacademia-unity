using Zenject;

namespace Project.Scripts.UI.Controller {
    public class UIController {
        // a UIPoints-ot tartalmazó GameObjecten van egy ZenjectBinding komponens
        // annak a segítségével történik az inject (nem az installerben van definiálva)
        [Inject]
        public UIPoints UIPoints { private get; set; }
        
        public void OnPlayerScoreChanged(int newScore) {
            UIPoints.ShowScore(newScore);
        }
    }
}