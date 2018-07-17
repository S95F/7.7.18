using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Scripts by Steven Funk
// 7.10.18
public class healthbar : MonoBehaviour {

	private List<GameObject> bars;
	private int enemies = 0;
	private int allies = 0;
	public Canvas enemy;
	public Canvas ally;
	private void Start () {
		bars = new List<GameObject> ();
		createBar ();
	}

	private void Updatehbar(int target){

		// aquires parts so we can change a particular health bar
		Image hpbar;
		Text txt; 
		hpbar = bars [target].gameObject.GetComponentsInChildren<Image> ()[1];
		txt = bars [target].gameObject.GetComponentInChildren<Text> ();
		// aquires values from the object
		hbar barvars = bars [target].GetComponent<hbar> ();
		float curhp = barvars.curhp;
		float maxhp = barvars.maxhp;
		// ratio is made and applied to the transform and text is adjusted to display the ratio
		float ratio = curhp / maxhp;
		hpbar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		txt.text = curhp.ToString() + " / " + maxhp.ToString(); 
	}
	public void DealDmg(float dmg, int target){
		// aquires values from the object
		hbar barvars = bars [target].GetComponent<hbar> ();
		float curhp = barvars.curhp;
		float maxhp = barvars.maxhp;
		// decrements and makes sure we dont go under
		curhp -= Mathf.Abs(dmg);
		if (curhp < 0)
			curhp = 0;

		barvars.curhp = curhp;
		Updatehbar (target);
	}

	public void HealDmg(float dmg, int target){
		// aquires values from the object
		hbar barvars = bars [target].GetComponent<hbar> ();
		float curhp = barvars.curhp;
		float maxhp = barvars.maxhp;
		// decrements and makes sure we dont go under
		curhp += Mathf.Abs(dmg);
		if (curhp > maxhp)
			curhp = maxhp;
		barvars.curhp = curhp;
		Updatehbar (target);
	}
	private void createEnemyBar(string name, int maxHP = default(int)){
		bars.Add(Instantiate ((GameObject)Resources.Load ("hbar"),enemy.transform));
		if (maxHP > 0) {
			hbar barvars = bars [bars.Count-1].GetComponent<hbar> ();
			barvars.maxhp = maxHP;
			barvars.curhp = maxHP;
		}
		enemies++;
		bars [bars.Count-1].transform.localPosition -= new Vector3 (70, 50*enemies + 75*(enemies-1), 0);
		Updatehbar (bars.Count-1);
		Text txt; 
		txt = bars [bars.Count-1].gameObject.GetComponentsInChildren<Text> ()[1];
		txt.text = name;
	}
	private void createAllyBar(string name, int maxHP = default(int)){
		bars.Add(Instantiate ((GameObject)Resources.Load ("hbar"),ally.transform));
		if (maxHP > 0) {
			hbar barvars = bars [bars.Count-1].GetComponent<hbar> ();
			barvars.maxhp = maxHP;
			barvars.curhp = maxHP;
		}
		allies++;
		bars [bars.Count-1].transform.localPosition -= new Vector3 (-30, 50*allies + 75*(allies-1), 0);
		Updatehbar (bars.Count-1);
		Text txt; 
		txt = bars [bars.Count-1].gameObject.GetComponentsInChildren<Text> ()[1];
		txt.text = name;
	}
	private void createBar(){
		
		createEnemyBar ("Cubesies");
		createAllyBar ("Steven",200);
	}
}
