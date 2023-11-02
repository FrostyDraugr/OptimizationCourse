using UnityEngine;
using System.Collections;
using TMPro;
using Unity.Entities;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

namespace UIECS
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private TMP_Text _textFPS;

        private EntityManager _em;
        
        private IEnumerator Start()
        {
            _em = World.DefaultGameObjectInjectionWorld.EntityManager;
            yield return new WaitForSeconds(1);
            Debug.Log("Run");
            StartCoroutine(UpdateEnemies());
        }

        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            //if(Input.GetKeyDown(KeyCode.R))
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}

        }

        private IEnumerator UpdateEnemies()
        {
            while (true)
            {
                int enemies = _em.CreateEntityQuery(ComponentType.ReadOnly<AsteroidECS.Asteroid>()).CalculateEntityCount();
                yield return new WaitForSeconds(0.1f);
                _text.text = "Asteroids: " + enemies;
                _textFPS.text = "FPS: " + (int)(1f/Time.unscaledDeltaTime);
            }
        }
    }
}

