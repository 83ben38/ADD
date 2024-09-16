using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class FreeplayWaveGenerator : MonoBehaviour
{
    public static FreeplayWaveGenerator manager;
    public GameObject apple;
    public GameObject avocado;
    public GameObject banana;
    public GameObject blueberry;
    public GameObject coconut;
    public GameObject grape;
    public GameObject lemon;
    public GameObject mango;
    public GameObject miniAvocado;
    public GameObject miniGrape;
    public GameObject miniOrange;
    public GameObject orange;
    public GameObject pear;
    public GameObject raspberry;
    public GameObject strawberry;
    private GameObject[] allFruits;
    public WaveScriptableObject generateWave(float credits)
    {
        int numThings = Random.Range(1, 4);
        credits /= numThings;
        WaveScriptableObject wso = ScriptableObject.CreateInstance<WaveScriptableObject>();
        wso.waves = new GameObject[numThings];
        wso.configs = new EnemyConfiguration[numThings];
        wso.waveDelays = new float[numThings];
        wso.waveNums = new int[numThings];
        wso.waveSpacings = new float[numThings];
        for (int i = 0; i < numThings; i++)
        {
            int style = Random.Range(1, 5);
            int numEnemies;
            switch (style)
            {
                case 1:
                {
                    numEnemies = 1;
                }
                    break;
                case 2: 
                case 3:
                {
                    numEnemies = Random.Range(3,8);
                }
                    break;
                default:
                {
                    numEnemies = Random.Range((int)(credits*0.001),(int)(credits*0.002));
                }
                    break;
            }

            if (style == 4)
            {
                style = 3;
            }

            wso.waveDelays[i] = 0;
            wso.waveNums[i] = numEnemies;
            wso.waveSpacings[i] = 10f / numEnemies;
            int enemyNum = Random.Range(0, 15);
            wso.waves[i] = allFruits[enemyNum];
            wso.configs[i] = generateSection(style, credits/numEnemies, enemyNum);
        }
        return wso;
    }

    public EnemyConfiguration generateSection(int style, float credits, int enemyNum)
    {
        EnemyConfiguration config = new EnemyConfiguration();
        float abilityCredits;
        if (enemyNum is 0 or 9 or 10)
        {
            abilityCredits = 0;
        }
        else if (enemyNum is 13 or 12)
        {
            abilityCredits = credits * 2 / 3;
        }
        else if (style == 3)
        {
            abilityCredits = Random.Range(0.5f, 0.8f) * credits;
        }
        else
        {
            abilityCredits = Random.Range(0.2f, 0.5f) * credits;
        }

        float statsCredits = credits - abilityCredits;
        int hp;
        int speed;
        if (style == 1)
        {
            speed = Random.Range(8, 15);
            hp = (int)(statsCredits / speed);
        }
        else if (style == 2)
        {
            speed = Random.Range(20, 30 + (int)(credits/100f));
            hp = (int)(statsCredits / speed);
        }
        else
        {
            speed = Random.Range(8, 25);
            hp = (int)(statsCredits / speed);
        }

        hp++;
        config.hp = hp;
        config.speed = speed;
        config.minSize = 200;
        config.maxSize = Math.Max(300, ((int)Math.Log(hp, 2)-2) * 100);
        if (enemyNum == 1)
        {
            config.orangeOptions = new OrangeOptions();
            config.orangeOptions.splitAmount = 1;
            int enemyNum2 = Random.Range(0, 15);
            config.orangeOptions.childObject = allFruits[enemyNum2]; ;
            config.orangeOptions.childConfig = generateSection(1, abilityCredits, enemyNum2);
        }

        if (enemyNum == 2)
        {
            config.bananaOptions = new BananaOptions();
            config.bananaOptions.speedIncrease = Math.Max(1,(int)Math.Log(abilityCredits, 1.5));
        }

        if (enemyNum == 3)
        {
            config.blueberryOptions = new BlueberryOptions();
            config.blueberryOptions.speedIncrease = Math.Max(1,(int)abilityCredits / hp);
        }

        if (enemyNum is 4 or 8)
        {
            config.coconutOptions = new CoconutOptions();
            config.coconutOptions.reductionMod = Math.Max(1,(int)(abilityCredits * 4 / (hp*speed)));
        }

        if (enemyNum is 5)
        {
            config.grapeOptions = new GrapeOptions();
            config.grapeOptions.spawnCooldown = Random.Range(0.5f, 2f);
            int enemyNum2 = Random.Range(0, 15);
            config.grapeOptions.childObject = allFruits[enemyNum2]; ;
            config.grapeOptions.childConfig = generateSection(3, abilityCredits/(2f/config.grapeOptions.spawnCooldown), enemyNum2);
        }
        if (enemyNum == 6)
        {
            config.lemonOptions = new LemonOptions();
            config.lemonOptions.radius = Random.Range(1, 3);
            config.lemonOptions.speedIncrease = Math.Max(1,(int)((Math.Log(abilityCredits, 2)-5)/config.lemonOptions.radius));
        }

        if (enemyNum == 7)
        {
            config.mangoOptions = new MangoOptions();
            config.mangoOptions.radius = Random.Range(1, 3);
            config.mangoOptions.healAmount = (int)(abilityCredits / (config.mangoOptions.radius * 10));
        }

        if (enemyNum == 11)
        {
            config.orangeOptions = new OrangeOptions();
            config.orangeOptions.splitAmount = Random.Range(2,6);
            int enemyNum2 = Random.Range(0, 15);
            config.orangeOptions.childObject = allFruits[enemyNum2]; ;
            config.orangeOptions.childConfig = generateSection(2, abilityCredits, enemyNum2);
        }

        if (enemyNum == 14)
        {
            config.strawberryOptions = new StrawberryOptions();
            config.strawberryOptions.stunRadius = Random.Range(1, 3);
            config.strawberryOptions.stunAmount = (float)Math.Log(abilityCredits, 2) / (config.strawberryOptions.stunRadius*5);
        }
        return config;
    }

    private void Start()
    {
        manager = this;
        allFruits = new GameObject[15];
        allFruits[0] = apple;
        allFruits[1] = avocado;
        allFruits[2] = banana;
        allFruits[3] = blueberry;
        allFruits[4] = coconut;
        allFruits[5] = grape;
        allFruits[6] = lemon;
        allFruits[7] = mango;
        allFruits[8] = miniAvocado;
        allFruits[9] = miniGrape;
        allFruits[10] = miniOrange;
        allFruits[11] = orange;
        allFruits[12] = pear;
        allFruits[13] = raspberry;
        allFruits[14] = strawberry;
    }
}
