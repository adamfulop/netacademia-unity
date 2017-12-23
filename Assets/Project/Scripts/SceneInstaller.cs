using Project.Scripts.Signal;
using Zenject;

namespace Project.Scripts {
    public class SceneInstaller : MonoInstaller<SceneInstaller> {
        public override void InstallBindings() {
            Container.DeclareSignal<PlayerScoreChangedSignal>();
        }
    }
}