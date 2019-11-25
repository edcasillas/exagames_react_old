using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGolemController : _GolemController
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

	}
}
