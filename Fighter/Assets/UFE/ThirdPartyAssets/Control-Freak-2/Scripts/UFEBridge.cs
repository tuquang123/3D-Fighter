using System;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ControlFreak2
{
	[Serializable]
	public class DataAbility
	{
		public int cost;
		public string id;
		public Sprite icon;
		public Sprite frame;
	}
	[Serializable]
	public class DataBtn
	{
		public string type;
		//public int current;
		public List<GameObject> btn;
	}
public class UFEBridge : UFE3D.InputTouchControllerBridge 
	{
	private InputRig rig;
	// -----------------
	public override void Init()
		{
		rig = GetComponentInChildren<InputRig>();
		
		UFE.OnMove += OnMovePerformed;
		UFE.OnGameBegin += OnInitAbility;
		UFE.OnGameEnds += DeActivePanel;


#if UNITY_EDITOR
		if (rig == null)
			{
			Debug.LogError("UFE Bridge [" + this.name + "] is missing an Input Rig!!");
			}
#endif		
	
		CF2Input.activeRig = this.rig;
		}

	private void DeActivePanel(ControlsScript winner, ControlsScript loser)
	{
		root.SetActive(false);
	}

	public DataBtn dataBtn1;
	
	public DataBtn dataBtn2;
	
	public DataBtn dataBtn3;

	public DataSkill dataSkillJack;
	
	public List<AbilityUI> imagesIconJacAbilityUis;
	
	public GameObject root;

	void Awake()
	{
		//UFE.OnMove += OnMovePerformed;
		//UFE.OnGameBegin += OnInitAbility;
	}

	void OnMovePerformed(MoveInfo move, ControlsScript player)
	{
		if (move == null) return;

		if (player.playerNum == 1)
		{
			HandleMove(dataBtn1, move);
			HandleMove(dataBtn2, move);
			HandleMove(dataBtn3, move);
		}
	}
	
	void HandleMove(DataBtn dataBtn, MoveInfo move)
	{
		if (move.description == dataBtn.type)
		{
			foreach (var btn in dataBtn.btn)
			{
				btn.SetActive(false);
			}
            
			if (dataBtn.btn.Count > 0)
			{
				int index = Random.Range(0, dataBtn.btn.Count);
				dataBtn.btn[index].SetActive(true);
			}
		}
	}

	public void OnInitAbility(ControlsScript player1, ControlsScript player2, StageOptions stage)
	{
		if (player1.myInfo.characterName == "Jack")
		{
			root.SetActive(true);
			
			var data = dataSkillJack.dataAbilities;
			
			for (int i = 0; i < imagesIconJacAbilityUis.Count; i++)
			{
				imagesIconJacAbilityUis[i].UpdateUi(data[i].icon,data[i].frame,data[i].cost);
			}
		}
		
	}
	void OnDisable()
	{
		UFE.OnMove -= OnMovePerformed;
		UFE.OnGameBegin -= OnInitAbility;
		UFE.OnGameEnds -= DeActivePanel;
	}

	// --------------
	public override float GetAxis(string axisName)
		{ return CF2Input.GetAxis(axisName);  }

	// --------------
	public override float GetAxisRaw(string axisName)
		{ return CF2Input.GetAxisRaw(axisName);  }

	// --------------
	public override bool GetButton(string axisName)
		{ return CF2Input.GetButton(axisName);  }


	// --------------------
	public override void ShowBattleControls(bool visible, bool animate)
		{
		if (this.rig == null)
			return;

		if (visible && (CF2Input.activeRig != this.rig))
			CF2Input.activeRig = this.rig;

		if (visible == !this.rig.AreTouchControlsHiddenManually())
			return;

		this.rig.ShowOrHideTouchControls(visible, !animate);
		}
	}
}
