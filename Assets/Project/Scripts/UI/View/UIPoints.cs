using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIPoints : MonoBehaviour {
    [Inject]
    public Text Text { private get; set; }

    public void ShowScore(int newScore) {
        // minden updateben frissítjuk a pontszám kijelzőt a játékos pontszáma alapján
        Text.text = string.Format("Pontszám: {0} / 8", newScore);
    }
}