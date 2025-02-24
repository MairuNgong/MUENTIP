document.addEventListener('DOMContentLoaded', function () {
    
    var editProfileBtn = document.querySelector('.edit-profile-btn');
    const createTab = document.getElementById('createTab');
    const participateTab = document.getElementById('participateTab');
    const createActivities = document.getElementById('createActivities');
    const participateActivities = document.getElementById('participateActivities');
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
    
});

