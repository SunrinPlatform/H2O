using UnityEngine;

namespace Entity {
    public class Unit : MonoBehaviour {
        [SerializeField] internal int maxHealth;
        [SerializeField] internal int currentHealth;
        [SerializeField] internal bool isDamage = false;
        [SerializeField] internal int attack;
        [SerializeField] internal int defence;

        virtual protected void Start() {
            currentHealth = maxHealth;
        }

        virtual protected void Update() {

        }
    }
}