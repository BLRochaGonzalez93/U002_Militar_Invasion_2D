using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    public class WorldProperties
    {
        public string name;
        public int id;
        public int playerLives;
        public int seconds;

        public int columns;
        public List<int> enemiesIds;
    }

    static public List<WorldProperties> worlds = new()
    {
        new WorldProperties()
        {
            name = "World 1",
            id = 0,
            playerLives = 5,
            seconds = 20,
            columns = 5,
            enemiesIds = new List<int>() { 0, 0},
        },

        new WorldProperties()
        {
            name = "World 2",
            id = 1,
            playerLives = 4,
            seconds = 40,
            columns = 6,
            enemiesIds = new List<int>() { 1, 1, 0 },
        },

        new WorldProperties()
        {
            name = "World 3",
            id = 2,
            playerLives = 3,
            seconds = 60,
            columns = 5,
            enemiesIds = new List<int>() { 2, 2, 1, 0 },
        },

        new WorldProperties()
        {
            name = "World 4",
            id = 3,
            playerLives = 3,
            seconds = 90,
            columns = 7,
            enemiesIds = new List<int>() { 3, 2, 1, 2 },
        },

        new WorldProperties()
        {
            name = "World 5",
            id = 4,
            playerLives = 5,
            seconds = 120,
            columns = 10,
            enemiesIds = new List<int>() { 3, 2, 1, 2, 0, 0 },
        },

        new WorldProperties()
        {
            name = "Boss",
            id = 5,
            playerLives = 7,
            seconds = 120,
            columns = 1,
            enemiesIds = new List<int>() { 4 },
        }
    };

    static public WorldProperties GetWorldById(int _id)
    {
        for (int i = 0; i < worlds.Count; i++)
        {
            if (worlds[i].id == _id)
            {
                return worlds[i];
            }
        }
        return null;
    }
}
