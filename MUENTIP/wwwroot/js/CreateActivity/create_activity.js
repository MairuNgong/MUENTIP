function goBack() {
    if (document.referrer) {
        window.history.back();
    }
    else {
        window.location.href = '/Home/';
    }
}
const textarea = document.getElementById("autoResize");

textarea.addEventListener("input", () => {
    textarea.style.height = "auto";
    textarea.style.height = textarea.scrollHeight + "px";
});