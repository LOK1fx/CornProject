using LOK1game.Character;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LOK1game
{
    public class CharacterSpawnPoint : MonoBehaviour
    {
        public string Id => _id;

        private string _id;

        public Vector3 Position => transform.position;
        public bool AllowNeutral => _allowNeutral;
        public bool AllowPlayer => _allowPlayer;
        public bool AllowEnemy => _allowEnemy;
        public bool AllowNPC => _allowNPC;
        public bool AllowSpectator => _allowSpectator;

        [Header("Flags")]
        [SerializeField] private bool _allowNeutral;
        [SerializeField] private bool _allowPlayer;
        [SerializeField] private bool _allowEnemy;
        [SerializeField] private bool _allowNPC;
        [SerializeField] private bool _allowSpectator;
        
        private List<Actor> _spawnedAtPoint = new List<Actor>();

        [Space]
        [Header("EDITOR")]
        [SerializeField] private GUIStyle _noIdLabelStyle;
        [SerializeField] private GUIStyle _idLabelStyle;


        public Actor SpawnActor(Actor actor)
        {
            var newActor = Instantiate(actor, transform.position, transform.rotation);

            ValidateCharacter(newActor);

            _spawnedAtPoint.Add(newActor);

            return newActor;
        }

        public Actor FalseSpawn(Actor actor, bool applyRotation = false)
        {
            actor.transform.position = transform.position;

            if (applyRotation)
            {
                actor.transform.rotation = transform.rotation;
            }

            ValidateCharacter(actor);

            if (_spawnedAtPoint.Contains(actor))
            {
                _spawnedAtPoint.Remove(actor);
            }

            return actor;
        }

        private void ValidateCharacter(Actor actor)
        {
            if (actor.TryGetComponent<ICharacter>(out var character))
            {
                character.OnSpawnedOnSpawnPoint(this);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.1f);

#if UNITY_EDITOR

            var label = string.IsNullOrEmpty(Id) ? "<size=45>!</size> \nNO SPAWN POINT's ID!" : $"SP:{Id}";
            var style = string.IsNullOrEmpty(Id) ? _noIdLabelStyle : _idLabelStyle;

            Handles.Label(transform.position + Vector3.up * 0.5f, label, style);
            
#endif
        }

#if UNITY_EDITOR

        public void Editor_GenerateNewId()
        {
            _id = GUID.Generate().ToString();
            EditorUtility.SetDirty(this);

            Debug.Log($"new Id generated - {Id}");
        }

#endif
    }
}