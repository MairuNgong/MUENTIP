function goBack() {
    if (document.referrer) {
        window.history.back();
    }
    else {
        window.location.href = '/Home/';
    }
}

window.addEventListener("pageshow", function (event) {
    let perfEntries = performance.getEntriesByType("navigation");
    let historyTraversal = event.persisted || (perfEntries.length && perfEntries[0].type === "back_forward");

    if (historyTraversal) {
        window.location.reload();
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6");

    elements.forEach(el => {
        if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});