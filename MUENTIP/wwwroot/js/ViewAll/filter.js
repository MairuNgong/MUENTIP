if (tag == "Realtime") {
    let selected_tags = [];
  
    const FilterActivity = (selected_tags) => {
        const container = document.getElementById("act-div");
        if (selected_tags.length === 0) {
            renderActivities(container, filter_tags);
        } else {
            renderActivities(container, selected_tags);
        }
    };

    const CloseFilterWindow = () => {
        const filterWindow = document.getElementById("filter_window");
        filterWindow.style.display = "none"; 
    }
  
    const ApplyFilter = (tag, tag_element) => {
        if (!selected_tags.includes(tag)) {
            tag_element.style.outline = "black solid 2px";
            selected_tags.push(tag);
        } else {
            tag_element.style.outline = "0";
            selected_tags = selected_tags.filter(selectedTag => selectedTag !== tag);
        }
        FilterActivity(selected_tags);
    };
  
    const ToggleFilterWindow = () => {
        const filterWindow = document.getElementById("filter_window");

        if (filterWindow.style.display === "none" || filterWindow.style.display === "") {
            filterWindow.style.display = "block"; 
        } else {
            filterWindow.style.display = "none"; 
        }
    };
  
    const tags_group = document.getElementById("tags_group");
    filter_tags.forEach(tag => {
        const tag_element = document.createElement("span");
        tag_element.className = "filter_tag";
        tag_element.textContent = tag.tagName;
        tag_element.addEventListener("click", () => ApplyFilter(tag, tag_element));
        tags_group.appendChild(tag_element);
    });

    document.getElementById("filter_button").addEventListener("click", ToggleFilterWindow);
    document.getElementById("close_button").addEventListener("click", CloseFilterWindow);
}
  