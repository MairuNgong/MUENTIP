var Appliers = JSON.parse(JSON.stringify(selectModel)).appliers;
var ApplyMax = JSON.parse(JSON.stringify(selectModel)).applyMax;
var activity_id = JSON.parse(JSON.stringify(selectModel)).activity_id;

window.addEventListener("pageshow", function (event) {
    let perfEntries = performance.getEntriesByType("navigation");
    let historyTraversal = event.persisted || (perfEntries.length && perfEntries[0].type === "back_forward");

    if (historyTraversal) {
        window.location.reload();
    }
});

console.log(activity_id)
console.log(selectModel)
console.log(ApplyMax)

function renderApplier(container) {
    
    container.innerHTML = "";

    
    const totalCheckboxes = Appliers.length;
   
    Appliers.forEach(applicant => {
        const applicant_card = document.createElement("div");
        applicant_card.className = "applicant_card";

        const left_group = document.createElement("div");
        left_group.className = "left_group";

        const left_content_text = document.createElement("div");
        left_content_text.className = "left_content_text";

        const owner = document.createElement("div");
        const img_people = document.createElement("img");
        img_people.className = "icon";
        if (applicant.userImgLink != null) {
            img_people.src = applicant.userImgLink; 
        }
        else {
            img_people.src = "/img/default-profile.png"
        }

        img_people.addEventListener("click", function () {
            window.location.href = `/Profile/Index?id=${applicant.userId}`;
        });

        owner.className = "applicant_detail";
        owner.appendChild(img_people);

        const owner_text = document.createElement("span");
        owner_text.textContent = `${applicant.userName}`;
        owner_text.style.marginLeft = "10px"; 

        owner.appendChild(owner_text);

        left_content_text.appendChild(owner);

        left_group.appendChild(left_content_text);

        
        const checkboxWrapper = document.createElement("div");
        checkboxWrapper.className = "checkbox_wrapper";

        const checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.className = "checkbox";
        checkbox.id = `checkbox-${applicant.userName}`; 
        checkbox.style.marginLeft = "240px";


        checkboxWrapper.appendChild(checkbox);
        left_group.appendChild(checkboxWrapper);

        applicant_card.appendChild(left_group);

        container.appendChild(applicant_card);

    });

    document.getElementById("confirmBtn").addEventListener("click", async function () {
        
        const selectedCheckboxes = document.querySelectorAll(".checkbox:checked");
        const Button = document.getElementById("confirmBtn");
        Button.disabled = true;
        Button.textContent = "Sending...";
        Button.style.backgroundColor = "#45a049";
        Button.style.cursor = "not-allowed";
        
        const sentUserIds = new Set();
        console.log("Sending");
        
        for (const checkbox of selectedCheckboxes) {
            
            const userName = checkbox.id.replace("checkbox-", "");  
            const applicant = Appliers.find(app => app.userName === userName); 

            if (applicant) {
                const user_id = applicant.userId;

                
                if (sentUserIds.has(user_id)) {
                    
                    continue;
                }

                
                sentUserIds.add(user_id);

                const activity_id = JSON.parse(JSON.stringify(selectModel)).activity_id; 

                
                const formData = new FormData();
                formData.append("user_id", user_id);
                formData.append("activity_id", activity_id);

                
                try {
                    const response = await fetch("/Select/select_create", {
                        method: "POST",
                        body: new URLSearchParams(formData),
                        headers: { "Content-Type": "application/x-www-form-urlencoded" }
                    });

                    
                    const data = await response.json();
                    console.log(data);
                } catch (error) {
                    console.error('Error:', error);
                    Button.disabled = false;
                    Button.textContent = "Confirm";
                    Button.style.backgroundColor = "";
                    Button.style.cursor = "pointer";
                }
            }

        }
        console.log("ApplyOn created successfully!");

        const overlay = document.getElementById("overlay");
        const popup = document.getElementById("notify-popup");
        const progressBar = document.getElementById("progress-bar");

        overlay.style.display = "block";
        popup.style.display = "flex";
        popup.classList.remove("fade-out");
        overlay.classList.remove("fade-out");
        progressBar.style.width = "0%";

        setTimeout(() => {
            progressBar.style.width = "100%";
        }, 50);

        setTimeout(() => {
            popup.classList.add("fade-out");
            overlay.classList.add("fade-out");

            setTimeout(() => {
                popup.style.display = "none";
                overlay.style.display = "none";
                window.location.reload();
            }, 1000);
        }, 1000);
        setTimeout(function () {
            window.location.href = '/Home';
        }, 500);  
    });



    
    document.querySelectorAll(".checkbox").forEach(checkbox => {
        checkbox.addEventListener("change", function () {
            
            const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;

            
            document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
        });
    });

    
    const initialCheckedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
    document.getElementById("selectedCount").textContent = `${initialCheckedCheckboxes} / ${totalCheckboxes}`;
}


document.getElementById("selectAllBtn").addEventListener("click", function () {
   
    const checkboxes = document.querySelectorAll(".checkbox");

    
    checkboxes.forEach(checkbox => {
        checkbox.checked = true;  
    });

    
    const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
    const totalCheckboxes = Appliers.length;
    document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
});

document.getElementById("picture").addEventListener("click", function () {
    window.location.href = `/Profile/Index?id=${selectModel.ownerId}`;
})



const container = document.getElementById("container");
renderApplier(container);