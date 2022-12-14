using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyOrb
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class OrbSoundManager : MonoBehaviour
    {
        [SerializeField] private Vector2 velocityRange = new Vector2(0, 5);
        [SerializeField] private Vector2 collisionVelocityRange = new Vector2(3, 9);
        [Space]
        [SerializeField] private Vector2 pitchRange = new Vector2(1, 2);
        [SerializeField] private AnimationCurve pitchResponse = new();
        [Space]
        [SerializeField] private Vector2 volumeRange = new Vector2(0, 1);
        [SerializeField] private AnimationCurve volumeResponse = new();
        [SerializeField] private float inAirFadeSpeed = 40;
        [SerializeField] private AudioSource loopSource;
        [Space]
        [SerializeField] private AudioSource collisionSource;
        [SerializeField] private Vector2 collisionPitchRange = new Vector2(1,1.5f);
        [SerializeField] private AnimationCurve collisionResponse = new();
        // Variables
        private Rigidbody _RB;
        private List<GameObject> _colliders = new();
        private float _collisionBasedVolume = 0;
        // Start is called before the first frame update
        void Start()
        {
            _RB = GetComponent<Rigidbody>();
            loopSource.volume = 0;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 velocity = _RB.velocity;

            float normalizedMagnitude = Normalize(velocity.magnitude, velocityRange[0], velocityRange[1]);

            loopSource.pitch = Mathf.Lerp(pitchRange[0], pitchRange[1], pitchResponse.Evaluate(normalizedMagnitude));
            float speedBasedVolume = Mathf.Lerp(volumeRange[0], volumeRange[1], volumeResponse.Evaluate(normalizedMagnitude));

            _collisionBasedVolume = IsOrbInMidAir() ? Mathf.Lerp(_collisionBasedVolume, 0, Time.deltaTime * inAirFadeSpeed): 1;

            loopSource.volume = speedBasedVolume * _collisionBasedVolume;
            

        }

        private float Normalize(float value, float from1, float to1)
        {
            return (value - from1) / (to1 - from1) * (1 - 0) + 0;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!_colliders.Contains(collision.gameObject))
            {
                _colliders.Add(collision.gameObject);
            }
            if (collision.relativeVelocity.magnitude > collisionVelocityRange[0])
            {
                PlayCollisionSound(collision);
            }
        }

        private void PlayCollisionSound(Collision collision)
        {
            float normalizedCollisionMagnitude = Normalize(Mathf.Clamp(collision.relativeVelocity.magnitude,0, collisionVelocityRange[1]),0, collisionVelocityRange[1]);
            collisionSource.pitch = Random.Range(collisionPitchRange[0], collisionPitchRange[1]) + normalizedCollisionMagnitude / 0.3f;

            collisionSource.volume = collisionResponse.Evaluate(normalizedCollisionMagnitude);
            collisionSource.PlayOneShot(collisionSource.clip);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (_colliders.Contains(collision.gameObject))
            {
                _colliders.Remove(collision.gameObject);
            }

        }
        private bool IsOrbInMidAir() => _colliders.Count < 1;
    }
}
