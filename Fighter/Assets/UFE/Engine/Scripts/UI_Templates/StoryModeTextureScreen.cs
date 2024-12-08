using UnityEngine;
using System.Collections;
using UFE3D;

public class StoryModeTextureScreen : StoryModeScreen {
	#region public instance properties
	public AudioClip onLoadSound;
	public AudioClip music;
	public bool skippable = true;
	public bool stopPreviousSoundEffectsOnLoad = false;
	public float delayBeforePlayingMusic = 0.1f;
	public float delayBeforeGoingToNextScreen = 3f;
	public float minDelayBeforeSkipping = 0.1f;
	#endregion

	#region public override methods
	public override void OnShow (){
		base.OnShow ();
		
		if (this.music != null){
			UFE.DelayLocalAction(delegate(){UFE.PlayMusic(this.music);}, this.delayBeforePlayingMusic);
		}
		
		if (this.stopPreviousSoundEffectsOnLoad){
			UFE.StopSounds();
		}
		
		if (this.onLoadSound != null){
			UFE.DelayLocalAction(delegate(){UFE.PlaySound(this.onLoadSound);}, this.delayBeforePlayingMusic);
		}
		
		this.StartCoroutine(this.ShowScreen());
		
		/*// Lấy thông tin nhân vật Player 1
		var player1Character = UFE.GetPlayer1();  

        // Lấy các alternative costumes của Player 1
		var alternativeCostumes = player1Character.alternativeCostumes;

        // Kiểm tra xem có đủ alternative costumes không
		if (alternativeCostumes != null && alternativeCostumes.Length > 1)
		{
			// Thay đổi skin của nhân vật, ở đây chọn costume thứ 1 (chỉ số 1)
			player1Character.characterPrefab = alternativeCostumes[1].prefab;  // Chọn alternative costume thứ 1

			// Đặt lại nhân vật Player 1 với skin mới
			UFE.SetPlayer1(player1Character);
		}
		else
		{
			Debug.Log("No alternative costumes available for Player 1.");
		}*/

	}
	
	public virtual IEnumerator ShowScreen(){
		float startTime = Time.realtimeSinceStartup;
		float time = 0f;
		
		while(
			time < this.delayBeforeGoingToNextScreen && 
			!(skippable && Input.anyKeyDown && time > this.minDelayBeforeSkipping)
		){
			yield return null;
			time = Time.realtimeSinceStartup - startTime;
		}
		
		this.GoToNextScreen();
	}
	#endregion
}
