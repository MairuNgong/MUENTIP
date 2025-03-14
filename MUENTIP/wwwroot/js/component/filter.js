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

  if (!selected_tags.includes(tag)) {
    tag_element.style.border = "black solid 2px";
    selected_tags.push(tag); 
  } else {
 
    tag_element.style.border = "0";
    selected_tags = selected_tags.filter(selectedTag => selectedTag !== tag);
  }
  FilterActivity(selected_tags); 
};

const ShowFilterWindow = () => {
    const filterWindow = document.getElementById("filter_window");
  filterWindow.style.display = "block";  

}

const CloseFilterWindow = () => {
    const filterWindow = document.getElementById("filter_window");
  filterWindow.style.display = "none"; 
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





