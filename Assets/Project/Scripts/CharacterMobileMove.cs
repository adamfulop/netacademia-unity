using System;
using CnControls;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterMobileMove : NetworkBehaviour {
public CharacterSettings CharacterSettings;
    
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _movement;

    private const float TOLERANCE = 0.01f;

    [SyncVar]
    private bool _isMoving;
    
    // más komponensek referenciáinak inicializálása
    // (sima GetComponent<>() -> ugyanazon a GameObjecten keressük a komponenseket)
    private void Awake() {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (isLocalPlayer) {
            // vízszintes és függőleges input tengely értékeinek elkérése (-1 és 1 közötti érték)
            // tengelyek leképzése alapból a nyilak vagy WASD
            // Lásd Edit -> Project Settings -> Input
            var inputVertical = CnInputManager.GetAxis("Vertical");
            var inputHorizontal = CnInputManager.GetAxis("Horizontal");

            Move(inputHorizontal, inputVertical);    
            CmdSetIsMoving(inputHorizontal, inputVertical);
        }
        
        SetAnimation();
        SetAudio();
    }
    
    private void SetAudio() {
        // ha mozog a játékos, és épp nem megy a lépés hang, akkor lejátsszuk
        // ha nem mozog a játékos, és épp megy a lépés hang, akkor leállítjuk
        if (_isMoving) {
            if (!_audioSource.isPlaying) _audioSource.Play();
        } else {
            if (_audioSource.isPlaying) _audioSource.Stop();
        }
    }

    private void Move(float inputHorizontal, float inputVertical) {
        // a _movement az elmozdulás vektor az aktuális updateben
        // skálázzuk a sebességgel (Speed) és az előző update óta eltelt idővel (Time.deltaTime)
        _movement.Set(inputHorizontal, 0, inputVertical);
        _movement = _movement.normalized * CharacterSettings.Speed * Time.deltaTime;
        // alkalmazzuk az elmozdulást
        _rigidbody.MovePosition(transform.position + _movement);

        if (_isMoving) {    // "== 0" helyett "> tolerance", mert float
            // ha valamelyik input nem 0, akkor forgatunk is
            Rotate(inputHorizontal, inputVertical);
        }
    }

    // true, ha épp mozog a játékos (legalább egyik input nem 0), egyébként false
    [Command]
    private void CmdSetIsMoving(float inputHorizontal, float inputVertical) {
        _isMoving = Math.Abs(inputHorizontal) > TOLERANCE || Math.Abs(inputVertical) > TOLERANCE;
    }

    private void Rotate(float inputHorizontal, float inputVertical) {
        // Quaternion részletesebb információ: https://docs.unity3d.com/ScriptReference/Quaternion.html
        // Matematikai alapok: https://hu.wikipedia.org/wiki/Kvaterni%C3%B3k
        var targetDirection = new Vector3(inputHorizontal, 0, inputVertical);             // cél irány
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);        // cél orientáció
        // interpolálunk a jelenlegi és cél orientáció között (3. paraméter 0-1 közötti szám kell legyen, hogy épp "hol tartunk" a fordulásban)
        var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.deltaTime * CharacterSettings.TurnSmoothing);
        // alkalmazzuk az elfordulást
        _rigidbody.MoveRotation(newRotation);
    }

    private void SetAnimation() {
        // animátor paraméter beállítása a bemenet függvényében
        _animator.SetBool("IsRunning", _isMoving);
    }
}
