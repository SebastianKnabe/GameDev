using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossBehavior : MonoBehaviour
{
    [SerializeField] private float teleportCooldown;
    [SerializeField] private List<GameObject> teleportPoints;

    [SerializeField] private GameObject meteorSpawnArea;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float meteorRainCooldown;
    [SerializeField] private float meteorRainCanneling;
    [SerializeField] private float meteorsPerSecond;
    [SerializeField] private float meteorSpawnDistance;

    [SerializeField] private GameObject addSpawnArea;
    [SerializeField] private GameObject spawnAddPrefab;
    [SerializeField] private int numberOfAdds = 2;

    [SerializeField] private VoidEvent bossDefeatedEvent;

    private SpriteRenderer spriteRenderer;
    private Renderer renderer;
    private EnemyEntity enemyEntity;
    private Animator animator;
    private WizardBossFX bossFX;
    private float teleportTimer = 0f;
    private int lastTeleportLocation = 0;
    private float meteorRainCooldownTimer = 0f;
    private float meteorRainChannelTimer = 0f;
    private float meteorSpawnTimer = 0f;
    private bool isChanneling = false;
    private int phase = 1;
    private int addCount = 0;
    private Bounds meteorSpawnAreaBounds;
    private Bounds addSpawnAreaBounds;
    private Vector3 lastMeteorPos;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyEntity = gameObject.GetComponent<EnemyEntity>();
        meteorSpawnAreaBounds = meteorSpawnArea.GetComponent<BoxCollider2D>().bounds;
        addSpawnAreaBounds = addSpawnArea.GetComponent<BoxCollider2D>().bounds;
        animator = gameObject.GetComponent<Animator>();
        bossFX = gameObject.GetComponent<WizardBossFX>();
        renderer = gameObject.GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        /*
         * Phase 1:
         *  - Schießen
         * Phase 2:
         *  - Teleport
         * Phase 3:
         *  - Adds die Schilden
         * Phase 4: 
         *  - Mehr Adds
         *  - Metoerregen
         */
        if (phase >= 2)
        {
            Teleport();
        }

        if (phase >= 3)
        {
            MeteorRain();
        }

    }

    private void Teleport()
    {
        if (teleportCooldown < teleportTimer && !isChanneling && renderer.isVisible)
        {
            Debug.Log("Casting Teleport");
            bossFX.playTeleportSound();
            teleportTimer = 0;
            int teleportIndex = lastTeleportLocation;
            while (teleportIndex == lastTeleportLocation)
            {
                teleportIndex = Random.Range(0, teleportPoints.Count);
            }
            gameObject.transform.localPosition = teleportPoints[teleportIndex].transform.localPosition;
            lastTeleportLocation = teleportIndex;

            FlipSprite();
        }
        else if (!isChanneling)
        {
            teleportTimer += Time.fixedDeltaTime;
        }

    }

    private void MeteorRain()
    {
        //Channel Meteor
        if (meteorRainChannelTimer < meteorRainCanneling && isChanneling)
        {
            meteorRainChannelTimer += Time.fixedDeltaTime;
            CastMeteor();
        }
        //Abbruch Channel Meteor
        else if (meteorRainChannelTimer >= meteorRainCanneling && isChanneling)
        {
            Debug.Log("Stop Casting Meteor");
            meteorRainCooldownTimer = 0f;
            isChanneling = false;
            bossFX.turnBackgroundNormal();
        }
        //Check Meteor Cooldown Ready
        else if (meteorRainCooldown < meteorRainCooldownTimer)
        {
            Debug.Log("Casting Meteor");
            isChanneling = true;
            meteorRainChannelTimer = 0f;
        }
        // Meteor Cooldown abklingen
        else
        {
            meteorRainCooldownTimer += Time.fixedDeltaTime;
        }
    }

    private void CastMeteor()
    {
        animator.SetTrigger("castAttack1");
        bossFX.turnBackgroundRed();
        if (meteorSpawnTimer > (1.0f / meteorsPerSecond))
        {
            bool tooCloseToOldSpawn = true;
            Vector3 offset;
            Vector3 pos = lastMeteorPos;
            while (tooCloseToOldSpawn)
            {
                offset = new Vector3(Random.Range(-meteorSpawnAreaBounds.size.x / 2, meteorSpawnAreaBounds.size.x / 2), Random.Range(-meteorSpawnAreaBounds.size.y / 2, meteorSpawnAreaBounds.size.y / 2));
                pos = meteorSpawnAreaBounds.center + offset;

                if ((pos - lastMeteorPos).magnitude > meteorSpawnDistance)
                {
                    tooCloseToOldSpawn = false;
                }

            }

            Quaternion rotation = Quaternion.identity;
            bossFX.playMeteorSound();
            GameObject g = Instantiate(meteorPrefab, pos, rotation);
            g.transform.Rotate(0f, 0f, -90f);
            meteorSpawnTimer = 0f;
            lastMeteorPos = pos;
        }
        else
        {
            meteorSpawnTimer += Time.fixedDeltaTime;
        }
    }

    private void SpawnAdds()
    {
        SetShield(true);
        addCount = numberOfAdds;
        for (int i = 0; i < numberOfAdds; i++)
        {
            bool tooCloseToOldSpawn = true;
            Vector3 offset;
            Vector3 pos = lastMeteorPos;
            while (tooCloseToOldSpawn)
            {
                offset = new Vector3(Random.Range(-addSpawnAreaBounds.size.x / 2, addSpawnAreaBounds.size.x / 2), Random.Range(-addSpawnAreaBounds.size.y / 2, addSpawnAreaBounds.size.y / 2));
                pos = addSpawnAreaBounds.center + offset;

                if ((pos - lastMeteorPos).magnitude > meteorSpawnDistance)
                {
                    tooCloseToOldSpawn = false;
                }

            }
            Instantiate(spawnAddPrefab, pos, Quaternion.identity);
        }
    }

    private void FlipSprite()
    {
        if (transform.localPosition.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void CheckPhases()
    {
        float healthPercentage = enemyEntity.getHealthPercentage();

        if (healthPercentage <= 0.0f)
        {
            animator.SetTrigger("isDead");
            bossFX.turnBackgroundNormal();
            phase = 0;
            return;
        }

        if (healthPercentage <= 0.75f && phase < 2)
        {
            phase = 2;
            SpawnAdds();
            InstantTeleport();
            numberOfAdds *= 2;
            return;
        }

        if (healthPercentage <= 0.5f && phase < 3)
        {
            phase = 3;
            SpawnAdds();
            InstantTeleport();
            return;
        }
    }

    private void InstantTeleport()
    {
        teleportTimer = teleportCooldown;
        Teleport();
    }

    private void SetShield(bool shield)
    {
        enemyEntity.setShield(shield);
        gameObject.transform.GetChild(0).gameObject.SetActive(shield);

    }

    public void onAddDeadEvent()
    {
        addCount = addCount - 1;
        if (addCount < 0)
        {
            addCount = 0;
        }

        if (addCount == 0)
        {
            SetShield(false);
        }
    }

    void OnDestroy()
    {
        bossDefeatedEvent.Raise();
    }
}
