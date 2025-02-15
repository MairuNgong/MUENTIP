var Appliers = JSON.parse(JSON.stringify(selectModel)).appliers; 
var ApplyMax = JSON.parse(JSON.stringify(selectModel)).applyMax;

function renderApplier(container) {
  // Remove all child elements from the container to overwrite it
  container.innerHTML = "";

  // Track the total number of activities (checkboxes)
  const totalCheckboxes = Appliers.length;

  // Iterate over the activities (we are only displaying owner info now)
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
    img_people.src = applicant.userImgLink; // Display the applicant's profile picture
    owner.className = "applicant_detail";
    owner.appendChild(img_people);

    const owner_text = document.createElement("span");
    owner_text.textContent = `${applicant.userName} - ${applicant.email}`; // Display username and email
    owner.appendChild(owner_text);

    left_content_text.appendChild(owner);

    left_group.appendChild(left_content_text);

    // Add checkbox
    const checkboxWrapper = document.createElement("div");
    checkboxWrapper.className = "checkbox_wrapper";

    const checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.className = "checkbox";
    checkbox.id = `checkbox-${applicant.userName}`;

    checkboxWrapper.appendChild(checkbox);
    left_group.appendChild(checkboxWrapper);

    applicant_card.appendChild(left_group);

    container.appendChild(applicant_card);
});

  // Add event listeners to checkboxes after rendering
  document.querySelectorAll(".checkbox").forEach(checkbox => {
    checkbox.addEventListener("change", function() {
      // นับจำนวน checkbox ที่ถูกเลือก
      const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
    
      // อัพเดตจำนวนที่แสดงใน <span id="selectedCount">
      document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
    });
  });

  // Initial update of selected count
  const initialCheckedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
  document.getElementById("selectedCount").textContent = `${initialCheckedCheckboxes} / ${totalCheckboxes}`;
}

// Add selectAll functionality
document.getElementById("selectAllBtn").addEventListener("click", function() {
  // Get all checkboxes within the container
  const checkboxes = document.querySelectorAll(".checkbox");

  // Loop through all checkboxes and set the "checked" property to true
  checkboxes.forEach(checkbox => {
    checkbox.checked = true;  // Check each checkbox
  });

  // Update the selected count
  const checkedCheckboxes = document.querySelectorAll(".checkbox:checked").length;
  const totalCheckboxes = ApplyMax;
  document.getElementById("selectedCount").textContent = `${checkedCheckboxes} / ${totalCheckboxes}`;
});

// Add confirmation functionality
document.getElementById("confirmBtn").addEventListener("click", function() {
  // Get all checked checkboxes
  const checkedCheckboxes = document.querySelectorAll(".checkbox:checked");

  // Display a confirmation message with the number of selected checkboxes
  alert(`You have selected ${checkedCheckboxes.length} Applier(s).`);

  // Optionally, you can perform additional actions here, such as sending the selected data to a server
});

// Assuming you have a container with the id "container" in your HTML
const container = document.getElementById("container");
renderApplier(container);

