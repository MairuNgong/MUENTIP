let selected_tags = [];
let lastsearch = "";
const NotFullButton = document.getElementById("Not_full_Only");

let notFullActive = false;

const FilterActivity = (selected_tags) => {
    const container = document.getElementById("container");
    console.log("from filter:");
    console.log(lastsearch);
    if (selected_tags.length === 0) {
        renderActivities(container, tags, Infinity, lastsearch, notFullActive);
    }
    else {
        renderActivities(container, selected_tags, Infinity, lastsearch, notFullActive);
    }
};

const ApplyFilter = (tag, tag_element) => {
    
    if (!selected_tags.includes(tag)) {
        tag_element.style.border = "black solid 2px";
        selected_tags.push(tag);  
    } else {
        
        tag_element.style.border = "0";
        selected_tags = selected_tags.filter(selectedTag => selectedTag !== tag);
    }
    FilterActivity(selected_tags); 
};

let showWindow = false;

const ShowFilterWindows = () => {
    if (showWindow === false) {
        showWindow = true;
        filter_button.style.border = "2px solid black";
        const filterWindow = document.getElementById("filter_window");
        filterWindow.style.display = "block";
    }
    else {
        CloseFilterWindows()
    }

}

const CloseFilterWindows = () => {
    showWindow = false;
    filter_button.style.border = "0px solid black";
    const filterWindow = document.getElementById("filter_window");
    filterWindow.style.display = "none";  
}


const tags_group = document.getElementById("tags_group");
tags.forEach(tag => {
    const tag_element = document.createElement("span");
    tag_element.className = "filter_tag";
    tag_element.textContent = tag.tagName;
    tag_element.addEventListener("click", () => ApplyFilter(tag, tag_element));
    tags_group.appendChild(tag_element);
});



NotFullButton.addEventListener("click", () => {
    notFullActive = !notFullActive; 

    console.log(notFullActive); 

    const container = document.getElementById("container");

    if (notFullActive) {

        NotFullButton.style.border = "2px solid black"; 
        if (selected_tags.length === 0) {
            renderActivities(container, tags, Infinity, lastsearch, notFullActive);
        }
        else {
            renderActivities(container, selected_tags, Infinity, lastsearch, notFullActive);
        }
    }
    else {
        
        NotFullButton.style.border = "none";
        if (selected_tags.length === 0) {
            renderActivities(container, tags, Infinity, lastsearch, notFullActive);
        }
        else {
            renderActivities(container, selected_tags, Infinity, lastsearch, notFullActive);
        }
    }
});



const search_apply = () => {
    const searchTerm = document.getElementById("search_bar").value.trim().toLowerCase();
    console.log("Searching for:", searchTerm);
    lastsearch = searchTerm;
   
    const filteredActivities = activities.filter(activity =>
        activity.title && activity.title.toLowerCase().includes(searchTerm)
    );

   
    console.log(`Filtered activities count: ${filteredActivities.length}`);

    
    const countDisplay = document.getElementById("filtered_count");
    if (countDisplay) {
        countDisplay.textContent = `Found ${filteredActivities.length} activities`;
    }
    filteredActivities.forEach(activity => {
        console.log(`Activity: ${activity.title}, Tag: ${activity.tag}`);
    });

    if (selected_tags.length === 0) {
        renderActivities(container, tags, Infinity, lastsearch, notFullActive);
    }
    else {
        renderActivities(container, selected_tags, Infinity, lastsearch, notFullActive);
    }
}



document.getElementById("search_button").addEventListener("click", search_apply);

document.getElementById("filter_button").addEventListener("click", ShowFilterWindows);
document.getElementById("close_button").addEventListener("click", CloseFilterWindows);
document.getElementById("search_bar").addEventListener("keypress", (event) => {
    if (event.key === "Enter") {
        search_apply();
    }
});

