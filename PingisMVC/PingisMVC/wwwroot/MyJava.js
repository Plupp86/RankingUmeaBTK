function loadStats(playerId) {
	var divToEdit = document.getElementById(playerId);
	$.ajax({
		url: "/Home/PlayerStats",
		type: "GET",
		data: { "id": playerId },
		success: function (result) {
			divToEdit.innerHTML = result;
			//$($"#stats").html(result);
		}
	});
}

function toggleStats(playerId) {
	var statsToToggle = document.getElementById("tb"+playerId);
	//var divToFill = document.getElementById("div"+playerId)
	$.ajax({
		url: "/Home/PlayerStats",
		type: "GET",
		data: { "id": playerId },
		success: function (result) {
			statsToToggle.innerHTML = result;
			//$($"#stats").html(result);
		}
	});

	$(statsToToggle).toggle();

	
}