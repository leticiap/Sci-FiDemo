using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private GameObject _hitMarkerPrefab;
    private float _gravity = 9.8f;
    [SerializeField] private AudioSource _weaponAudio;
    private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _isReloading;
    private UIManager _uiManager;
    [SerializeField] private GameObject _weapon;
    public bool hasCoin;
    // Use this for initialization
	void Start ()
    {
        hasCoin = false;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _isReloading = false;
        _uiManager.UpdateAmmo(0);
        _uiManager.UpdateCoinInvetory(false);
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(Reload());
            _uiManager.UpdateAmmo(_currentAmmo);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();
	}

    void Shoot()
    {
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        _muzzleFlash.SetActive(true);
        if (!_weaponAudio.isPlaying)
            _weaponAudio.Play();
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit:" + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 1f);
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }
    void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        Vector3 velocity = direction * speed;
        velocity.y -= _gravity;
        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _isReloading = false;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
    }
}
