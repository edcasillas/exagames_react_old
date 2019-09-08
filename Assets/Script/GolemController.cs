using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	#region
	private readonly string IDLE_STATE_NAME = "Idle1";
	private readonly string WALK_STATE_NAME = "Walk";
	private readonly string DEATH_STATE_NAME = "Death1";
	private readonly string GET_HIT_STATE_NAME = "GetHit2";
	private readonly string NORMAL_ATTACK_STATE_NAME = "Attack3";
	private readonly string FIRE_ATTACK_STATE_NAME = "Attack2";
	#endregion

	#region Animation Parameters
	private readonly string DEATH_TRIGGER = "Death1";
	private readonly string FIRE_ATTACK_TRIGGER = "Attack2";
	private readonly string ATTACK_TRIGGER = "Attack3";
	private readonly string GET_HIT_TRIGGER = "Attack3";
	private readonly string WALKING_BOOL = "Walking";
	#endregion
	
	void Start()
    {
		player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }
	
    void Update()
    {

		if(Input.GetKeyDown(KeyCode.M)) 
		{
			isDead = true;
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
					if (canAttack) {
						canAttack = false;
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

	public void Attack() 
	{
		Walk(false);
		animator.SetTrigger(ATTACK_TRIGGER);
	}

	public void AttackWithFire() 
	{
		Walk(false);
		animator.SetTrigger(FIRE_ATTACK_TRIGGER);
	}

	public void Walk(bool isWalking) 
	{
		animator.SetBool(WALKING_BOOL, isWalking);
	}

	public void Die() 
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
