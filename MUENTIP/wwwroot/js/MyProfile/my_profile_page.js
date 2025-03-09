document.addEventListener('DOMContentLoaded', function () {
    
    var editProfileBtn = document.querySelector('.edit-profile-btn');
    const createTab = document.getElementById('createTab');
    const participateTab = document.getElementById('participateTab');
    const createActivities = document.getElementById('createActivities');
    const participateActivities = document.getElementById('participateActivities');
    if (editProfileBtn)
        editProfileBtn.addEventListener('click', function () {
            window.location.href = "/EditProfile"; 
        });

    if (createTab && participateTab && createActivities && participateActivities) {
        createTab.classList.add('active');  
        createActivities.classList.add('active');  
        
        createTab.addEventListener('click', function () {
            createTab.classList.add('active');
            participateTab.classList.remove('active');
            createActivities.classList.add('active');
            participateActivities.classList.remove('active');
            
        });

        participateTab.addEventListener('click', function () {
            participateTab.classList.add('active');
            createTab.classList.remove('active');
            participateActivities.classList.add('active');
            createActivities.classList.remove('active');
            
        });
    }

    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, input, textarea");

    elements.forEach(el => {
        if (el.tagName === "INPUT" || el.tagName === "TEXTAREA") {
            el.style.fontFamily = '"Noto Sans Thai", serif';
        } else if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });

});

