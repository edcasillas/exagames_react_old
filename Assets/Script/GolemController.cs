using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBarController))]
public class GolemController : MonoBehaviour {
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
	private bool specialAttackTriggered;

	private HealthBarController healthBarController;

	[SerializeField]
	private PlayerController player;

	private bool isDead = false;
	private bool animDeadPlayed= false;

	private readonly string PLAYER_TAG = "Player";

	#region Boss Stats
	[SerializeField]
	private int maxLife;

	private int _health;
	public int Health {
		get { return _health; }
		private set {
			_health = value;
			healthBarController.UpdateBar(_health);
		}
	}

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

	public  void SetPlayer(PlayerController _player) 
	{
		player = _player;
	}

	public void SetSpecialAttackTriggered(bool _specialAttackTriggered) 
	{
		specialAttackTriggered = _specialAttackTriggered;
	}

	private void Awake() {
		healthBarController = GetComponent<HealthBarController>();
		Health = maxLife;
	}

	private void Update()
    {
		if (Health <= 0)
			isDead = true;

		if(Input.GetKeyDown(KeyCode.R)) 
		{
			GetDamage(10);
		}

		if(Input.GetKeyDown(KeyCode.F)) {
			SetSpecialAttackTriggered(true);
		}

		if(!isDead) 
		{
			//When the player is in range the boss go to follow him and try to attack him
			if (player.Health > 0 && Vector3.Distance(player.transform.position, transform.position) < maxChasingRange) 
			{
				Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
				if (Vector3.Distance(playerPos, transform.position) > maxCloseDistance && (!specialAttackTriggered || !canAttack) && 
																							(animator.GetCurrentAnimatorStateInfo(0).IsName(IDLE_STATE_NAME) || animator.GetCurrentAnimatorStateInfo(0).IsName(WALK_STATE_NAME)))
				{
					transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
					transform.position = Vector3.MoveTowards(transform.position, playerPos, walkSpeed * Time.deltaTime);
					Walk(true);
				}else if(canAttack) 
				{
					if(specialAttackTriggered) 
					{
						Debug.Log("SpecialAttack! ");
						SpecialAttack();
					}else if(Vector3.Distance(playerPos, transform.position) < maxCloseDistance)
					{
						Debug.Log("Attack! ");
						Attack();
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
    
	public void GetDamage(int _damage) 
	{
		if(!isDead) 
		{
			Health -= _damage;
			animator.SetTrigger(GET_HIT_TRIGGER);
		}
	}

	private void Attack() 
	{
		canAttack = false;
		Walk(false);
		animator.SetTrigger(ATTACK_TRIGGER);
		StartCoroutine(CooldownAttack());
	}

	private void SpecialAttack() 
	{
		if(!isDead && canAttack) 
		{
			canAttack = false;
			specialAttackTriggered = false;
			Walk(false);
			animator.SetTrigger(SPECIAL_ATTACK_TRIGGER);
			StartCoroutine(SpawnProjectilWithDelay(1.5f));
			StartCoroutine(CooldownAttack());
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

	private void SpawnProjectil() {
		Instantiate(projectilePrefab, startProjectilePosition.transform.position, startProjectilePosition.transform.rotation);
		//var projectileController = obj.GetComponent<BossProjectileController>();
		//projectileController.SetLocalRotation()
	}

	private IEnumerator SpawnProjectilWithDelay(float _timeToDelay) 
	{
		yield return new WaitForSeconds(_timeToDelay);
		SpawnProjectil();
	}

	private IEnumerator CooldownAttack ()
	{
		yield return new WaitForSeconds(attackCooldownTime);
		canAttack = true;
	}
}
