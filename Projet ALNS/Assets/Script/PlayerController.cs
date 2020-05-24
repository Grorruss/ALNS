using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public float speed;
    Rigidbody2D playerRigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float speedBullet;
    public float fireDelay;
    public int damage;
    private float lastBullet;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
   
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isWalking = (Mathf.Abs(horizontal) + Mathf.Abs(vertical)) > 0;

        anim.SetBool(("isWalking"), isWalking);

        if (isWalking)
        {
            anim.SetFloat("x", horizontal);
            anim.SetFloat("y", vertical);

            
            //transform.position += new Vector3(horizontal, vertical, 0).normalized * Time.deltaTime;
        }
        playerRigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

        collectedText.text = "Item Collected : " + collectedAmount;

        float shootHorizontal = Input.GetAxisRaw("ShootHorizontal");
        float shootVertical = Input.GetAxisRaw("ShootVertical");

        if((shootHorizontal != 0 || shootVertical != 0) && (Time.time>lastBullet+ fireDelay)){
            Shoot(shootHorizontal, shootVertical);
            lastBullet = Time.time;
        }
    }

    void Shoot(float x, float y){
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<BulletController>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * speedBullet : Mathf.Ceil(x) * speedBullet,
            (y < 0) ? Mathf.Floor(y) * speedBullet : Mathf.Ceil(y) * speedBullet,
            0
        );
    }
}
