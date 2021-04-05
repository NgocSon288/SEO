const activeTab = (event, tabId, tabGroup) => {
	let tabs;
	let tabBtns;
	let scope;
	if (tabGroup) {
		scope = document.querySelector(`#${tabGroup}`);
		tabs = scope.querySelectorAll(`.tab-item.${tabGroup}`);
		tabBtns = scope.querySelectorAll(`.tab-btn.${tabGroup}`);
	} else {
		tabs = document.querySelectorAll(`.tab-item`);
		tabBtns = document.querySelectorAll(`.tab-btn`);
	}

	tabs.forEach((tab) => {
		tab.classList.remove("active");
	});
	tabBtns.forEach((tab) => {
		tab.classList.remove("active");
	});

	event.currentTarget.className += " active";
	document.getElementById(tabId).classList.add("active");
};

(() => {
	const btnToTop = document.getElementById("scrollToTop");
	window.onscroll = () => {
		if (window.pageYOffset > screen.height) {
			btnToTop.classList.remove("hidden");
		} else {
			btnToTop.classList.add("hidden");
		}
	};
	btnToTop.addEventListener("click", () => {
		window.scrollTo({
			top: 0,
			behavior: "smooth",
		});
	});
})();

const swiper = new Swiper(".recommend-container", {
	direction: "horizontal",
	loop: true,
	scrollbar: false,
	speed: 800,
	slidesPerView: "auto",
	spaceBetween: 18,
	centeredSlides: true,

	slideClass: "recommend-slide",

	pagination: {
		el: ".swiper-pagination",
		clickable: true,
	},

	navigation: {
		nextEl: ".swiper-button-next",
		prevEl: ".swiper-button-prev",
	},
});

document.getElementById("hamburger").addEventListener("click", () => {
	document.getElementById("nav__menu").classList.add("active");
});

document.getElementById("close-menu").addEventListener("click", () => {
	document.getElementById("nav__menu").classList.remove("active");
});
