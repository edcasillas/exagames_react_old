using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemController : MonoBehaviour
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float walkSpeed;
	[SerializeField]
	private float maxCloseDistance;
	[SerializeField]
	private float maxChasingRange;
	[SerializeField]
	private float attackCooldownTime;
	private bool canAttack = true;

	private GameObject player;

	private bool isDead = false;
	private bool animDeadPlayed= false;

	private readonly string PLAYER_TAG = "Player";

	#region Boss Stats
	[SerializeField]
	private float maxLife;
	[SerializeField]
	private float life;
	[SerializeField]
	private float damage;
	[SerializeField]
	private float cooldownGetDamage;//Me quede aqui jeje, falta implementarlo
	#endregion

	#region Shot / Special Ability
	[SerializeField]
	private GameObject startProjectilePosition;
	[SerializeField]
	private GameObject projectilePrefab;
	#endregion

	#region UI
	[SerializeField]
	private Image life_FillImage;
	#endregion

	#region Animations state names
	private readonly string IDLE_STATE_NAME = "Idle1";
	private readonly string WALK_STATE_NAME = "Walk";
	private readonly string DEATH_STATE_NAME = "Death1";
	private readonly string GET_HIT_STATE_NAME = "GetHit2";
	private readonly string NORMAL_ATTACK_STATE_NAME = "Attack3";
	private readonly string FIRE_ATTACK_STATE_NAME = "Attack2";
	#endregion

	#region Animation Parameters
	private readonly string DEATH_TRIGGER = "Death1";
	private readonly string SPECIAL_ATTACK_TRIGGER = "Attack2";
	private readonly string ATTACK_TRIGGER = "Attack3";
	private readonly string GET_HIT_TRIGGER = "GetHit2";
	private readonly string WALKING_BOOL = "Walking";
	#endregion
	
	void Start()
    {
		life_FillImage.fillAmount = life;
		UpdateLifeBar();
		player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }
	
    void Update()
    {
		if (life <= 0)
			isDead = true;

		if(Input.GetKeyDown(KeyCode.R)) 
		{
			GetDamage(10);
		}

		if(Input.GetKeyDown(KeyCode.F)) {
			SpawnProjectil();
		}

		if(!isDead) 
		{
			//When the player is in range the boss go to follow him and try to attack him
			if (Vector3.Distance(player.transform.position, transform.position) < maxChasingRange) 
			{
				transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
				Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
				if (Vector3.Distance(playerPos, transform.position) > maxCloseDistance && (animator.GetCurrentAnimatorStateInfo(0).IsName(IDLE_STATE_NAME) || animator.GetCurrentAnimatorStateInfo(0).IsName(WALK_STATE_NAME))) //Check if the distances between the object are bigger than the max close distance
				{
					transform.position = Vector3.MoveTowards(transform.position, playerPos, walkSpeed * Time.deltaTime);
					Walk(true);
				} else //If the boss are the enough closer, can attack the enemy
				{
					Walk(false);
					if (canAttack && Vector3.Distance(playerPos, transform.position) < maxCloseDistance) 
					{
						canAttack = false;
						Debug.Log("Attack! ");
						Attack();
						StartCoroutine(CooldownAttack());
					}
				}
			} else//If the player isn't in vision range or chasing range the golem going to be in "Idle"  state 
			{
				Walk(false);
			}
		} else 
		{
			if(!animDeadPlayed) 
			{
				Die();
				animDeadPlayed = true;
			}
		}
    }

	private void UpdateLifeBar() 
	{
		life_FillImage.fillAmount = (life / maxLife);
	}

	private void SpawnProjectil() 
	{
		Instantiate(projectilePrefab, startProjectilePosition.transform.position, startProjectilePosition.transform.rotation);
		//var projectileController = obj.GetComponent<BossProjectileController>();
		//projectileController.SetLocalRotation()
	}

	public void GetDamage(float _damage) 
	{
		if(!isDead) 
		{
			life -= _damage;
			UpdateLifeBar();
			animator.SetTrigger(GET_HIT_TRIGGER);
		}
	}

	private void Attack() 
	{
		Walk(false);
		animator.SetTrigger(ATTACK_TRIGGER);
	}

	//TODO: Sentencia para que no deba moverse ni hacer nada hasta que termine el ataque especial
	public void SpecialAttack() 
	{
		if(!isDead && canAttack) 
		{
			Walk(false);
			SpawnProjectil();
			StartCoroutine(CooldownAttack());
			animator.SetTrigger(SPECIAL_ATTACK_TRIGGER);
		}
	}

	private void Walk(bool isWalking) 
	{
		animator.SetBool(WALKING_BOOL, isWalking);
	}

	private void Die() 
	{
		Walk(false);
		animator.SetTrigger(DEATH_TRIGGER);
	}

	private IEnumerator CooldownAttack ()
	{
		yield return new WaitForSeconds(attackCooldownTime);
		canAttack = true;
	}
}
