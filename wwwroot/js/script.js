// carousel slider
var swiper = new Swiper(".mySwiper", {
    pagination: {
        el: ".swiper-pagination",
        dynamicBullets: true,
    },
});


     //product slider
     //product slider
var productSwiper = new Swiper(".productSwiper", {
    slidesPerView: 3,
    spaceBetween: 20,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        // when window width is >= 320px
        320: {
            slidesPerView: 1,
            spaceBetween: 10
        },
        // when window width is >= 480px
        480: {
            slidesPerView: 1,
            spaceBetween: 10
        },
        // when window width is >= 640px
        640: {
            slidesPerView: 2,
            spaceBetween: 10
        },
          // when window width is >= 992px
          992: {
            slidesPerView: 3,
            spaceBetween: 20
        },
    }
});


// /productslider


// review slider
var swiper = new Swiper('.review-container', {
    slidesPerView: 1,
    spaceBetween: 30,
    loop: true,
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },
    breakpoints: {
        640: {
            slidesPerView: 1,
            spaceBetween: 20,
        },
        768: {
            slidesPerView: 2,
            spaceBetween: 30,
        },
        1024: {
            slidesPerView: 3,
            spaceBetween: 30,
        },
    }
});