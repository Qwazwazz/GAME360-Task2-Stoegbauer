using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private Camera cam;

    [Header("Audio")]

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        HandleShooting();
    }

    private void HandleLook()
    {
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        Debug.DrawLine(transform.position, mousePos, Color.white, Time.deltaTime);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.linearVelocity = movement * moveSpeed;
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }

    }

    private void FireBullet()
    {
        if (GameManagerEx.Instance.score > 499 && GameManagerEx.Instance.score < 1000)
            fireRate = 0.3f;
        if (GameManagerEx.Instance.score > 1000)
            fireRate = 0.1f;

        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        AudioManagerEx.PlaySound(SoundType.SHOOT);
        // Play shoot sound effect
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Player hit by enemy - lose a life
            GameManagerEx.Instance.LoseLife();
        }

        if (other.CompareTag("Collectible"))
        {
            // Player collected an item
            Collectible collectible = other.GetComponent<Collectible>();
            if (collectible)
            {
                GameManagerEx.Instance.CollectiblePickedUp(25);
                AudioManagerEx.PlaySound(SoundType.COIN);
                Destroy(other.gameObject);


            }
        }
    }
}
