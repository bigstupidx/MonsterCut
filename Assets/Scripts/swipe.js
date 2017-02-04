#pragma strict

var startTime: float;
var startPos: Vector2;
var couldBeSwipe: boolean;
var comfortZone: float;
var minSwipeDist: float;
var maxSwipeTime: float;
var swipeTime:float;
var swipeDist:float;
var swipeDirection:int;
//private var area:Rect;

function Start(){
	//area=Rect(Screen.width-200, 100, 200, Screen.height-200);
}

function Update() {

#if UNITY_EDITOR || UNITY_WEBPLAYER
	if(Input.GetMouseButtonDown(0)){//&&area.Contains(Input.mousePosition)){
		startPos=Input.mousePosition;
		startTime = Time.time;
		couldBeSwipe=true;
	}
	if(Input.GetMouseButtonUp(0)&&couldBeSwipe){
		swipeTime = Time.time - startTime;
        swipeDist = (Input.mousePosition - startPos).magnitude;
        if (couldBeSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist)) {
            // It's a swiiiiiiiiiiiipe!
            swipeDirection = Mathf.Sign(Input.mousePosition.x - startPos.x);
			if(swipeDirection==-1){
				gameObject.SendMessage("swipe");
			}
			if(swipeDirection==1){
				gameObject.SendMessage("swipeBack");
			}
        }
        
		couldBeSwipe=false;
	}
    
#else
	if (Input.touchCount == 1) {
        var touch:Touch = Input.touches[Input.touchCount-1];
        //if(previusTouch==null) previusTouch=touch;
        //else if(previusTouch!=touch){ previusTouch==null; couldBeSwipe = false; }
        switch (touch.phase) {
            case TouchPhase.Began:
                couldBeSwipe = true;
                startPos = touch.position;
                startTime = Time.time;
                break;
            case TouchPhase.Moved:
                if (Mathf.Abs(touch.position.y - startPos.y) > comfortZone) {
                    couldBeSwipe = false;
                }
                break;
            case TouchPhase.Stationary:
                //couldBeSwipe = false;
                break;
            case TouchPhase.Ended:
                swipeTime = Time.time - startTime;
                swipeDist = (touch.position - startPos).magnitude;
                if (couldBeSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist)) {
                    // It's a swiiiiiiiiiiiipe!
                    swipeDirection = Mathf.Sign(touch.position.x - startPos.x);
					print(swipeDirection);
					if(swipeDirection==-1){
						print("swipe");
						gameObject.SendMessage("swipe");
					}
					if(swipeDirection==1){
						print("swipeback");
						gameObject.SendMessage("swipeBack");
					}
                    // Do something here in reaction to the swipe.
                }
                break;
        }
    }
    else{
    	//startPos = touch.position;
        //startTime = Time.time;
        couldBeSwipe = false;
    }
#endif
}

function disableSwipe(){
	couldBeSwipe=false;
}