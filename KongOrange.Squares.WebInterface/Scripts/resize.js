//RESIZING
if(device == "touch"){
	$(document).on("touchstart", ".resizeButton", selectButton);
	$(document).on("touchmove", moveButton);
	$(document).on("touchend", releaseButton);
}
else if(device == "pc"){
	$(document).on("mousedown", ".resizeButton", selectButton);
	$(document).on("mousemove", moveButton);
	$(document).on("mouseup", releaseButton);
}

var resizeWidth = $(".resizeButton").width();
var resizeActive = false;
var resizeStatus = "none";
var resizeButton,
buttonY,
buttonX,
cornerX,
cornerY;
function selectButton(e){
	resizeStatus = "none";
	e.preventDefault();
	$(".resizeButton").finish();
	if(!mouseDown && !resizeActive){
		resizeActive = true;
		resizeButton = $(this);
		buttonY = parseInt(resizeButton.css("top"));
		buttonX = parseInt(resizeButton.css("left"))
		var buttonID = this.id;
		switch(buttonID){
			case "tl": cornerX = 0; cornerY = 0; break;
			case "tr": cornerX = gridWidth; cornerY = 0; break;
			case "br": cornerX = gridWidth; cornerY = gridWidth; break;
			case "bl": cornerX = 0; cornerY = gridWidth; break;
		}
	   
		var parentOffset = $("#grid").offset(); //coordinates relative to the grid
		
		if(device == "touch"){
			var relX = e.originalEvent.changedTouches[0].pageX - parentOffset.left;
			var relY = e.originalEvent.changedTouches[0].pageY - parentOffset.top;
		}else if(device == "pc"){
			var relX = e.pageX - parentOffset.left; //X coords of mouse click
			var relY = e.pageY - parentOffset.top; //Y coords of mouse click
		}
		
		//copiedPiece.animate({width: squareWidth+"px", height: squareWidth+"px"}, 300, "linear");
		
	}
}
function moveButton(e){
	if(resizeActive){
		var parentOffset = $("#grid").offset();
		if(device == "touch"){
			var relX = e.originalEvent.changedTouches[0].pageX - parentOffset.left;
			var relY = e.originalEvent.changedTouches[0].pageY - parentOffset.top;
		}else if(device == "pc"){
			var relX = e.pageX - parentOffset.left; //X coords of mouse click
			var relY = e.pageY - parentOffset.top; //Y coords of mouse click
		}
		var xDist = Math.abs(relX-cornerX);
		var yDist = Math.abs(relY-cornerY);
		var finalDist = (xDist+yDist)/2;
		
		if(resizeButton.attr("id") == "tl"){
			if(relX > cornerX && relY > cornerY && gridLength > 2){
				if(finalDist > squareWidth*0.6){
					resizeStatus = "shrinking";
					resizeButton.animate({"left": buttonX+squareWidth, "top": buttonY+squareWidth},100);
					$(".outerLeft, .outerTop").css({"opacity": 0.2});
					$(".outerLeft, .outerTop").parent(".pieceWrapper").css({"background-color": "rgba(48,48,48,0.3)"});
				}
				else{
					resizeButton.finish();
					resizeButton.css({"left": finalDist+cornerX-resizeWidth*0.25, "top": finalDist+cornerY-resizeWidth*0.25});
					$(".outerLeft, .outerTop").css({"opacity": 1});
					$(".outerLeft, .outerTop").parent(".pieceWrapper").css({"background-color": "transparent"});
					resizeStatus = "none";
				}
			}
			else if(!(relX > cornerX && relY > cornerY) && gridLength < 6){
				if(finalDist > 30){
					resizeStatus = "enlarging";
					resizeButton.animate({"left": buttonX-60, "top": buttonY-60},100);
					$("#gridBorder").css({"width": "calc(107.5% + 50px)", "height": "calc(107.5% + 50px)", "left": "calc(-3.75% - 50px)", "top": "calc(-3.75% - 50px)"});
				}
				else{
					resizeButton.finish();
					resizeButton.animate({"left": -finalDist+cornerX-resizeWidth*0.25, "top": -finalDist+cornerY-resizeWidth*0.25},100);
					$("#gridBorder").css({"width": "107.5%", "height": "107.5%", "left": "-3.75%", "top": "-3.75%"});
					resizeStatus = "none";
				}
			}
		}
		
		else if(resizeButton.attr("id") == "tr"){
			if(relX < cornerX && relY > cornerY && gridLength > 2){
				if(finalDist > squareWidth*0.6){
					resizeStatus = "shrinking";
					resizeButton.animate({"left": buttonX-squareWidth, "top": buttonY+squareWidth},100);
					$(".outerRight, .outerTop").css({"opacity": 0.2});
					$(".outerRight, .outerTop").parent(".pieceWrapper").css({"background-color": "rgba(48,48,48,0.3)"});
				}
				else{
					resizeButton.finish();
					resizeButton.css({"left": -finalDist+cornerX-resizeWidth*0.75, "top": finalDist+cornerY-resizeWidth*0.25});
					$(".outerRight, .outerTop").css({"opacity": 1});
					$(".outerRight, .outerTop").parent(".pieceWrapper").css({"background-color": "transparent"});
					resizeStatus = "none";
				}
			}
			else if(!(relX < cornerX && relY > cornerY) && gridLength < 6){
				if(finalDist > 30){
					resizeStatus = "enlarging";
					resizeButton.animate({"left": buttonX+60, "top": buttonY-60},100);
					$("#gridBorder").css({"width": "calc(107.5% + 50px)", "height": "calc(107.5% + 50px)", "top": "calc(-3.75% - 50px)"});
				}
				else{
					resizeButton.finish();
					resizeButton.animate({"left": finalDist+cornerX-resizeWidth*0.75, "top": -finalDist+cornerY-resizeWidth*0.25},100);
					$("#gridBorder").css({"width": "107.5%", "height": "107.5%", "left": "-3.75%", "top": "-3.75%"});
					resizeStatus = "none";
				}
			}
		}
		
		else if(resizeButton.attr("id") == "br"){
			if(relX < cornerX && relY < cornerY && gridLength > 2){
				if(finalDist > squareWidth*0.6){
					resizeStatus = "shrinking";
					resizeButton.animate({"left": buttonX-squareWidth, "top": buttonY-squareWidth},100);
					$(".outerRight, .outerBottom").css({"opacity": 0.2});
					$(".outerRight, .outerBottom").parent(".pieceWrapper").css({"background-color": "rgba(48,48,48,0.3)"});
				}
				else{
					resizeButton.finish();
					resizeButton.css({"left": -finalDist+cornerX-resizeWidth*0.75, "top": -finalDist+cornerY-resizeWidth*0.75});
					$(".outerRight, .outerBottom").css({"opacity": 1});
					$(".outerRight, .outerBottom").parent(".pieceWrapper").css({"background-color": "transparent"});
					resizeStatus = "none";
				}
			}
			else if(!(relX < cornerX && relY < cornerY) && gridLength < 6){
				if(finalDist > 30){
					resizeStatus = "enlarging";
					resizeButton.animate({"left": buttonX+60, "top": buttonY+60},100);
					$("#gridBorder").css({"width": "calc(107.5% + 50px)", "height": "calc(107.5% + 50px)"});
				}
				else{
					resizeButton.finish();
					resizeButton.animate({"left": finalDist+cornerX-resizeWidth*0.75, "top": finalDist+cornerY-resizeWidth*0.75},100);
					$("#gridBorder").css({"width": "107.5%", "height": "107.5%", "left": "-3.75%", "top": "-3.75%"});
					resizeStatus = "none";
				}
			}
		}
		
		else if(resizeButton.attr("id") == "bl"){
			if(relX > cornerX && relY < cornerY && gridLength > 2){
				if(finalDist > squareWidth*0.6){
					resizeStatus = "shrinking";
					resizeButton.animate({"left": buttonX+squareWidth, "top": buttonY-squareWidth},100);
					$(".outerLeft, .outerBottom").css({"opacity": 0.2});
					$(".outerLeft, .outerBottom").parent(".pieceWrapper").css({"background-color": "rgba(48,48,48,0.3)"});
				}
				else{
					resizeButton.finish();
					resizeButton.css({"left": finalDist+cornerX-resizeWidth*0.25, "top": -finalDist+cornerY-resizeWidth*0.75});
					$(".outerLeft, .outerBottom").css({"opacity": 1});
					$(".outerLeft, .outerBottom").parent(".pieceWrapper").css({"background-color": "transparent"});
					resizeStatus = "none";
				}
			}
			else if(!(relX > cornerX && relY < cornerY) && gridLength < 6){
				if(finalDist > 30){
					resizeStatus = "enlarging";
					resizeButton.animate({"left": buttonX-60, "top": buttonY+60},100);
					$("#gridBorder").css({"width": "calc(107.5% + 50px)", "height": "calc(107.5% + 50px)", "left": "calc(-3.75% - 50px)"});
				}
				else{
					resizeButton.finish();
					resizeButton.animate({"left": -finalDist+cornerX-resizeWidth*0.25, "top": finalDist+cornerY-resizeWidth*0.75},100);
					$("#gridBorder").css({"width": "107.5%", "height": "107.5%", "left": "-3.75%", "top": "-3.75%"});
					resizeStatus = "none";
				}
			}
		}
	}
}
	

