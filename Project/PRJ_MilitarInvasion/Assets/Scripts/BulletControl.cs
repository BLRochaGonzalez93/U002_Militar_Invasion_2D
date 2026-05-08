using UnityEngine;

public enum BulletType { PLAYER, ENEMY, BLAST }

public class BulletControl : MonoBehaviour
{
    private BulletType type;
    private float speed;
    private int dir;

    private GameManager manager;

    public void InitBullet(BulletType _type, float _speed, GameManager _manager)
    {
        type = _type;
        speed = _speed;
        manager = _manager;
        if (_type == BulletType.PLAYER)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
    }

    public void InitBlast(BulletType _type, GameManager _manager)
    {
        type = _type;
        manager = _manager;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (type)
        {
            case BulletType.ENEMY:
                if (other.CompareTag("Player"))
                {
                    manager.GetDamagePlayer();
                    Destroy(gameObject);
                }
                break;
            case BulletType.PLAYER:
                if (other.CompareTag("Enemy"))
                {
                    manager.GetDamageEnemy(other.gameObject);
                    Destroy(gameObject);
                }
                break;
            case BulletType.BLAST:
                if (other.CompareTag("Player"))
                {
                    for (int i = manager.totalLives; i >= 0; i--)
                    {
                        manager.GetDamagePlayer();
                    }
                }
                break;
        }
    }
}
