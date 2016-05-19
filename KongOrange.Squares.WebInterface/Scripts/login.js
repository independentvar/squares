$(document).ready(function(){
	$("#loginMenu").on("click", function(){
		$("#loginContainer").show();
		$("#login").show();
	});
	
	$("#createButton").on("click", function(){
		$("#login").hide();
		$("#createAccount").show();
	});
	
	$("#loginContainer2").on("click", function(e){
		if(e.target == this){
			$(".loginDiv").hide();
			$("#loginContainer").hide();
		}
	});
	
	$(document).keyup(function(e){
		if(e.keyCode === 27){
			$(".loginDiv").hide();
			$("#loginContainer").hide();
		}
	});
});