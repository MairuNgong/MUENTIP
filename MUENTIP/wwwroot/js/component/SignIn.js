document.addEventListener("DOMContentLoaded", () => {
    const homeTab = document.getElementById("homeTab");
    const searchTab = document.getElementById("searchTab");
    const creatActivityTab = document.getElementById("creatActivityTab");
    const profileTab = document.getElementById("profileTab");
    const logoutTab = document.getElementById("logoutTab");

    homeTab.addEventListener("click", () => {
        window.location.href = "/Home/";
    });

    searchTab.addEventListener("click", () => {
        window.location.href = "/SearchPage/";
    });
    creatActivityTab.addEventListener("click", () => {
        window.location.href = "/CreateActivity/";
    });
    profileTab.addEventListener("click", () => {
        window.location.href = `/Profile/Index/${userId}`;
    });

    logoutTab.addEventListener("click", () => {
        window.location.href = "/Account/Logout/";
    });
});