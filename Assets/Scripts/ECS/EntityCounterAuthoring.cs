using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Entities;

namespace UIECS
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        private EntityManager _em;
        
        private IEnumerator Start()
        {
            _em = World.DefaultGameObjectInjectionWorld.EntityManager;
            yield return new WaitForSeconds(1);
            Debug.Log("Run");
            StartCoroutine(UpdateEnemies());
        }

        private IEnumerator UpdateEnemies()
        {
            while (true)
            {
                int enemies = _em.CreateEntityQuery(ComponentType.ReadOnly<AsteroidECS.Asteroid>()).CalculateEntityCount();
                yield return new WaitForSeconds(0.1f);
                _text.text = "Asteroids: " + enemies;
            }
        }
    }
}

