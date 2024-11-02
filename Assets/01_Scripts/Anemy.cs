using UnityEngine;
using Entity;

public class Anemy : Unit
{

    Vector2 pos;
    [SerializeField] float delta;
    [SerializeField] float speed;
    [SerializeField] float range;

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
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);

        bool move = true;

        for (int i = 0; i < cols.Length; i++) {
            if (cols[i].gameObject.name == "Player") {
                move = false;
                attack(cols[i].gameObject);
            }
        }
    
        if (move) {
            Vector2 v = pos;
            v.x += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }
    }

    void attack(GameObject obj)
    {
        Vector2 direction = (Vector2) obj.transform.position - (Vector2)transform.position;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
