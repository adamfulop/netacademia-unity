using Project.Scripts.Signal;
using Project.Scripts.UI.Controller;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI {
    public class UIInstaller : MonoInstaller<UIInstaller> {
        public override void InstallBindings() {
            // Singleton UIController osztály a contexten belül
            Container.Bind<UIController>().AsSingle().NonLazy();
            // az elstütött PlayerScoreChangedSignalokat a UI Controller OnPlayerScoreChanged
            // metódusa kezeli le (meghívódik)
            Container.BindSignal<int, PlayerScoreChangedSignal>().To<UIController>(c => c.OnPlayerScoreChanged)
                .AsSingle();
            
            // a Text osztályt a GameObject-ről injecteljük
            Container.Bind<Text>().FromComponentSibling().WhenInjectedInto<UIPoints>();
        }
    }
}