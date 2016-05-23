function rotation(){
	var piece = $("#piecesContainer .pieces"), degree = 0, timer;
	updateRotate();
	function updateRotate(){
		piece.css({ WebkitTransform: 'rotate(' + degree + 'deg)'});  
		piece.css({ '-moz-transform': 'rotate(' + degree + 'deg)'});                      
		timer = setTimeout(function() {
			degree -= 0.3; 
			if(degree < -360){
				degree += 360;
			}		
			updateRotate();
		},5);
	}
} 
rotation();

var i=0;
function floatLeft(){
	i++;
	var random=Math.floor((Math.random() * 3000) + 7000);
	$("#piece_"+i).animate({"left":"-20%"},random, function(){
    	$(this).css({"left": "110%"});
	});
  	if("piece_"+i == $("#piecesContainer .pieces").last().attr("id")){
   		i = 0;
	}
  	setTimeout(floatLeft, random-7000);
}
floatLeft();