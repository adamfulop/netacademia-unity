using System;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    public float Speed = 10f;            // karakter haladási sebessége
    public float TurnSmoothing = 20f;    // kanyarodás sebessége
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _movement;

    private const float TOLERANCE = 0.01f;
    
    // más komponensek referenciáinak inicializálása
    // (sima GetComponent<>() -> ugyanazon a GameObjecten keressük a komponenseket)
    private void Awake() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // vízszintes és függőleges input tengely értékeinek elkérése (-1 és 1 közötti érték)
        // tengelyek leképzése alapból a nyilak vagy WASD
        // Lásd Edit -> Project Settings -> Input
        var inputVertical = Input.GetAxis("Vertical");
        var inputHorizontal = Input.GetAxis("Horizontal");

        Move(inputHorizontal, inputVertical);
        SetAnimation(inputHorizontal, inputVertical);
    }

    private void Move(float inputHorizontal, float inputVertical) {
        // a _movement az elmozdulás vektor az aktuális updateben
        // skálázzuk a sebességgel (Speed) és az előző update óta eltelt idővel (Time.deltaTime)
        _movement.Set(inputHorizontal, 0, inputVertical);
        _movement = _movement.normalized * Speed * Time.deltaTime;
        // alkalmazzuk az elmozdulást
        _rigidbody.MovePosition(transform.position + _movement);

        if (Math.Abs(inputHorizontal) > TOLERANCE || Math.Abs(inputVertical) > TOLERANCE) {    // "== 0" helyett "> tolerance", mert float
            // ha valamelyik input nem 0, akkor forgatunk is
            Rotate(inputHorizontal, inputVertical);
        }
    }

    private void Rotate(float inputHorizontal, float inputVertical) {
        // Quaternion részletesebb információ: https://docs.unity3d.com/ScriptReference/Quaternion.html
        // Matematikai alapok: https://hu.wikipedia.org/wiki/Kvaterni%C3%B3k
        var targetDirection = new Vector3(inputHorizontal, 0, inputVertical);             // cél irány
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);        // cél orientáció
        // interpolálunk a jelenlegi és cél orientáció között (3. paraméter 0-1 közötti szám kell legyen, hogy épp "hol tartunk" a fordulásban)
        var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.deltaTime * TurnSmoothing);
        // alkalmazzuk az elfordulást
        _rigidbody.MoveRotation(newRotation);
    }

    private void SetAnimation(float inputHorizontal, float inputVertical) {
        // animátor paraméter beállítása a bemenet függvényében
        var isRunning = Math.Abs(inputHorizontal) > TOLERANCE || Math.Abs(inputVertical) > TOLERANCE;    // igaz, ha valamelyik tengely nem 0
        _animator.SetBool("IsRunning", isRunning);
    }
}
