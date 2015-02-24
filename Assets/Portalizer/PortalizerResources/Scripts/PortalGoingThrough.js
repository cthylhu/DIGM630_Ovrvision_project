public	var otherPortal	: Transform;
private var IgnoredWall : Collider;
private var ModifiedObjectScript_Script	: ModifiedObjectScript;
private var ModifiedFpsScript_Script	: ModifiedFpsScript;
//-----------------------------------------------------------------
function Start() {
	IgnoredWall = transform.parent.parent.GetComponent(Collider);
}
//-----------------------------------------------------------------
function OnTriggerEnter (other : Collider) {
	if (!otherPortal) {
		return;
	}
	
	if ((other.gameObject.layer==9) || (other.gameObject.layer==11)){ //IF object has Collider
	
		ModifiedObjectScript_Script = other.gameObject.GetComponent('ModifiedObjectScript');
		
		if (!ModifiedObjectScript_Script) {
			ModifiedObjectScript_Script				= other.gameObject.AddComponent('ModifiedObjectScript');
			ModifiedObjectScript_Script.Portal		= transform.parent;
			ModifiedObjectScript_Script.otherPortal = otherPortal;
			ModifiedObjectScript_Script.Clone		= (9 == other.gameObject.layer);
			ModifiedObjectScript_Script.Init();
		}
		
		if (IgnoredWall) {
			Physics.IgnoreCollision(other, IgnoredWall);
		}
	} else if (other.gameObject.layer==10) { //If object has CharacterController				
		ModifiedFpsScript_Script = other.gameObject.GetComponent('ModifiedFpsScript');
		
		if (ModifiedFpsScript_Script) {			
			if (other.gameObject.tag == 'Player') {
				other.gameObject.tag				= 'PlayerTemp';
				ModifiedFpsScript_Script.Teleported	= false;
			} 
		} else {			
			other.gameObject.tag					= 'PlayerTemp';
			ModifiedFpsScript_Script				= other.gameObject.AddComponent('ModifiedFpsScript');
			ModifiedFpsScript_Script.Portal			= transform.parent;
			ModifiedFpsScript_Script.otherPortal	= otherPortal;				
			ModifiedFpsScript_Script.Init();				
		}	
	}	   			
}
//-----------------------------------------------------------------
function OnTriggerExit(other : Collider) {	
	if (!otherPortal) {
		return;
	}
	
	if (other.gameObject.layer == 10) {
		//We cant destroy ModifiedFpsScript from here. We saying to our script, that he could be deleted if he wont
		if (other.gameObject.tag == 'PlayerTemp') {
			other.gameObject.tag = 'Player';
		}
	} else if (other.gameObject.layer == 11) {
		if (IgnoredWall) {	
			Physics.IgnoreCollision(other, IgnoredWall, false);
		}
		
		ModifiedObjectScript_Script = other.transform.gameObject.GetComponent('ModifiedObjectScript');
		Destroy(ModifiedObjectScript_Script);
	}	
}
//------------------------------------------------------------------