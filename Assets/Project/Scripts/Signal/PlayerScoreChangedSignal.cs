using Zenject;

namespace Project.Scripts.Signal {
    // Jelzés, ha változott a lokális játékos pontszáma
    public class PlayerScoreChangedSignal : Signal<PlayerScoreChangedSignal, int> {}
}