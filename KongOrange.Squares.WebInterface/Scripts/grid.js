// INITIALIZE
var device; 
try{  
	document.createEvent("TouchEvent");  
	device = "touch";  
}catch(e){  
	device = "pc"; 
}
if(device == "touch"){
	$(document).on("touchstart", ".pieces", selectPiece);
	$(document).on("touchstart", ".pieceHolder", selectPiece);
	$(document).on("touchstart", ".rotationButton", rotatePiece);
	$(document).on("touchend", ".pieces", releasePiece);
	$(document).on("touchend", ".pieceHolder", releasePiece);
	$("body").on("touchmove", movePiece);
}
else if(device == "pc"){
	$(document).on("mousedown", ".pieces", selectPiece);
	$(document).on("mousedown", ".pieceHolder", selectPiece);
	$(document).on("mousedown", ".rotationButton", rotatePiece);
	$(document).on("mouseup", ".pieces", releasePiece);
	$(document).on("mouseup", ".pieceHolder", releasePiece);
	$("body").mousemove(movePiece);
}

var gridWidth = $("#grid").width(); // Everything depends on this aka setting
var mouseDown = false;
var active = false;
var copiedPiece;
var pieceHolderCoords; //coords - used as the identifier
var gridLength = 3; // Everything depends on this aka setting
var squareWidth = gridWidth/gridLength;
var squareData;

$(window).on("resize", function(){
	gridWidth = $("#grid").width();
	squareWidth = gridWidth/gridLength;
	$(".pieceWrapper, .pieceHolder").width(squareWidth+"px").height(squareWidth+"px");
	$(".rotationButton").css({"margin-left": (gridWidth/gridLength-20)+"px", "margin-top": -(gridWidth/gridLength-5)+"px"});
});

squareData = {
    "setID": 3,
    "gridLength": gridLength,
	"squares":[
	{
		"coords": "2,2",
		"imgSrc": "/Content/Images/tool/14.jpg",
		"rotation": 0,
		"pieceID": 12
	},{
		"coords":"3,2",
		"imgSrc": "/Content/Images/tool/17.jpg",
		"rotation": -180,
		"pieceID": 32
	},{
		"coords": "2,3",
		"imgSrc": "/Content/Images/tool/28.jpg",
		"rotation": 0,
		"pieceID": 12
	}]
};

if(localStorage.squareData) {
    squareData = JSON.parse(localStorage.squareData);
}

if (squareData) {
    gridLength = squareData.gridLength;
    createGrid(squareData);
}
else{
	createGrid();
}
	
function createGrid(data){
    $("#grid").empty();
    $("#grid").css({ "background-image": "url(/Content/images/grid" + gridLength + ".png)" });
    squareWidth = gridWidth / gridLength;
	var x = 1;
	var y = 1;
	for(var i=1; i<=(gridLength*gridLength); i++){
		var className = "";
		if(x == 1){
			className += " outerLeft";
		}
		if(y == 1){
			className += " outerTop";
		}
		if(x == gridLength){
			className += " outerRight";
		}
		if(y == gridLength){
			className += " outerBottom";
		}
		$("#grid").append("<div class='pieceWrapper'><div class='pieceHolder"+className+"' data-rotation='0' data-coords='"+x+","+y+"' id='pieceHolder_"+i+"'></div><div id='asd' class='rotationButton'></div></div>");
		x++;
		if(x > gridLength){
			x = 1;
			y++;
		}
	}
	if(data){
		console.log(data);
		for(var i=0; i<data.squares.length; i++){
			var squareCoords = data.squares[i].coords;
			var squareImg = data.squares[i].imgSrc;
			var rotationDegree = data.squares[i].rotation;
			var pieceID = data.squares[i].pieceID; 
			$("[data-coords='"+squareCoords+"']").css({"background-image": "url('"+squareImg+"')", "transform": "rotate("+rotationDegree+"deg)"}).attr({"data-rotation": rotationDegree, "data-pieceID": pieceID});
		}
		squareData.gridLength = gridLength;
		saveWork();
	}
	
	$(".pieceWrapper, .pieceHolder").width(squareWidth+"px").height(squareWidth+"px");
	$(".rotationButton").css({"margin-left": (gridWidth/gridLength-20)+"px", "margin-top": -(gridWidth/gridLength-5)+"px"});
}
function rotatePiece(e){
	var rotation = $(this).prev(".pieceHolder").attr("data-rotation");
	rotation -= 90;
	var rotateThis = $(this).prev(".pieceHolder");
	rotateThis.attr("data-rotation", rotation);
	rotateThis.each(function(){this.deg = rotation+90;}).animate({deg: rotation}, {
		duration: 300,
		step: function(now){
			rotateThis.css({"transform": "rotate("+now+"deg)"});
		}
	});
	saveWork();
}

