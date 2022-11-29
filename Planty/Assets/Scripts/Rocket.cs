using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{

    [SerializeField]
    private float angle = 100f, rocketM = 1f, G = 1f;
    
    private bool Alive = true;
    private bool onAir = false;

    private GameObject Planets;
    private Transform explosion;
    private ParticleSystem explosionPartic;
    private Rigidbody2D rocket_body;
    private Slider power_slider;
    private SceneManagerCS scene_manager;

    void Start()
    {
        Planets = GameObject.Find("Planets");
        explosion = GameObject.Find("Explosion").transform;
        power_slider = GameObject.Find("UI/Power_Slider").GetComponent<Slider>();
        scene_manager = GameObject.Find("SceneManager").GetComponent<SceneManagerCS>();

        explosionPartic = explosion.GetComponent<ParticleSystem>();
        rocket_body = gameObject.GetComponent<Rigidbody2D>();

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

            if (Alive && onAir) 
                rocket_body.AddForce(v.normalized * gForce);
        }

        if (Alive && onAir) 
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rocket_body.velocity);

        changeAngle();
    }

    public void changeAngle(){
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!onAir && !isMouseOverUI() && mousePos.x < transform.position.x)
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

            float radians = angle * Mathf.Deg2Rad;

            float rx = -1 * Mathf.Sin(radians);
            float ry = Mathf.Cos(radians);

            Vector3 v = new Vector3(rx, ry, 0f) * velo;

            rocket_body.velocity = v;

            onAir = true;
        }
        
    }

    private bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Planet")
        {
            Alive = false;

            explosionPartic.Play();
            Destroy(this.gameObject, 0.5f);
        }

        if(other.transform.tag == "spacestation")
        {
            scene_manager.nextLevel();
        }
    }
}
