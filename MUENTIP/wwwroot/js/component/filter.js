let selected_tags = [];

const FilterActivity = (selected_tags) => {
  const container = document.getElementById("container");
  if (selected_tags.length === 0) 
    {
      renderActivities(container, tags);
    }
  else
  {
    renderActivities(container, selected_tags);
  }
};

const ApplyFilter = (tag,tag_element) => {
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

const ShowFilterWindow =()=> {
  const filterWindow = document.getElementById("filter_window");
  filterWindow.style.display = "block";  // Show the window

}

const CloseFilterWindow =()=> {
  const filterWindow = document.getElementById("filter_window");
  filterWindow.style.display = "none";  // Hide the window
}


const tags_group =document.getElementById("tags_group");
tags.forEach(tag => {
  const tag_element = document.createElement("span");
  tag_element.className = "filter_tag";
  tag_element.textContent = tag.tagName;
  tag_element.addEventListener("click", () => ApplyFilter(tag,tag_element));
  tags_group.appendChild(tag_element);
});

document.getElementById("filter_button").addEventListener("click", ShowFilterWindow);
document.getElementById("close_button").addEventListener("click", CloseFilterWindow);





