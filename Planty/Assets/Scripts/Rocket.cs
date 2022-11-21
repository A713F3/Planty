using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{

    public float velocity = 2f;
    public float angle = 100f;
    public float rocketM = 1f;
    public float G = 1f;
    
    private bool Alive = true;
    private bool onAir = false;

    public GameObject Planets;
    public Transform explosion;
    private ParticleSystem explosionPartic;
    private Rigidbody2D rocket_body;

    public Slider power_slider;

    void Start()
    {
        explosionPartic = explosion.GetComponent<ParticleSystem>();
        rocket_body = gameObject.GetComponent<Rigidbody2D>();
        //power_slider_cs = power_slider.GetComponent<PowerSlider>(); 

        transform.rotation = Quaternion.Euler(0, 0, angle);

        rocket_body.mass = rocketM;
    }

    void Update()
    {
        foreach (Transform planet in Planets.transform)
        {
            Planet cs = planet.gameObject.GetComponent<Planet>();

            float dist = Vector3.Distance(planet.position, transform.position);
            float gForce = G * (cs.planetM * rocketM) / (dist * dist);

            Vector3 v = planet.transform.position - transform.position;

            if (Alive && onAir) rocket_body.AddForce(v.normalized * gForce);
        }

        if (Alive && onAir) transform.rotation = Quaternion.LookRotation(Vector3.forward, rocket_body.velocity);

        changeAngle();
    }
    
    private bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void changeAngle(){
        if (!onAir)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0) && !isMouseOverUI() && mousePos.x < transform.position.x)
            {
                Vector3 dir = mousePos - transform.position;
        
                float a = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, a));

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 200f * Time.deltaTime);
                angle = a;
            }
            
        }
    }

    public void Launch(){
        if (!onAir)
        {
            float velo = power_slider.value;
            //power_slider_cs.add = 0;

            float radians = angle * Mathf.Deg2Rad;

            float rx = -1 * Mathf.Sin(radians);
            float ry = Mathf.Cos(radians);

            Vector3 v = new Vector3(rx, ry, 0f) * velo;

            rocket_body.velocity = v;

            onAir = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Planet")
        {
            Alive = false;

            explosionPartic.Play();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
