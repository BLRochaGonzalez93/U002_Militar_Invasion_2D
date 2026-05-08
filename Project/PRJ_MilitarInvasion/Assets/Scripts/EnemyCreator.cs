using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public class EnemyProperties
    {
        public int enemyId;
        public int enemyLives;
    }

    static public List<EnemyProperties> enemies = new()
    {
        new EnemyProperties() { enemyId = 0, enemyLives = 1 },
        new EnemyProperties() { enemyId = 1, enemyLives = 2 },
        new EnemyProperties() { enemyId = 2, enemyLives = 5 },
        new EnemyProperties() { enemyId = 3, enemyLives = 7 },
        new EnemyProperties() { enemyId = 4, enemyLives = 100 },
    };

    static public EnemyProperties CloneEnemy(EnemyProperties _enemy)
    {
        EnemyProperties newEnemy = new()
        {
            enemyId = _enemy.enemyId,
            enemyLives = _enemy.enemyLives
        };

        return newEnemy;
    }

    static public EnemyProperties GetEnemyById(int _id)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].enemyId == _id)
            {
                return CloneEnemy(enemies[i]);
            }
        }
        return null;
    }
}
