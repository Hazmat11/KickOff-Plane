using UnityEngine;

public class DCABehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject cannon;

    private bool _playerDetected = false;
    private GameObject _target;
    private float _time = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerDetected)
        {
            cannon.transform.LookAt(_target.transform.position);
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position + 2 * transform.up, Quaternion.Euler(0,90,0));
                bullet.GetComponent<DCABullet>().Target = _target;
                _time = 2;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _target = other.gameObject;
            transform.LookAt(_target.transform.position);
            _playerDetected = true;
            _time = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerDetected = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
