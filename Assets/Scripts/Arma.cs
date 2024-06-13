using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject arma;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 50f;

    //

    public int maxBullet = 10; // ou maxAmmo
    private int currentBullet;

    private float nextTimeToFire = 0;
    public float reloadindTime = 1f;

    private bool isReloading = false;
    bool taArmado = false;

    // Start is called before the first frame update
    void Start()
    {
        currentBullet = maxBullet;
        arma.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;
        if(Input.GetKeyUp(KeyCode.E)) {
            if (!taArmado) { taArmado = true; }
            else {taArmado = false; }
        
        }

        if (taArmado)
        {
            arma.SetActive(true);
        }
        else
        {
            arma.SetActive(false);
        }
        if (currentBullet <= 0)
        {
            // Reload();
            if (Input.GetMouseButton(1))
            {

                StartCoroutine(Reload());
            }
            return;
        }
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && taArmado)
        {           
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }
    void Shoot()
    {
        currentBullet--;
        Instantiate(bala, firePoint.position, firePoint.rotation);

    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadindTime);
        currentBullet = maxBullet;
        isReloading = false;
    }
}
