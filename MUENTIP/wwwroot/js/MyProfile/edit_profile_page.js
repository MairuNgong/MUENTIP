const tags = JSON.parse(JSON.stringify(activityModel)).interestedTags; 
const availableTagsObj = JSON.parse(JSON.stringify(activityModel)).availableTags;
const availableTags = availableTagsObj.map(tag => tag.tagName); 
console.log("availableTags", availableTags);
console.log(tags);
document.getElementById("addTagBtn").addEventListener("click", function() {
    openTagModal(); 
});

function openTagModal() {
    const modal = document.getElementById("tagModal");
    const availableTagsList = document.getElementById("availableTags");
    availableTagsList.innerHTML = "";  
    availableTags.forEach(function(tag) {
        const li = document.createElement("li");
        li.textContent = tag;
        li.addEventListener("click", function() {
            addTagToProfile(tag);  
        });
        availableTagsList.appendChild(li);
    });

    modal.style.display = "block";  
}

function closeTagModal() {
    const modal = document.getElementById("tagModal");
    modal.style.display = "none"; 
}


function addTagToProfile(tag) {
    if (tags.includes(tag)) {
        alert("Tag already added");
        return;
    }

    const tagNav = document.getElementById("tag-nav");
    const li = document.createElement("li");
    li.classList.add("hot-tag-ele");
    li.textContent = tag;

    const removeButton = document.createElement("button");
    removeButton.type = "button";
    removeButton.classList.add("remove-tag-btn");
    removeButton.textContent = "X";
    removeButton.addEventListener("click", function() {
        removeTagFromProfile(tag);  
    });

    li.appendChild(removeButton);
    tagNav.appendChild(li);

    tags.push(tag);
}

function removeTagFromProfile(tag) {
    const tagNav = document.getElementById("tag-nav");
    const tagListItems = tagNav.getElementsByClassName("hot-tag-ele");

    for (let i = 0; i < tagListItems.length; i++) {
        const item = tagListItems[i];
        if (item.textContent.includes(tag)) {
            item.remove();
            break;
        }
    }

    const tagIndex = tags.indexOf(tag);
    if (tagIndex > -1) {
        tags.splice(tagIndex, 1);
    }
}

document.querySelector("form").addEventListener("submit", function(event) {
    const existingTagsInputs = document.querySelectorAll("input[name='InterestedTags']");
    existingTagsInputs.forEach(input => input.remove());

    tags.forEach(tag => {
        const tagsInput = document.createElement("input");
        tagsInput.type = "hidden";
        tagsInput.name = "InterestedTags";  
        tagsInput.value = tag;  
        this.append(tagsInput); 
    });


    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, input, textarea");

    elements.forEach(el => {
        if (el.tagName === "INPUT" || el.tagName === "TEXTAREA") {
            el.style.fontFamily = '"Noto Sans Thai", serif';
        } else if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });

});

async function uploadImage() {
    const fileInput = document.getElementById("fileInput");
    const fileInputButton = document.getElementById("fileInputButton");

    if (!fileInput.files.length) return;

    
    fileInputButton.disabled = true;
    fileInputButton.textContent = "Uploading...";
    fileInputButton.style.backgroundColor = "#45a049";
    fileInputButton.style.cursor = "not-allowed";

    const file = fileInput.files[0];
    let formData = new FormData();
    formData.append("file", file);

    try {
        const response = await fetch("/EditProfile/Image", {
            method: "POST",
            body: formData
        });

        const data = await response.json();
        if (response.ok) {
            document.getElementById("profile-img").src = data.imageUrl;
            document.getElementById("profileImageLink").value = data.imageUrl;
        } else {
            alert("Upload failed: " + data.message);
        }
    } catch (error) {
        alert("An error occurred: " + error.message);
    } finally {
        
        fileInputButton.disabled = false;
        fileInputButton.style.backgroundColor = ""; 
        fileInputButton.style.cursor = "pointer";
        fileInputButton.textContent = "Upload";
    }
}


document.getElementById('EditForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Prevent form submission

    const Button = document.getElementById("submitButton");
    Button.disabled = true;
    Button.textContent = "Saving...";
    Button.style.backgroundColor = "#45a049";
    Button.style.cursor = "not-allowed";
    // Collect form data
    let formData = new FormData();
    formData.append('UserName', document.getElementById('Name').value);
    formData.append('Email', document.getElementById('Email').value);
    formData.append('Info', document.getElementById('Detail').value);
    formData.append('BirthDate', document.getElementById('Birthday').value);
    formData.append('Gender', document.getElementById('Gender').value);
    formData.append('Education', document.getElementById('Education').value);
    formData.append('Address', document.getElementById('Address').value);
    formData.append('showCreate', document.getElementById('showCreate').checked);
    formData.append('showParticipate', document.getElementById('showParticipate').checked);
    // Handle tags
   
    formData.append('InterestedTags', JSON.stringify(tags));
    console.log(JSON.stringify(tags))
    // Handle profile image
    let profileImageLink = document.getElementById('profileImageLink').value;
    if (profileImageLink) {
        formData.append('ProfileImageLink', profileImageLink);
    }

    // Send the request
    const response = await fetch('/EditProfile/Edit', {
        method: 'POST',
        body: formData
    })
    const result = await response.json();
    if (result.success) {
        window.location.href = `/Profile/Index/${result.message}`;
    }
    else {
        Message = document.getElementById("error");
        while (Message.firstChild) {
            Message.removeChild(Message.firstChild);
        }
        Message.style.color = "red";
        Message.textContent = result.message;
        Button.disabled = false;
        Button.style.backgroundColor = "";
        Button.style.cursor = "pointer";
        Button.textContent = "Save Changes";
    }
});