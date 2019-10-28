using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[RequireComponent(typeof(movementController))]
[RequireComponent(typeof(Health))]
public class PlayerController : MovingObject {
	movementController controller;
    [SerializeField]
    Transform respawn;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Camera cam;

    public Vector3 lastDir = new Vector3(1,0,0);

    public bool hasAxe = true;
    [SerializeField]
    private GameObject Axe;
    [SerializeField]
    public GameObject lastAxe;
    private AxeController axeController;
    [SerializeField]
    private GameObject lightningEffect;
    [SerializeField]
    private GameObject teleport1;
    [SerializeField]
    private GameObject teleport2;
    private bool canTakeDammage = true;
    
    [SerializeField]
    private Health health;
	
    [SerializeField]
	Vector3 input;
	
	[SerializeField]
	bool hasDash;
	

	[SerializeField]
	float dashDist = 2;
	float dashWait = .5f;
	float dashUntilNext;
	bool canDash;
	bool CanDash(){
		if (hasDash) {
			
				DashCount = true;
			
			if (dashUntilNext <= 0) {
				return true;
			}
			return false;
		}
		return false;
	}
	bool DashCount = true;
    Transform sprite;

	float scaleX;
	// Use this for initialization
	void Start () {
        controller = GetComponent<movementController>();
        health = GetComponent<Health>();
        sprite = transform.GetChild(0);
        scaleX = sprite.localScale.x;

	}
    
	// Update is called once per frame
	void Update () {

		CalculateVelocity (input);
		if (input.x != 0) {
            sprite.localScale = new Vector3(Mathf.Sign (input.x) == 1?-scaleX:scaleX, sprite.localScale.y, sprite.localScale.z);
		}
        if(input.x != 0 || input.z != 0)
        {
            lastDir = input;
        }
        
        anim.SetFloat("x", Mathf.Abs(input.x));
        anim.SetFloat("y", Mathf.Abs(input.z));

        controller.move (velocity * Time.deltaTime);

		if (dashUntilNext > 0 && DashCount) {
			dashUntilNext -= Time.deltaTime;
		}
		canDash = CanDash ();
	}



	public void SetDiractionalInput(Vector3 Input){
		input = Input;
	}


	public void Dash(){
		if(canDash){
		controller.dash (dashDist, lastDir);
		dashUntilNext = dashWait;
			if (!controller.collisions.below) {
				DashCount = false;
			}
		}
	}
	void spawnAxe()
    {
        anim.SetTrigger("attack");
        var v3 = Input.mousePosition;
        v3.z = 90.2967763f;
        v3 = cam.ScreenToWorldPoint(v3);
        Vector3 dir = v3 - transform.position;
        dir.y = transform.position.y;
        GameObject lastAxe = Instantiate(Axe, transform.position, Quaternion.identity);
        axeController = lastAxe.GetComponent<AxeController>();
        axeController.input = dir.normalized;
        axeController.player = this;
        hasAxe = false;
    }	
    void Teleport()
    {
        if (lastAxe)
        {
            canTakeDammage = false;
            Instantiate(teleport1, transform.position, Quaternion.identity);
            GameObject lightning = Instantiate(lightningEffect, Vector3.Lerp(transform.position, lastAxe.transform.position, 0.3f), Quaternion.identity);
            lightning.transform.LookAt(lastAxe.transform.position);
            Quaternion q = lightning.transform.rotation;
            q.eulerAngles = new Vector3(90, q.eulerAngles.y, q.eulerAngles.z);
            lightning.transform.rotation = q;
            
            
            Vector3 pos = lastAxe.transform.position - transform.position;
            transform.Translate(pos);
            Instantiate(teleport2, lastAxe.transform.position, Quaternion.identity);
            canTakeDammage = true;
        }
    }
    public void TrythrowAxe()
    {
        if (hasAxe)
        {
            spawnAxe();
        } else
        {
            Teleport();
        }
    }
   

    public void addHealth(int add)
    {
        health.health += health.health < health.getMaxHealth() ? add : 0;
    }

    public void removeHealth(int remove)
    {
        if (canTakeDammage)
        {
            if (remove == 0)
                remove = 1;
            health.health -= remove;
            StartCoroutine(hitCooldown());
            if (health.health <= 0)
            {
                death();
            }
        }
    }

    public void death()
    {
        health.health = health.getMaxHealth();
        transform.position = respawn.position;
        GameObject.FindGameObjectWithTag("EnemyMaster").GetComponent<EnemyMaster>().Respawn();
        hasAxe = true;
        GameObject[] axe = GameObject.FindGameObjectsWithTag("axe");
        if(axe.Length > 0)
        {
            foreach(GameObject a in axe)
            {
                Destroy(a);
            }
        }
        
    }

    public void shake()
    {
        cam.gameObject.SendMessage("shake");
    }
    
    IEnumerator hitCooldown()
    {
        canTakeDammage = false;
        yield return new WaitForSeconds(0.3f);
        canTakeDammage = true;
    }
}
