var Appliers = JSON.parse(JSON.stringify(selectModel)).appliers;
var activity_id = JSON.parse(JSON.stringify(selectModel)).activity_id;

console.log(activity_id);
console.log(selectModel);

function renderApplier(container) {
    // Remove all child elements from the container to overwrite it
    container.innerHTML = "";

    // Iterate over the applicants (display owner info now)
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
            img_people.src = applicant.userImgLink; // Display the applicant's profile picture
        } else {
            img_people.src = "/img/default-profile.png";
        }

        img_people.addEventListener("click", function () {
            window.location.href = `/Profile/Index?id=${applicant.userId}`;
        });

        owner.className = "applicant_detail";
        owner.appendChild(img_people);

        const owner_text = document.createElement("span");
        owner_text.textContent = `${applicant.userName}`; // Display username and email

        owner.appendChild(owner_text);

        left_content_text.appendChild(owner);

        left_group.appendChild(left_content_text);

        applicant_card.appendChild(left_group);

        container.appendChild(applicant_card);
    });
}

document.getElementById("picture").addEventListener("click", function () {
    window.location.href = `/Profile/Index?id=${selectModel.ownerId}`;
})

// Assuming you have a container with the id "container" in your HTML
const container = document.getElementById("container");
renderApplier(container);