function selectPiece(e){
	e.preventDefault();
	$("#grid .pieces").finish();
	$(".pieceHolder").finish();
	if(!active){
		pieceHolderCoords = 0;
		active = true;
		mouseDown = true;
		copiedPiece = $(this).clone().appendTo("#grid").removeAttr("id");
		var triggerClass = $(this).attr("class");
		if(triggerClass.indexOf("pieceHolder") >= 0){
			$(this).css({"background-image": "none", "transform": "rotate(0deg)"});
			pieceHolderCoords =  $(this).attr("data-coords");
			copiedPiece.css({"background-color": "white"});
		}
		else{
			copiedPiece.css({"transform": "rotate(0deg)"});
		}
	   
		parentOffset = $("#grid").offset(); //grid offset from the top left corner of the document
		if(device == "touch"){
			var relX = e.originalEvent.changedTouches[0].pageX - parentOffset.left;  //X coords of touchdown click relative to grid hence the offset substraction
			var relY = e.originalEvent.changedTouches[0].pageY - parentOffset.top;  //Y coords of touchdown relative to grid
		}else if(device == "pc"){
			var relX = e.pageX - parentOffset.left; //X coords of mouse click relative to grid
			var relY = e.pageY - parentOffset.top; //Y coords of mouse click relative to grid
		}
		
		copiedPiece.animate({width: squareWidth+"px", height: squareWidth+"px"}, 300, "linear");
		copiedPiece.css({"left": relX-(squareWidth/2), "top": relY-(squareWidth/2), "position": "absolute"});
	}
}
	
function movePiece(e){
	if(mouseDown){
		if(device == "touch"){
			var relX = e.originalEvent.changedTouches[0].pageX - parentOffset.left;
			var relY = e.originalEvent.changedTouches[0].pageY - parentOffset.top;
		}else if(device == "pc"){
			var relX = e.pageX - parentOffset.left;
			var relY = e.pageY - parentOffset.top;
		}
		copiedPiece.css({"left": relX-(squareWidth/2), "top": relY-(squareWidth/2)});
		
		if(relX <= gridWidth && relX >= 0 && relY <= gridWidth && relY >= 0){ //if inside of the grid
			var xCoord = Math.ceil(relX/(squareWidth));
			var yCoord = Math.ceil(relY/(squareWidth));
			pieceHolderCoords = xCoord+","+yCoord;
			$(".pieceHolder").css({"opacity": 1});
			$(".pieceHolder").parent(".pieceWrapper").css({"background-color": "transparent"});
			
			$("[data-coords='"+pieceHolderCoords+"']").css({"opacity": 0.8});
			$("[data-coords='"+pieceHolderCoords+"']").parent(".pieceWrapper").css({"background-color": "rgba(48,48,48,0.3)"});
		}
		else{
			$(".pieceHolder").css({"opacity": 1});
			$(".pieceHolder").parent(".pieceWrapper").css({"background-color": "transparent"});
			pieceHolderCoords = 0;
		}
	}
}
	

function releasePiece(e){
	if(active){
		mouseDown = false;
		if(pieceHolderCoords != 0){ //if released inside grid
			var targetLeft = $("[data-coords='"+pieceHolderCoords+"']").position().left; //the targeted left location of pieceholder
			var targetTop = $("[data-coords='"+pieceHolderCoords+"']").position().top;
			copiedPiece.animate({left: targetLeft, top: targetTop}, 200, "linear", function(){
				var imageSrc = copiedPiece.css("background-image");
				var rotation = copiedPiece.attr("data-rotation");
				var pieceID = copiedPiece.attr("data-pieceID");
				if(!rotation){rotation = 0;}
				$("[data-coords='"+pieceHolderCoords+"']").css({"background-image": imageSrc, "transform": "rotate("+rotation+"deg)"});
				$("[data-coords='"+pieceHolderCoords+"']").attr({"data-rotation": rotation, "data-pieceID": pieceID});
				copiedPiece.remove();
				saveWork();
				active = false;
			});
		}
		else{
			copiedPiece.remove();
			active = false;
		}
		$(".pieceHolder").css({"opacity": 1});
		$(".pieceHolder").parent(".pieceWrapper").css({"background-color": "transparent"});
	}
}


//LOCAL STORAGE
function saveWork() {
    squareData.squares = [];
    $(".pieceHolder").each(function () {
        if ($(this).css("background-image") != "none") {
            var imgSrc = $(this).css("background-image").replace('url("', '').replace('")', '');
            var coords = $(this).attr("data-coords");
            var rotation = $(this).attr("data-rotation")
            var pieceID = $(this).attr("data-pieceID")
            squareData.squares.push({ "coords": coords, "imgSrc": imgSrc, "rotation": rotation, "pieceID": pieceID });
        }
    });
    localStorage.squareData = JSON.stringify(squareData);
}

