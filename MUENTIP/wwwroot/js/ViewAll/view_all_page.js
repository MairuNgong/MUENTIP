document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6");

    elements.forEach(el => {
        if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});