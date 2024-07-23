using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyMove : MonoBehaviour
{

    Vector2 pos;
    [SerializeField] float delta = 1.0f;
    [SerializeField] float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 5.0f);

        bool move = true;

        for (int i = 0; i < cols.Length; i++) {
            if (cols[i].gameObject.name == "Player") {
                move = false;
            }
        }
    
        if (move) {
            Vector2 v = pos;
            v.x += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }
    }
}
