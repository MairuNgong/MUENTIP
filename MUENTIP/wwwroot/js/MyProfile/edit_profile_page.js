const tags = JSON.parse(JSON.stringify(activityModel)).interestedTags; 
const availableTagsObj = JSON.parse(JSON.stringify(activityModel)).availableTags;
const availableTags = availableTagsObj.map(tag => tag.tagName); 
console.log("availableTags", availableTags);

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

    closeTagModal(); 
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
});
