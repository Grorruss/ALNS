﻿
using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Attack,
    Die
};

public enum EnemyType
{
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{
    //Animator anim;
    GameObject player;
    public EnemyState currState = EnemyState.Wander;
    public EnemyType enemyType;

    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float cooldown;

    private bool chooseDir = false;
    private bool dead = false;
    private bool cooldownAttack = false;

    //private float oldPosition = 0.0f

    private Vector3 randomDir;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState){
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                break;
        }

        if(isPlayerInRange(range)&& currState!=EnemyState.Die){
            currState = EnemyState.Follow;
        }else if(!isPlayerInRange(range) && currState != EnemyState.Die){
            currState = EnemyState.Wander;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange) {
            currState = EnemyState.Attack;
        }
            /*if (transform.position.x > oldPosition)
            {
                anim.SetFloat("x")
            }*/
        

        
    }

    private bool isPlayerInRange(float Range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(){
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander(){
        if(!chooseDir){
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(isPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }

    void Follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if(!cooldownAttack){
            switch(enemyType){
                case (EnemyType.Melee):
                    GameController.DamagePlayer();
                    StartCoroutine(Cooldown());
                break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(Cooldown());
                break;
            }
        }
    }

    private IEnumerator Cooldown()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(cooldown);
        cooldownAttack = false;
    }

    public void Death(){
       Destroy(gameObject);
    }
}
