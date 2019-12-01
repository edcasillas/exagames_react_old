using GolemsEnums;
using System.Collections;
using UnityEngine;

public class FireGolemController : GolemController
{
	#region Shot / Special Ability
	[SerializeField]
	private GameObject startProjectilePosition;
	[SerializeField]
	private GameObject projectilePrefab;
	#endregion

	private void Awake() {
		base.Awake();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		base.Update();
    }

	public override void TriggerSpecialAttack() 
	{
		if (!isDead && canAttack && !takingDamage) 
		{
			ChangeState(GolemStates.SpecialAttacking);
			PlayAnimationWithTrigger(SPECIAL_ATTACK_TRIGGER);
			StartCoroutine(SpawnProjectilWithDelay(1.5f));
			StartCoroutine(CooldownAttack());
			StartCoroutine(ChangeStateWhenAnimationIsOver(SPECIAL_ATTACK_STATE_NAME, GolemStates.Idle));
		}
	}

	private IEnumerator SpawnProjectilWithDelay(float _timeToDelay) {
		yield return new WaitForSeconds(_timeToDelay);
		SpawnProjectil();
	}

	private void SpawnProjectil() {
		Instantiate(projectilePrefab, startProjectilePosition.transform.position, startProjectilePosition.transform.rotation);
		specialAttackTriggered = false;
		canAttack = false;
	}
}
