 document.addEventListener("DOMContentLoaded", () => {
    const homeTab = document.getElementById("homeTab");
    const searchTab = document.getElementById("searchTab");
    const loginTab = document.getElementById("loginTab");

    const loginPopup = document.getElementById("loginPopup");
    const closePopup = document.getElementById("closePopup");

    const registerPopup = document.getElementById("registerPopup");
     const registerLink = document.getElementById("registerLink");
     const loginLink = document.getElementById("loginLink");
    const closeregisterPopup = document.getElementById("closeregisterPopup");

     const loginForm = document.getElementById("loginForm");
     const LoginMessage = document.getElementById("LoginMessage");
     const registerForm = document.getElementById("registerForm");
     const RegisterMessage = document.getElementById("RegisterMessage");
     

            homeTab.addEventListener("click", () => {
                window.location.href = "/Home/";
            });

            searchTab.addEventListener("click", () => {
                window.location.href = "/SearchPage/";
            });


            loginTab.addEventListener("click", () => {
                 loginPopup.style.display = "flex";
            });

            closePopup.addEventListener("click", () => {
                loginPopup.style.display = "none";
            });

            registerLink.addEventListener("click", (event) => {
                event.preventDefault();
                loginPopup.style.display = "none";
                registerPopup.style.display = "flex";
            });
            closeregisterPopup.addEventListener("click", () => {
                registerPopup.style.display = "none";
            });
            loginLink.addEventListener("click", (event) => {
                 event.preventDefault();
                 loginPopup.style.display = "flex";
                 registerPopup.style.display = "none";
             });


            loginForm.addEventListener("submit", async function (event) {
                event.preventDefault();

                const formData = new FormData(this);
                console.log(formData);
                const response = await fetch("/Account/Login", {
                    method: "POST",
                    body: new URLSearchParams(formData),
                    headers: {"Content-Type": "application/x-www-form-urlencoded" }
                });

                const result = await response.json();
                if (result.success) {
                    window.location.href = `/Profile/Index/${result.message}`;
                }
                else
                {
                    LoginMessage.textContent = result.message;
                 }
             });

            registerForm.addEventListener("submit", async function (event){
                    event.preventDefault();

                const formData = new FormData(this);
                const response = await fetch("/Account/Register", {
                    method: "POST",
                    body: new URLSearchParams(formData),
                    headers: {"Content-Type": "application/x-www-form-urlencoded" }
                 });

                const result = await response.json();
                if (result.success) {
                    RegisterMessage.style.color = "green";
                    RegisterMessage.textContent = "Registed. You can login now.";
                }
                else {

                    while (RegisterMessage.firstChild) {
                        RegisterMessage.removeChild(RegisterMessage.firstChild);
                    }
                    RegisterMessage.style.color = "red";
                    RegisterMessage.textContent = result.message;
                }
                
            });

            const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, input");
        
            elements.forEach(el => {
                if (el.tagName === "INPUT") {
                    el.style.fontFamily = '"Noto Sans Thai", serif';
                } else if (/[ก-๙]/.test(el.textContent)) { 
                    el.style.fontFamily = '"Noto Sans Thai", serif';
                }
            });     
 });

