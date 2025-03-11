let selected_tags = [];

window.addEventListener("pageshow", function (event) {
    let perfEntries = performance.getEntriesByType("navigation");
    let historyTraversal = event.persisted || (perfEntries.length && perfEntries[0].type === "back_forward");

    if (historyTraversal) {
        window.location.reload();
    }
});

function goBack() {
    if (document.referrer) {
        window.history.back();
    }
    else {
        window.location.href = '/Home/';
    }
}
ActivityForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    const Button = document.getElementById("create-button");
    Button.disabled = true;
    Button.textContent = "Saving...";
    Button.style.backgroundColor = "#45a049";
    Button.style.cursor = "not-allowed";
    const formData = new FormData(this);
    formData.append("selected_tags", JSON.stringify(selected_tags));
    const response = await fetch("/CreateActivity/Create", {
        method: "POST",
        body: new URLSearchParams(formData),
        headers: { "Content-Type": "application/x-www-form-urlencoded" }
    });

    const result = await response.json();
    if (result.success) {
        window.location.href = `/ViewActivity/Index?activity_id=${result.activityId}`;
    }
    else {

        while (Message.firstChild) {
            Message.removeChild(Message.firstChild);
        }
        Message.style.color = "red";
        Message.textContent = result.message;
        Button.disabled = false;
        Button.textContent = "Create";
        Button.style.backgroundColor = "";
        Button.style.cursor = "pointer";
    }
});


const textarea = document.getElementById("autoResize");
textarea.addEventListener("input", () => {
    textarea.style.height = "auto";
    textarea.style.height = textarea.scrollHeight + "px";
});

const AddTag = (tag, tag_element) => {
    if (!selected_tags.includes(tag)) {
        tag_element.style.border = "black solid 2px";
        selected_tags.push(tag);  
    } else {
        removeTag(tag);
    }
    updateInUseGroup();
};

const tags_group = document.getElementById("tags_group");
tags.forEach(tag => {
    const tag_element = document.createElement("span");
    tag_element.className = "filter_tag";
    tag_element.textContent = tag.tagName;
    tag_element.addEventListener("click", () => AddTag(tag, tag_element));
    tags_group.appendChild(tag_element);
});

const updateInUseGroup = () => {
    const inuse_group = document.getElementById("inuse_group");
    inuse_group.innerHTML = ""; 

    selected_tags.forEach(tag => {
        const tag_element = document.createElement("span");
        tag_element.className = "inuse_tag";
        tag_element.textContent = tag.tagName;

        const close_button = document.createElement("span");
        close_button.textContent = "x";
        close_button.className = "remove_tag";
        close_button.onclick = (event) => {
            event.stopPropagation(); 
            removeTag(tag);
        };
        tag_element.prepend(close_button);
        inuse_group.appendChild(tag_element);
    });
};

const removeTag = (tag) => {
    selected_tags = selected_tags.filter(selectedTag => selectedTag !== tag);
    document.querySelectorAll(".filter_tag").forEach(tagElement => {
        if (tagElement.textContent.includes(tag.tagName)) {
            tagElement.style.border = "0";
        }
    });
    updateInUseGroup();
};

document.addEventListener("DOMContentLoaded", () => {
    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, input, textarea");

    elements.forEach(el => {
        if (el.tagName === "INPUT" || el.tagName === "TEXTAREA") {
            el.style.fontFamily = '"Noto Sans Thai", serif';
        } else if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });
});