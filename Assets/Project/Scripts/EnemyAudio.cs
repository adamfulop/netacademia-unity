using UnityEngine;

// zombi hangjáért felelős komponens
public class EnemyAudio : MonoBehaviour {
    public int AudioInterval = 5;        // milyen sűrűn játszódjon le a hang

    private float _lastAudioPlayTime = 0;    // mikor játszódott le utoljára a hang
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        // ha a jelenlegi idő - utolsó lejátszási idő nagyobb, mint a lejátszási időköz, akkor lejátsszuk újra a hangot
        if (Time.time - _lastAudioPlayTime > AudioInterval) {
            _lastAudioPlayTime = Time.time;
            _audioSource.Play();
        }
    }
}
