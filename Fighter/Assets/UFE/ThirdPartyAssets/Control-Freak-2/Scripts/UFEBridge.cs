using System;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;
using UnityEngine.Serialization;
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

		public GameObject btnUnti;

		// -----------------
		public override void Init()
		{
			btnUnti.SetActive((false));
			rig = GetComponentInChildren<InputRig>();

			UFE.OnMove += OnMovePerformed;
			UFE.OnGameBegin += OnInitAbility;
			UFE.OnGameEnds += DeActivePanel;

			UFE.OnGaugeUpdate += ActiveUntiBtn;


            #if UNITY_EDITOR
			if (rig == null)
			{
				Debug.LogError("UFE Bridge [" + this.name + "] is missing an Input Rig!!");
			}
            #endif

			CF2Input.activeRig = this.rig;
		}

		private void ActiveUntiBtn(int targetgauge, float newvalue, ControlsScript character)
		{
			if (character.playerNum != 1) return;
			if (character.isAssist)       return;
			if (character.currentGaugesPoints[1] >= UFE.config.player1Character.maxGaugePoints)
			{
				btnUnti.SetActive(true);
			}
			else
			{
				btnUnti.SetActive((false));
			}
		}

		private void DeActivePanel(ControlsScript winner, ControlsScript loser)
		{
			root.SetActive(false);
		}

		public DataBtn dataBtn1;

		public DataBtn dataBtn2;

		public DataBtn dataBtn3;

		public DataSkill dataSkillJack;
		
		public DataSkill dataSkillRock;

		[FormerlySerializedAs("imagesIconJacAbilityUis")] public List<AbilityUI> imagesIconUIs;

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

				for (int i = 0; i < imagesIconUIs.Count; i++)
				{
					imagesIconUIs[i].UpdateUi(data[i].icon, data[i].frame, data[i].cost);
				}
			}
			if (player1.myInfo.characterName == "Rock")
			{
				root.SetActive(true);

				var data = dataSkillRock.dataAbilities;

				for (int i = 0; i < imagesIconUIs.Count; i++)
				{
					imagesIconUIs[i].UpdateUi(data[i].icon, data[i].frame, data[i].cost);
				}
			}
		}

		void OnDisable()
		{
			UFE.OnMove -= OnMovePerformed;
			UFE.OnGameBegin -= OnInitAbility;
			UFE.OnGameEnds -= DeActivePanel;

			UFE.OnGaugeUpdate -= ActiveUntiBtn;
		}

		// --------------
		public override float GetAxis(string axisName)
		{
			return CF2Input.GetAxis(axisName);
		}

		// --------------
		public override float GetAxisRaw(string axisName)
		{
			return CF2Input.GetAxisRaw(axisName);
		}

		// --------------
		public override bool GetButton(string axisName)
		{
			return CF2Input.GetButton(axisName);
		}


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
