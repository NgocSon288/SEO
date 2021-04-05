$.getJSON("assets/json/tinh_tp.json", function (data) {
	var items = "";
	$.each(data, function (index, item) {
		items += `<a class='location-item' href='#'>${item.name}</a>`;
	});

	$("#ds-diem-den-viet-nam .data").html(items);
});

document.getElementById("scroll-to-location").addEventListener("click", () => {
	window.scrollTo({
		behavior: "smooth",
		top: document.getElementById("ds-diem-den").offsetTop,
	});
});
