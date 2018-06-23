using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float ShootSeconds;
    
    // Projectile
    public float ProjectileVelocity;
    public Projectile ProjectilePrefab;
    public Vector3 ProjectileOffset;

    private Animator _animator;
    private SpawnedObjectsContainer _spawnedObjectsContainer;
    private Transform _player;

    private bool _canShoot;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spawnedObjectsContainer = FindObjectOfType<SpawnedObjectsContainer>();
    }

    public void OnSpawn() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        InvokeRepeating("TryShoot", ShootSeconds, ShootSeconds);
    }

    public void OnDespawn() {
        if (IsInvoking("TryShoot")) {
            CancelInvoke("TryShoot");
        }
    }

    private void OnDisable() {
        if (IsInvoking("TryShoot")) {
            CancelInvoke("TryShoot");
        }
    }

    private void TryShoot() {
        if (_player != null) {
            _canShoot = true;
            foreach (var spawnedObject in _spawnedObjectsContainer.SpawnedObjects) {
                if (spawnedObject == transform || spawnedObject == transform.parent) continue;

                if (_player.transform.position.x < transform.position.x) {
                    _canShoot = !(spawnedObject.position.x > _player.transform.position.x &&
                                  spawnedObject.position.x < transform.position.x);
                } else {
                    _canShoot = !(spawnedObject.position.x < _player.transform.position.x &&
                                  spawnedObject.position.x > transform.position.x);
                }

                if (!_canShoot) break;
            }

            if (_canShoot) {
                _animator.SetTrigger("skill_2");
                var projectile = LeanPool.Spawn(ProjectilePrefab, transform.position + ProjectileOffset, Quaternion.identity);
                var direction = Mathf.Sign(_player.transform.position.x - transform.position.x);
                projectile.Velocity =
                    new Vector2(
                         direction * ProjectileVelocity - 160 / 2f, // világ sebesség / 2
                        0
                    );
            }
        }
    }

    private void Update() {
        if (_player != null && _player.position.x > transform.position.x) {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}