$("header nav button").on("click", function(){
	$("header nav #mainMenu").css({"right": "0px"});
});

function closeMenu(){
	$("header nav #mainMenu").css({"right": "-180px"});
}

$(document).on("click", function(e){
	if($("header nav #mainMenu").css("right") == "0px" && document.getElementsByTagName("header")[0].contains(e.target) == false){
		closeMenu();
	}
});
$("#closeMenu").on("click", closeMenu);
$(document).keyup(function(e){
	if(e.keyCode === 27){
		closeMenu();
	}
});