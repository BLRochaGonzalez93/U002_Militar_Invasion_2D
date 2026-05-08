using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public float playerSpeed;
    public int totalLives;
    private GameObject player, background;
    private float totalSeconds;

    [Header("Worlds")]
    public Vector2 limitsStage;
    public Transform worldsGrid, UIGrid;
    public GameObject baseWorld, generalCanvas, UIBar;
    private WorldCreator.WorldProperties currentWorld;


    [Header("Enemies")]
    public Vector2 initialEnemyPos;
    public class EnemyProperties
    {
        public GameObject enemyObj;
        public EnemyCreator.EnemyProperties enemy;

        public EnemyProperties(GameObject _obj, EnemyCreator.EnemyProperties _enemy)
        {
            enemyObj = _obj;
            enemy = _enemy;
        }
    }
    public enum EnemyDir { RIGHT, LEFT }
    public List<EnemyProperties> listEnemies;
    private float enemySpeed, totalEnemySpeed, enemyFireRate;
    private EnemyDir direction;
    private Vector2 bossLimitStageX;
    private Vector2 bossLimitStageY;
    private float bossTimer;
    private float bossFireRate;
    Vector3 currentPointXY;

    private float blastSpeed;

    [Header("Stage")]
    public bool isPaused;
    public GameObject pausePanel;

    public GameObject winPanel, losePanel, livesBarBG, livesBar;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        PrintMenu();
        isPaused = false;
        currentPointXY = new(0, 2, 0);
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseResumeGame();
        }


        if (player != null)
        {
            UIBar.transform.Find("World").GetComponent<TextMeshProUGUI>().text = currentWorld.name;
            UIBar.transform.Find("Lives").GetComponent<TextMeshProUGUI>().text = "Lives: " + totalLives;
            UIBar.transform.Find("Seconds").GetComponent<TextMeshProUGUI>().text = "Seconds: " + ((int)totalSeconds + 1);

            bossTimer += Time.deltaTime;
            bossFireRate += Time.deltaTime;
            blastSpeed += Time.deltaTime;
            PlayerMovement();
            if (listEnemies.Count > 0)
            {
                EnemyMovement();
                EnemyAttack();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    PauseResumeGame();
                }
                else
                {
                    ClearStage();
                }
            }

            totalSeconds -= Time.deltaTime;
            if (totalSeconds <= 0)
            {
                ClearStage();
            }
        }
    }

    //Metodo que genera la Fase
    public void PrintMenu()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        generalCanvas.SetActive(true);
        UIBar.SetActive(false);
        livesBarBG.SetActive(false);
        livesBar.SetActive(false);
        for (int i = worldsGrid.childCount - 1; i >= 0; i--)
        {
            Destroy(worldsGrid.GetChild(i).gameObject);
        }
        for (int i = 0; i < WorldCreator.worlds.Count; i++)
        {
            GameObject newWorld = Instantiate(baseWorld, worldsGrid);
            newWorld.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = WorldCreator.worlds[i].name;
            newWorld.transform.Find("Lives").GetComponent<TextMeshProUGUI>().text = "Lives: " + WorldCreator.worlds[i].playerLives;
            newWorld.transform.Find("Seconds").GetComponent<TextMeshProUGUI>().text = "Seconds: " + WorldCreator.worlds[i].seconds;
            newWorld.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Worlds/" + WorldCreator.worlds[i].id);

            int tempId = WorldCreator.worlds[i].id;
            newWorld.GetComponent<Button>().onClick.AddListener(delegate { PrintStage(tempId); });


            // Comprobacion de las estrellas que tienes
            int totalStars = 0;
            if (PlayerPrefs.HasKey("World_" + WorldCreator.worlds[i].id) == true)
            {
                totalStars = PlayerPrefs.GetInt("World_" + WorldCreator.worlds[i].id);
            }
            newWorld.transform.Find("Stars").GetComponent<TextMeshProUGUI>().text = "Stars: " + totalStars + "/3";
        }
    }

    private void PrintStage(int _idWorld)
    {
        //Quitar el Menú
        generalCanvas.SetActive(false);
        UIBar.SetActive(true);


        //Imprimir Mundo
        currentWorld = WorldCreator.GetWorldById(_idWorld);
        totalLives = currentWorld.playerLives;
        totalSeconds = currentWorld.seconds;
        if(WorldCreator.GetWorldById(_idWorld).name == "Boss")
        {
            livesBarBG.SetActive(true);
            livesBar.SetActive(true);
        }


        //Imprimir el Fondo
        background = GameObject.CreatePrimitive(PrimitiveType.Quad);
        background.name = "Fondo";
        background.transform.position = new Vector2(0f, 0f);
        Destroy(background.GetComponent<MeshCollider>());
        background.GetComponent<Renderer>().enabled = false;

        GameObject backgroundSprite = new("Background Sprite");
        backgroundSprite.transform.SetParent(background.transform);
        backgroundSprite.transform.localScale = new Vector3(3.05f, 3.05f, 3.05f);
        backgroundSprite.transform.localPosition = new Vector2(0, 0);
        backgroundSprite.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Worlds/Warfield" + currentWorld.id);
        backgroundSprite.GetComponent<SpriteRenderer>().sortingOrder = -10;

        //Imprimir el Jugador
        player = GameObject.CreatePrimitive(PrimitiveType.Quad);
        player.name = "Player";
        player.transform.position = new Vector2(0f, -7f);
        Destroy(player.GetComponent<MeshCollider>());
        StartCoroutine(AddCollider(player, 99));
        player.GetComponent<Renderer>().enabled = false;
        player.tag = "Player";

        GameObject playerSprite = new("Player Sprite");
        playerSprite.transform.SetParent(player.transform);
        playerSprite.transform.localScale = new Vector3(4f, 4f, 4f);
        playerSprite.transform.localPosition = new Vector2(0, 0);
        playerSprite.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player/0");

        player.AddComponent<AnimControl>().InitAnim(playerSprite.GetComponent<SpriteRenderer>(), new List<Sprite>(Resources.LoadAll<Sprite>("Player")), 0.4f, AnimType.LOOP);

        //Imprimir Enemigos
        listEnemies = new List<EnemyProperties>();
        totalEnemySpeed = 0.2f;

        for (int i = 0; i < currentWorld.enemiesIds.Count; i++)
        {
            for (int j = 0; j < currentWorld.columns; j++)
            {
                GameObject newEnemy = GameObject.CreatePrimitive(PrimitiveType.Quad);
                newEnemy.name = "Enemy_" + currentWorld.enemiesIds[i];

                switch (currentWorld.enemiesIds[i])
                {
                    case 0:
                        newEnemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        break;
                    case 1:
                        newEnemy.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        break;
                    case 2:
                        newEnemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        break;
                    case 3:
                        newEnemy.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                        break;
                    case 4:
                        newEnemy.transform.localScale = new Vector3(2f, 2f, 2f);
                        break;
                }

                if(currentWorld.enemiesIds[i] == 4)
                {
                    newEnemy.transform.position = new Vector2(0, 4);
                    
                }
                else
                {
                    newEnemy.transform.position = new Vector2(initialEnemyPos.x + j * 1.2f, initialEnemyPos.y - i * 1.5f);
                }
                Destroy(newEnemy.GetComponent<MeshCollider>());
                StartCoroutine(AddCollider(newEnemy, currentWorld.enemiesIds[i]));
                newEnemy.GetComponent<MeshRenderer>().enabled = false;
                newEnemy.tag = "Enemy";

                GameObject spriteEnemy = new(newEnemy.name);
                spriteEnemy.transform.SetParent(newEnemy.transform);
                spriteEnemy.transform.localScale = new Vector3(4f, 4f, 4f);
                spriteEnemy.transform.localPosition = new Vector2(0, 0);
                spriteEnemy.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemies/" + currentWorld.enemiesIds[i] + "/" + currentWorld.enemiesIds[i] + "_1");

                EnemyCreator.EnemyProperties tempEnemy = EnemyCreator.GetEnemyById(currentWorld.enemiesIds[i]);

                newEnemy.AddComponent<AnimControl>().InitAnim(spriteEnemy.GetComponent<SpriteRenderer>(), new List<Sprite>(Resources.LoadAll<Sprite>("Enemies/" + currentWorld.enemiesIds[i].ToString())), 0.4f, AnimType.LOOP);

                listEnemies.Add(new EnemyProperties(newEnemy, tempEnemy));
            }
        }
    }

    private void ClearStage()
    {
        for (int i = 0; i < listEnemies.Count; i++)
        {
            Destroy(listEnemies[i].enemyObj);
        }
        listEnemies = new List<EnemyProperties>();
        Destroy(player);
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < allBullets.Length; i++)
        {
            Destroy(allBullets[i]);
        }
        Destroy(background);
        PrintMenu();
    }

    //Modificacion del Collider
    IEnumerator AddCollider(GameObject _obj, int _id)
    {
        yield return new WaitForSeconds(0.1f);
        if (_obj != null)
        {
            _obj.AddComponent<BoxCollider2D>().isTrigger = true;
            _obj.AddComponent<Rigidbody2D>().gravityScale = 0;

            switch (_id)
            {
                case 0:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.1f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(2.2f, 1.75f);
                    break;
                case 1:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.1f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(2.2f, 1.75f);
                    break;
                case 2:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.12f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(1.6f, 2.4f);
                    break;
                case 3:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.05f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 2.2f);
                    break;
                case 4:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.1f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(1.9f, 1.8f);
                    break;
                case 5:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0.13f, 0f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 16f);
                    break;
                case 98:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(0.12f, 0.4f);
                    break;
                case 99:
                    _obj.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.07f);
                    _obj.GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 0.6f);
                    break;
            }
        }
    }

    //Movimiento del Jugador
    private void PlayerMovement()
    {
        player.transform.Translate(Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime * Vector2.right);
        Vector2 currentPosition = player.transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, limitsStage.x, limitsStage.y);
        player.transform.position = currentPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet(BulletType.PLAYER, player.transform.position, 12, 99);
        }
    }

    //Movimiento del enemigo
    private void EnemyMovement()
    {
        enemySpeed += Time.deltaTime;
        if (enemySpeed >= totalEnemySpeed)
        {
            if (currentWorld.id == 5)
            {
                GameObject newEnemyBlast = GameObject.CreatePrimitive(PrimitiveType.Quad);
                newEnemyBlast.name = "New Enemy Blast";
                newEnemyBlast.transform.position = new Vector2(Random.Range(-13f, 13f), 0);
                Destroy(newEnemyBlast.GetComponent<MeshCollider>());
                newEnemyBlast.GetComponent<MeshRenderer>().enabled = false;
                newEnemyBlast.tag = "Enemy";
                if (blastSpeed >= 2.5)
                {
                    GameObject spriteEnemyBlast = new(newEnemyBlast.name);
                    spriteEnemyBlast.name = "Sprite_Warning";
                    spriteEnemyBlast.transform.SetParent(newEnemyBlast.transform);
                    spriteEnemyBlast.transform.localScale = new Vector3(25f, 50f, 15f);
                    spriteEnemyBlast.transform.localPosition = new Vector2(0, 0);
                    spriteEnemyBlast.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemies/Blast/Warning/0");

                    newEnemyBlast.AddComponent<AnimControl>().InitAnim(spriteEnemyBlast.GetComponent<SpriteRenderer>(), new List<Sprite>(Resources.LoadAll<Sprite>("Enemies/Blast/Warning")), 0.2f, AnimType.ONCE);
                    StartCoroutine(ExampleCoroutine(newEnemyBlast.transform.position));
                    blastSpeed = 0;
                }
                else
                {
                    Destroy(newEnemyBlast);
                }

                IEnumerator ExampleCoroutine(Vector3 _vect)
                {
                   yield return new WaitForSeconds(1.25f);
                    GameObject newEnemyBlast2 = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    newEnemyBlast2.name = "New Enemy Blast2";
                    newEnemyBlast2.transform.position = _vect;
                    Destroy(newEnemyBlast2.GetComponent<MeshCollider>());
                    StartCoroutine(AddCollider(newEnemyBlast2, 5));
                    newEnemyBlast2.GetComponent<MeshRenderer>().enabled = false;
                    newEnemyBlast2.tag = "Enemy";

                    GameObject spriteEnemyBlastShoot = new(newEnemyBlast2.name);
                    spriteEnemyBlastShoot.name = "Sprite_Shoot";
                    spriteEnemyBlastShoot.transform.SetParent(newEnemyBlast2.transform);
                    spriteEnemyBlastShoot.transform.localScale = new Vector3(25f, 50f, 15f);
                    spriteEnemyBlastShoot.transform.localPosition = new Vector2(0, 0);
                    spriteEnemyBlastShoot.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Enemies/Blast/Shoot/0");

                    newEnemyBlast2.AddComponent<AnimControl>().InitAnim(spriteEnemyBlastShoot.GetComponent<SpriteRenderer>(), new List<Sprite>(Resources.LoadAll<Sprite>("Enemies/Blast/Shoot")), 0.2f, AnimType.ONCE);

                    newEnemyBlast2.AddComponent<BulletControl>().InitBlast(BulletType.BLAST, this);

                }
            }

            for (int i = 0; i < listEnemies.Count; i++)
            {
                if(listEnemies[i].enemy.enemyId == 4)
                {
                    //bossTimer += Time.deltaTime;
                    bossLimitStageX = new Vector2(-8f, 8f);
                    bossLimitStageY = new Vector2(5f, 0f);


                    livesBar.GetComponent<Image>().fillAmount = listEnemies[i].enemy.enemyLives / 100f;




                    if (bossTimer >= 3f)
                    {
                        currentPointXY = new(Random.Range(bossLimitStageX.x, bossLimitStageX.y), Random.Range(bossLimitStageY.x, bossLimitStageY.y), 0);
                        bossTimer = 0;
                    }
                    
                    listEnemies[i].enemyObj.transform.position = Vector3.MoveTowards(listEnemies[i].enemyObj.transform.position, currentPointXY, 0.2f);
                }
                else
                {
                    switch (direction)
                    {
                        case EnemyDir.RIGHT:
                            listEnemies[i].enemyObj.transform.position = new Vector2(listEnemies[i].enemyObj.transform.position.x + 0.1f, listEnemies[i].enemyObj.transform.position.y);
                            break;
                        case EnemyDir.LEFT:
                            listEnemies[i].enemyObj.transform.position = new Vector2(listEnemies[i].enemyObj.transform.position.x - 0.1f, listEnemies[i].enemyObj.transform.position.y);
                            break;
                    }
                }
            }

            //Comprobar si algún enemigo ha llegado al limite de la pantalla
            CheckLimits();
            enemySpeed = 0;
        }
    }

    private void EnemyAttack()
    {
        enemyFireRate += Time.deltaTime;
        if (enemyFireRate >= 1)
        {
            int randomEnemy = Random.Range(0, listEnemies.Count);

            switch (listEnemies[randomEnemy].enemy.enemyId)
            {
                case 0:
                    CreateBullet(BulletType.ENEMY, listEnemies[randomEnemy].enemyObj.transform.position, 10, listEnemies[randomEnemy].enemy.enemyId);
                    break;
                case 1:
                    CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x - 0.1f, listEnemies[randomEnemy].enemyObj.transform.position.y), 10, listEnemies[randomEnemy].enemy.enemyId);
                    CreateBullet(BulletType.ENEMY, listEnemies[randomEnemy].enemyObj.transform.position, 10, listEnemies[randomEnemy].enemy.enemyId);
                    CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x + 0.1f, listEnemies[randomEnemy].enemyObj.transform.position.y), 10, listEnemies[randomEnemy].enemy.enemyId);
                    break;
                case 2:
                    CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x - 0.2f, listEnemies[randomEnemy].enemyObj.transform.position.y), 15, listEnemies[randomEnemy].enemy.enemyId);
                    CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x + 0.2f, listEnemies[randomEnemy].enemyObj.transform.position.y), 15, listEnemies[randomEnemy].enemy.enemyId);
                    break;
                case 3:
                    CreateBullet(BulletType.ENEMY, listEnemies[randomEnemy].enemyObj.transform.position, 8, listEnemies[randomEnemy].enemy.enemyId);
                    CreateBullet(BulletType.ENEMY, listEnemies[randomEnemy].enemyObj.transform.position, 8, listEnemies[randomEnemy].enemy.enemyId);
                    CreateBullet(BulletType.ENEMY, listEnemies[randomEnemy].enemyObj.transform.position, 8, listEnemies[randomEnemy].enemy.enemyId);
                    break;
                case 4:
                    if (bossFireRate >= 5)
                    {
                        CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x - 0.3f, listEnemies[randomEnemy].enemyObj.transform.position.y - 1.5f), 30, listEnemies[randomEnemy].enemy.enemyId);
                        CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x - 0.3f, listEnemies[randomEnemy].enemyObj.transform.position.y - 1.5f), 30, listEnemies[randomEnemy].enemy.enemyId);
                        CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x + 0.3f, listEnemies[randomEnemy].enemyObj.transform.position.y - 1.5f), 30, listEnemies[randomEnemy].enemy.enemyId);
                        CreateBullet(BulletType.ENEMY, new Vector2(listEnemies[randomEnemy].enemyObj.transform.position.x + 0.3f, listEnemies[randomEnemy].enemyObj.transform.position.y - 1.5f), 30, listEnemies[randomEnemy].enemy.enemyId);
                        bossFireRate = 0;
                    }
                    break;
            }
            enemyFireRate = 0;
        }
    }

    private void CheckLimits()
    {
        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].enemyObj.transform.position.x <= limitsStage.x)//Moviendo hacia la izquierda
            {
                direction = EnemyDir.RIGHT;
                SetDownEnemy();
                break;
            }
            if (listEnemies[i].enemyObj.transform.position.x >= limitsStage.y)//Moviendo hacia la derecha
            {
                direction = EnemyDir.LEFT;
                SetDownEnemy();
                break;
            }
        }
    }

    //Movimiento vertical & Velocidad aumentada Enemigos
    private void SetDownEnemy()
    {
        //Aumento de velocidad
        totalEnemySpeed -= 0.02f;
        if (totalEnemySpeed <= 0.02f)
        {
            totalEnemySpeed = 0.02f;
        }

        //Movimiento Vertical
        for (int i = 0; i < listEnemies.Count; i++)
        {
            listEnemies[i].enemyObj.transform.position = new Vector2(listEnemies[i].enemyObj.transform.position.x, listEnemies[i].enemyObj.transform.position.y - 0.5f);

            if (listEnemies[i].enemyObj.transform.position.y <= -6)
            {
                ClearStage();
                break;
            }
        }
    }

    //Metodo creacion de Balas
    private void CreateBullet(BulletType _type, Vector2 _pos, float _speed, int _id)
    {
        GameObject newBullet = GameObject.CreatePrimitive(PrimitiveType.Quad);
        newBullet.name = "Bullet";
        newBullet.transform.position = _pos;
        Destroy(newBullet.GetComponent<MeshCollider>());
        StartCoroutine(AddCollider(newBullet, 98));
        newBullet.GetComponent<MeshRenderer>().enabled = false;
        newBullet.tag = "Bullet";

        GameObject spriteBullet = new("Sprite Bullet");
        spriteBullet.transform.SetParent(newBullet.transform);
        spriteBullet.transform.localPosition = new Vector2(0, 0);

        //Tipo de Bala segun el Origen
        if (_type == BulletType.PLAYER)
        {
            spriteBullet.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            spriteBullet.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ammo/Rocket");
            spriteBullet.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_type == BulletType.ENEMY)
        {
            if (_id == 0 || _id == 1 || _id == 2)
            {
                spriteBullet.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                spriteBullet.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ammo/Bullet");
            }
            else if (_id == 3 || _id == 4)
            {
                spriteBullet.transform.localScale = new Vector3(2f, 2f, 2f);
                spriteBullet.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Ammo/Rocket");
            }
            spriteBullet.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        newBullet.AddComponent<BulletControl>().InitBullet(_type, _speed, this);
        Destroy(newBullet, 2);
    }

    //Daño a Enemigos
    public void GetDamageEnemy(GameObject _obj)
    {
        for (int i = 0; i < listEnemies.Count; i++)
        {
            if (listEnemies[i].enemyObj == _obj)
            {
                listEnemies[i].enemy.enemyLives--;
                if (listEnemies[i].enemy.enemyLives <= 0)
                {
                    CreateExplosion(listEnemies[i].enemyObj.transform.position);
                    Destroy(listEnemies[i].enemyObj);
                    listEnemies.RemoveAt(i);
                }
            }
        }

        if (listEnemies.Count == 0)
        {
            // Si matas a todos los enemigos
            CheckStars();
            ClearStage();
            winPanel.SetActive(true);
        }
    }

    //Daño al Jugador
    public void GetDamagePlayer()
    {
        totalLives--;
        if (totalLives <= 0)
        {
            CreateExplosion(player.transform.position);
            Destroy(player);
            ClearStage();
            losePanel.SetActive(true);
        }
    }

    //Explosiones con Eliminacion
    private void CreateExplosion(Vector2 _pos)
    {
        GameObject newExplosion = new("Explosion");
        newExplosion.transform.position = _pos;
        newExplosion.AddComponent<SpriteRenderer>();
        newExplosion.GetComponent<SpriteRenderer>().sortingOrder = 2;
        newExplosion.AddComponent<AnimControl>().InitAnim(newExplosion.GetComponent<SpriteRenderer>(), new List<Sprite>(Resources.LoadAll<Sprite>("Explosion")), 0.1f, AnimType.ONCE);
        Destroy(newExplosion, 2);
    }

    //Calculador de Estrellas
    private void CheckStars()
    {
        float result = (totalSeconds / currentWorld.seconds) * 3;
        int totalStars = (int)result + 1;

        if (PlayerPrefs.HasKey("World_" + currentWorld.id) == false)
        {
            PlayerPrefs.SetInt("World_" + currentWorld.id, totalStars);
        }
        else
        {
            int tempStars = PlayerPrefs.GetInt("World_" + currentWorld.id);
            if (totalStars > tempStars)
            {
                PlayerPrefs.SetInt("World_" + currentWorld.id, totalStars);
            }
        }
    }

    //Metodos de Pausa/Continuar del Juego
    public void PauseResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
