let selected_tags = [];

const FilterActivity = (selected_tags) => {
    const container = document.getElementById("container");
    if (selected_tags.length === 0) {
        renderActivities(container, tags);
    }
    else {
        renderActivities(container, selected_tags);
    }
};

const ApplyFilter = (tag, tag_element) => {
    // Check if the tag is already in the selected_tags array
    if (!selected_tags.includes(tag)) {
        tag_element.style.border = "black solid 2px";
        selected_tags.push(tag);  // Add the tag to the array if not already selected
    } else {
        // If the tag is already selected, you can optionally remove it (toggle effect)
        tag_element.style.border = "0";
        selected_tags = selected_tags.filter(selectedTag => selectedTag !== tag);
    }
    FilterActivity(selected_tags);  // Apply the filter with the accumulated tags
};

let showWindow = false;

const ShowFilterWindows = () => {
  if (showWindow === false){
    showWindow = true;
    filter_button.style.border = "2px solid black";
    const filterWindow = document.getElementById("filter_window");
    filterWindow.style.display = "block";
  }
  else
  {
    CloseFilterWindows()
  }
  
}

const CloseFilterWindows = () => {
  showWindow = false;
    filter_button.style.border = "0px solid black";
    const filterWindow = document.getElementById("filter_window");
    filterWindow.style.display = "none";  // Hide the window
}


const tags_group = document.getElementById("tags_group");
tags.forEach(tag => {
    const tag_element = document.createElement("span");
    tag_element.className = "filter_tag";
    tag_element.textContent = tag.tagName;
    tag_element.addEventListener("click", () => ApplyFilter(tag, tag_element));
    tags_group.appendChild(tag_element);
});

const NotFullButton = document.getElementById("Not_full_Only");

let notFullActive = false; // ???????????? false

NotFullButton.addEventListener("click", () => {
    notFullActive = !notFullActive; // ??????? true <-> false

    console.log(notFullActive); // ?????? true ???? false ????????????

    const container = document.getElementById("container");

    if (notFullActive) {
        // ?????????????????????????????
        NotFullButton.style.border = "2px solid black";  // ????????????
        const filteredActivities = activities.filter(activity => activity.applyCount < activity.applyMax);
        renderActivities(container, tags, filteredActivities.length, "", true);
    } else {
        // ??????????????????
        NotFullButton.style.border = "none";
        renderActivities(container, tags);
    }
});



const search_apply = () => {
    const searchTerm = document.getElementById("search_bar").value.trim().toLowerCase();
    console.log("Searching for:", searchTerm);

    // Filter activities based on the search term matching the title and having a non-empty title
    const filteredActivities = activities.filter(activity =>
        activity.title && activity.title.toLowerCase().includes(searchTerm)
    );

    // Log the number of filtered activities
    console.log(`Filtered activities count: ${filteredActivities.length}`);

    // Optionally display the count on the page
    const countDisplay = document.getElementById("filtered_count"); 
    if (countDisplay) {
        countDisplay.textContent = `Found ${filteredActivities.length} activities`;
    }
    filteredActivities.forEach(activity => {
        console.log(`Activity: ${activity.title}, Tag: ${activity.tag}`);
    });

    // Render only the filtered activities based on title match
    renderActivities(document.getElementById("container"), tags, filteredActivities.length, searchTerm);
}



document.getElementById("search_button").addEventListener("click", search_apply);

document.getElementById("filter_button").addEventListener("click", ShowFilterWindows);
document.getElementById("close_button").addEventListener("click", CloseFilterWindows);
document.getElementById("search_bar").addEventListener("keypress", (event) => {
    if (event.key === "Enter") {
        search_apply();
    }
});