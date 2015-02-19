private var Scene1 : Rect = Rect(10, 10, 100, 30);
private var Scene2 : Rect= Rect(10, 45, 100, 30);     
private var Scene3 : Rect= Rect(10, 80, 100, 30);        

function OnGUI(){           
        if (GUI.Button(Scene1,"Scene 1")) 
        	Application.LoadLevel ("Scene1");
        	  
        if (GUI.Button(Scene2,"Scene 2"))    
        	Application.LoadLevel ("Scene2"); 
        	
        if (GUI.Button(Scene3,"Scene 3"))        
        	Application.LoadLevel ("Scene3");    
           
}