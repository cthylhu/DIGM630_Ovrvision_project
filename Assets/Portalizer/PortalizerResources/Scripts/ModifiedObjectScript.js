public  var Portal 				: Transform;
public  var Clone 				: boolean;
public  var otherPortal			: Transform;
public 	var ClonedObject 		: Transform;
//-----------------------------------------------------------------
private var reflection 			: Matrix4x4 = Matrix4x4.zero;
private var rotate 				: Quaternion;
private var AxeRotation			: Quaternion;
private var ObjectRotation		: Quaternion;
private var PortalPlane			: Plane;
private var tmpVelocityRotationMatrix 	: Matrix4x4;
private var MOS 				: ModifiedObjectScript;
private var SpeedStabilisation  = 0.98;
//-----------------------------------------------------------------
private var ChildrenRenderer	: Array;
public	var Shaders				: Hashtable;
//-----------------------------------------------------------------
function GetMaskedShader(ShaderName : String, PortalTag : String) {	
	var shader = 'Portal/Masked/' + ShaderName;
	
	if (PortalTag == 'Portal1') {
		shader	+= ' Portal 1';
	} else {
		shader	+= ' Portal 2';
	}

	return shader;
}
//-----------------------------------------------------------------
public function Init() {
	if (!Clone) { 
		
		//Calculate transformations matrixs 
		if (reflection == Matrix4x4.zero) {					
			InitReflectionMatrixAndOtherStuff();	
		}		
		
		//Calculate new position for cloned object
		var position = reflection.MultiplyPoint(transform.position);
		
		//Disable collider, to make clone of object that would not collide with original,
		//even if portals are to close
		collider.isTrigger = true;
			
		//Now we create clone
		if (!ClonedObject) {
		    ClonedObject = Instantiate(transform, position, ObjectRotation * transform.rotation);
		    ClonedObject.gameObject.layer = 9;
		    
		    //Disable collision with original object
		    Physics.IgnoreCollision(rigidbody.collider, ClonedObject.rigidbody.collider);	
		    
		    //Enable trigers. Now it can collide with other objects    
		    collider.isTrigger 				= false;    
		    ClonedObject.collider.isTrigger = false;
		    
		    MOS = ClonedObject.GetComponent('ModifiedObjectScript');
		    MOS.Clone 						= true;
		    MOS.Portal 						= otherPortal;
		    MOS.otherPortal 				= Portal;
		    
		    //Make object kinematic. Now it's easy manipulate with it
		    ClonedObject.rigidbody.isKinematic		= true;
		    
		    //Get and store all renderers of ClonedObject
			MOS.ChildrenRenderer	= ClonedObject.GetComponentsInChildren(Renderer);
			MOS.Shaders				= new Hashtable(); 
			
			//Apply new shader
			for (var renderer_c : Renderer in MOS.ChildrenRenderer) {
				var _shaderName = renderer_c.material.shader.name;			
				MOS.Shaders.Add(renderer_c, _shaderName);
				renderer_c.material.shader = Shader.Find (GetMaskedShader(_shaderName, otherPortal.tag));
			} 
			//Set animation if exist
			var origlAnimation	= GetComponent(Animation);
			var cloneAnimation	= ClonedObject.GetComponent(Animation);
			
			if (origlAnimation && cloneAnimation) {
				cloneAnimation[origlAnimation.clip.name].time = origlAnimation[origlAnimation.clip.name].time;
			}    
		} 
		
		//Get and store all renderers of object
		ChildrenRenderer	= GetComponentsInChildren(Renderer);
		Shaders				= new Hashtable();
		
		//Apply new shader to them
		for (var renderer_ : Renderer in ChildrenRenderer) {
			var o_shaderName = renderer_.material.shader.name;	
			Shaders.Add(renderer_, o_shaderName);
			renderer_.material.shader = Shader.Find (GetMaskedShader(o_shaderName, Portal.tag));
		} 
	}
}
//-----------------------------------------------------------------
function Update() {
	//Calculating currunt position and rotation of object relative to Portal
	if (!Clone) {
		if (ClonedObject) {		
			if (otherPortal) {
				if (reflection == Matrix4x4.zero) {					
					InitReflectionMatrixAndOtherStuff();	
				}				
				ClonedObject.position			= reflection.MultiplyPoint3x4(transform.position);
				ClonedObject.transform.rotation = ObjectRotation * transform.rotation;
				
				//Set animation if exist
				var origlAnimation	= GetComponent(Animation);
				var cloneAnimation	= ClonedObject.GetComponent(Animation);
				
				if (origlAnimation && cloneAnimation) {
					cloneAnimation[origlAnimation.clip.name].time = origlAnimation[origlAnimation.clip.name].time;
				}  
			}		
	
			if (!PortalPlane.GetSide(transform.position)) {				
				if ((!Portal) || (!otherPortal))
					return;
					
				//Modifying Cloned Object, make it real
				Clone 					= true;
				var name_ 				= name;
				name 					= ClonedObject.name;
				
				rigidbody.isKinematic	= true;
				ClonedObject.name 		= name_;
				MOS 					= null;	
				MOS 					= ClonedObject.GetComponent('ModifiedObjectScript');	
				if (MOS) {
					MOS.ClonedObject	= transform;							 
					MOS.Clone			= false;
				} 

				ClonedObject.rigidbody.isKinematic		= false;
				ClonedObject.rigidbody.velocity 		= tmpVelocityRotationMatrix.MultiplyPoint(rigidbody.velocity * SpeedStabilisation);
				ClonedObject.rigidbody.angularVelocity	= tmpVelocityRotationMatrix.MultiplyPoint(rigidbody.angularVelocity);
				//Fast layer switching
				gameObject.layer 				= 9;
				ClonedObject.gameObject.layer	= 11;
				//
				ClonedObject = null;
		    }
	    }	
	}
}
//-----------------------------------------------------------------
function OnDestroy() {
	if (!Clone) {
		if (ClonedObject) {
			DestroyImmediate(ClonedObject.gameObject);
			
			for (var renderer_d : Renderer in ChildrenRenderer) {
				renderer_d.material.shader = Shader.Find(Shaders[renderer_d]);
			}
		}
	}
}
//-----------------------------------------------------------------
function InitReflectionMatrixAndOtherStuff() {	
	//calculate mirror reflection matrix
	var reflection1 : Matrix4x4 = CalculateReflectionMatrix(Vector4 (Portal.up.x,  Portal.up.y,  Portal.up.z,  0));
	var reflection2 : Matrix4x4 = CalculateReflectionMatrix(Vector4 (Portal.right.x,  Portal.right.y,  Portal.right.z,  0));
	//Calculate rotation
	rotate 			= (otherPortal.rotation) * Quaternion.Inverse(Portal.rotation);
	AxeRotation 	= Quaternion.AngleAxis(180, Portal.forward);
	ObjectRotation	= rotate * AxeRotation;
	//Calculate final reflection
	//Step1 Move to BEGIN OF COORDINATES  		
	reflection  = Matrix4x4.TRS((otherPortal.position + otherPortal.up*0.01), Quaternion.identity, new Vector3(1,1,1));			
	//Step2 Rotate Object on Difference Quaternion between 2 portals
	reflection *= Matrix4x4.TRS(new Vector3(0,0,0), rotate, new Vector3(1,1,1));
	//Step3 reflect from Up and Right vectors	
	reflection *= reflection1 * reflection2;
	//Step4 Move to Other portal position 
	reflection *= Matrix4x4.TRS(-(Portal.position + Portal.up*0.01), Quaternion.identity, new Vector3(1,1,1));
	//Init other variables
	PortalPlane = Plane(Portal.up, Portal.position + Portal.up*0.008);
	tmpVelocityRotationMatrix = Matrix4x4.TRS(new Vector3(0,0,0), ObjectRotation, new Vector3(1,1,1));	
}
//-----------------------------------------------------------------
function CalculateReflectionMatrix (plane : Vector4)
{
	var reflectionMat : Matrix4x4;
	
    reflectionMat.m00 = (1F - 2F*plane[0]*plane[0]);
    reflectionMat.m01 = (    -2F*plane[0]*plane[1]);
    reflectionMat.m02 = (    -2F*plane[0]*plane[2]);
    reflectionMat.m03 = (    -2F*plane[3]*plane[0]);

    reflectionMat.m10 = (    -2F*plane[1]*plane[0]);
    reflectionMat.m11 = (1F - 2F*plane[1]*plane[1]);
    reflectionMat.m12 = (    -2F*plane[1]*plane[2]);
    reflectionMat.m13 = (    -2F*plane[3]*plane[1]);

    reflectionMat.m20 = (    -2F*plane[2]*plane[0]);
    reflectionMat.m21 = (    -2F*plane[2]*plane[1]);
    reflectionMat.m22 = (1F - 2F*plane[2]*plane[2]);
    reflectionMat.m23 = (    -2F*plane[3]*plane[2]);

    reflectionMat.m30 = 0F;
    reflectionMat.m31 = 0F;
    reflectionMat.m32 = 0F;
    reflectionMat.m33 = 1F;
    
    return reflectionMat;
}