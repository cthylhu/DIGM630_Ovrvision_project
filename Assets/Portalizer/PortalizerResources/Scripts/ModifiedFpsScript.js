public  var Portal 				: Transform;
public  var otherPortal			: Transform;
public 	var ClonedObject 		: GameObject;
//============================================
public  var Teleported 			: boolean;
//============================================
private var reflection 			: Matrix4x4 = Matrix4x4.zero;
private var rotate 				: Quaternion;
private var AxeRotation			: Quaternion;
private var ObjectRotation		: Quaternion;
//--------------------------------------------------------------
private var PortalPlane					: Plane;
private var tmpVelocityRotationMatrix 	: Matrix4x4;
private var velocity					: Vector3;
private var tempPortal					: Transform; 
//--------------------------------------------------------------
private var tempPosition		: Vector3;
private var tempRotation		: Quaternion;
//--------------------------------------------------------------
public function Init() {
	InitReflectionMatrixAndOtherStuff();	

	if (!ClonedObject) {
	    ClonedObject = new GameObject(name + ' (FPS clone)');
	    ClonedObject.transform.position = reflection.MultiplyPoint(transform.position);
	    ClonedObject.transform.rotation = ObjectRotation * transform.rotation;
	    
	    //WHAT ELSE SHOULD BE CLONED FROM ORIGINAL OBJECT TO LOOK RIGHT
	    //clone camera, animated character or anything what you need
	    //assign masked shader if needed (use ModifiedObjectScript as Example)
   	} 
}
//-----------------------------------------------------------------
function Update() {	
	//Check script. Delete it if needed
   	if (gameObject.tag == 'Player') {
		if (!Teleported) {	
			//Delete script. Object has "visited(not teleported)" Inside zone, but leave it
			DestroyImmediate(transform.gameObject.GetComponent('ModifiedFpsScript'));
			return;
		} else {
			//Object leave Inside zone of one portal, and move to other portal. Script should work
			gameObject.tag = 'PlayerTemp';
			Teleported = false;
		}
	} 
						
	if (ClonedObject) {
		ClonedObject.transform.position			= reflection.MultiplyPoint3x4(transform.position);
		ClonedObject.transform.rotation			= ObjectRotation * transform.rotation;
		
		//CONTROL ALL IMPORTANT PARAMETERS, THAT SHOULD MAKE CLONED OBJECT LOOKS LIKE ORIGINAL 
		//rotate your cloned camera, assign animation, e.t.c (use ModifiedObjectScript as Example)
		
		if (!PortalPlane.GetSide(transform.position)) {
			//Going through portal
			if ((!Portal) || (!otherPortal)) 
				return;
				
			tempPortal	= Portal;
			Portal		= otherPortal;
			otherPortal	= tempPortal;
			//------------------------------------------		
			Teleported	= true;		
			//EXAMPLE OF VELOOCITY TRANSFORMATION
			//velocity = tmpVelocityRotationMatrix.MultiplyPoint(GetComponent('YourSpeedControllerScript').velocity);
			//------------------------------------------
			tempPosition		= ClonedObject.transform.position;
			tempRotation		= ClonedObject.transform.rotation;
	
			ClonedObject.transform.position = transform.position;
			ClonedObject.transform.rotation = transform.rotation;

			transform.position	= tempPosition;	
			transform.rotation	= tempRotation;
			
			//GetComponent('YourSpeedControllerScript').velocity = velocity;
			
			//CLONED AND ORIGINAL CHARACTER SWITCHED THEIR PLACES AND ROTATIONS, 
			//ASSIGN SHADERS OR ANY OTHER ACTION THAT NEEDED
			//rotate your cloned camera, assign animation, e.t.c (use ModifiedObjectScript as Example)
			//Dont forget to make your character stand up again
						
			//Recalculate rotation matrix
			InitReflectionMatrixAndOtherStuff();
	    }
    }	
}
//-----------------------------------------------------------------
function OnDestroy() {
	if (ClonedObject) {
		DestroyImmediate(ClonedObject);
		
		//Assign default shaders to renderes here (use ModifiedObjectScript as Example)
	}
}
//-----------------------------------------------------------------
function InitReflectionMatrixAndOtherStuff() {	
	//calculate mirror reflection matrix
	var reflection1 : Matrix4x4 = CalculateReflectionMatrix(Vector4 (Portal.transform.up.x,  Portal.transform.up.y,  Portal.transform.up.z,  0));
	var reflection2 : Matrix4x4 = CalculateReflectionMatrix(Vector4 (Portal.transform.right.x,  Portal.transform.right.y,  Portal.transform.right.z,  0));				
	//Calculate rotation
	rotate 			= (otherPortal.transform.rotation) * Quaternion.Inverse(Portal.transform.rotation);
	AxeRotation 	= Quaternion.AngleAxis(180, Portal.transform.forward);
	ObjectRotation	=  rotate * AxeRotation;
	//Calculate final reflection
	//Step1 Move to BEGIN OF COORDINATES  		
	reflection  = Matrix4x4.TRS(otherPortal.transform.position + otherPortal.transform.up*0.01, Quaternion.identity, new Vector3(1,1,1));			
	//Step2 Rotate Object on Difference Quaternion between 2 portals
	reflection *= Matrix4x4.TRS(new Vector3(0,0,0), rotate, new Vector3(1,1,1));
	//Step3 reflect from Up and Right vectors	
	reflection *= reflection1 * reflection2;
	//Step4 Move to Other portal position 
	reflection *= Matrix4x4.TRS(-(Portal.transform.position + Portal.transform.up*0.01), Quaternion.identity, new Vector3(1,1,1));	
	//Oteh variables
	PortalPlane = Plane(Portal.transform.up, Portal.transform.position  + Portal.transform.up*0.008);
	tmpVelocityRotationMatrix	= Matrix4x4.TRS(new Vector3(0,0,0), ObjectRotation, new Vector3(1,1,1));
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