function releaseButton(e){
	if(resizeActive){
		if(resizeStatus == "enlarging" || resizeStatus == "shrinking"){
		    squareData.squares = [];
			$(".pieceHolder").each(function(){
				if($(this).css("background-image") != "none"){
				    var imgSrc = $(this).css("background-image").replace('url("', '').replace('")', '');
					var coords = $(this).attr("data-coords");
					var rotation = $(this).attr("data-rotation")
					var pieceID = $(this).attr("data-pieceID")
					var xCoord = parseInt(coords.split(",")[0]);
					var yCoord = parseInt(coords.split(",")[1]);
					
					if(resizeStatus == "enlarging"){
						if(resizeButton.attr("id") == "tl"){
							var coords = (xCoord+1) + "," + (yCoord+1);
						}
						else if(resizeButton.attr("id") == "tr"){
							var coords = xCoord + "," + (yCoord+1);
						}
						else if(resizeButton.attr("id") == "bl"){
							var coords = (xCoord+1) + "," + yCoord;
						}
						else if(resizeButton.attr("id") == "br"){
							var coords = xCoord + "," + yCoord;
						}
					}
					else if(resizeStatus == "shrinking"){
						if(resizeButton.attr("id") == "tl"){
							var coords = (xCoord-1) + "," + (yCoord-1);
						}
						else if(resizeButton.attr("id") == "tr"){
							var coords = xCoord + "," + (yCoord-1);
						}
						else if(resizeButton.attr("id") == "bl"){
							var coords = (xCoord-1) + "," + yCoord;
						}
						else if(resizeButton.attr("id") == "br"){
							var coords = xCoord + "," + yCoord;
						}
					}
					squareData.squares.push({"coords": coords, "imgSrc": imgSrc, "rotation": rotation, "pieceID": pieceID});
				}
			});
			if(resizeStatus == "enlarging"){
			    squareData.gridLength++;
			    gridLength++;
			}
			else if(resizeStatus == "shrinking"){
			    squareData.gridLength--;
			    gridLength--;
			}
			createGrid(squareData);
		}
		
		resizeButton.finish();
		$("#gridBorder").css({"width": "107.5%", "height": "107.5%", "left": "-3.75%", "top": "-3.75%"});
		$(".outerLeft, .outerBottom, .outerRight, .outerTop").css({"opacity": 1});
		resizeButton.css({"left": buttonX, "top": buttonY});
		resizeActive = false;
	}
}


