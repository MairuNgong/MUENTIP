const activities = JSON.parse(JSON.stringify(activityModel)).cards;
const tags = JSON.parse(JSON.stringify(activityModel)).tags;
console.log(tags)
const img_people_src = "../img/people.png";
const img_clock_src = "../img/clock.png";
const img_calendar_src = "../img/calendar.png";
const img_location_src = "../img/location-pin.png";
const img_arrow_src = "../img/right-arrow.png";

function renderActivities(container, ShowedtagList, maxActivities = Infinity) {
  container.innerHTML = "";

  const filteredActivities = activities.filter(activity =>
      activity.tagsList.some(tag => ShowedtagList.some(showedTag => showedTag.tagName === tag))
  );
  const activitiesToShow = filteredActivities.slice(0, maxActivities);

  activitiesToShow.forEach(activity => {
      const activity_card = document.createElement("div");
      activity_card.className = "activity_card";

      const left_group = document.createElement("div");
      left_group.className = "left_group";

      const left_content_text = document.createElement("div");
      left_content_text.className = "left_content_text";

      const title = document.createElement("div");
      title.className = "activity_title";
      title.textContent = activity.title;

      const owner = document.createElement("div");
      const img_people = document.createElement("img");
      const owner_text = document.createElement("span");
      owner.className = "activity_detail";
      img_people.className = "icon";
      img_people.src = img_people_src;
      owner.appendChild(img_people);
      owner_text.textContent = activity.owner;
      owner.appendChild(owner_text);

      const location = document.createElement("div");
      const img_location = document.createElement("img");
      const location_text = document.createElement("span");
      location.className = "activity_detail";
      img_location.className = "icon";
      img_location.src = img_location_src;
      location.appendChild(img_location);
      location_text.textContent = activity.location;
      location.appendChild(location_text);

      const start_date_time = document.createElement("div");
      const img_start_date_time = document.createElement("img");
      const start_date_time_text = document.createElement("span");
      const start_date_time_value = document.createElement("span");
      const start_date_time_label = document.createElement("span");
      start_date_time.className = "activity_detail";
      img_start_date_time.className = "icon";
      img_start_date_time.src = img_clock_src;
      start_date_time.appendChild(img_start_date_time);
      let date_obj = new Date(activity.startDateTime);
      let date = date_obj.toISOString().split("T")[0];
      let time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
      start_date_time_label.textContent = "start : ";
      start_date_time_value.textContent = `${date} ${time}`;
      start_date_time_text.appendChild(start_date_time_label);
      start_date_time_text.appendChild(start_date_time_value);
      start_date_time.appendChild(start_date_time_text);


      const end_date_time = document.createElement("div");
      const img_end_date_time = document.createElement("img");
      const end_date_time_text = document.createElement("span");
      const end_date_time_value = document.createElement("span");
      const end_date_time_label = document.createElement("span");
      img_end_date_time.className = "icon";
      img_end_date_time.src = img_clock_src;
      end_date_time.className = "activity_detail";
      end_date_time.appendChild(img_end_date_time);
      date_obj = new Date(activity.endDateTime);
      date = date_obj.toISOString().split("T")[0];
      time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
      end_date_time_label.textContent = "end : ";
      end_date_time_value.textContent = `${date} ${time}`;
      end_date_time_text.appendChild(end_date_time_label);
      end_date_time_text.appendChild(end_date_time_value);
      end_date_time.appendChild(end_date_time_text);


      const deadline_date_time = document.createElement("div");
      const img_deadline_date_time = document.createElement("img");
      const deadline_date_time_text = document.createElement("span");
      deadline_date_time.className = "activity_detail";
      img_deadline_date_time.className = "icon";
      img_deadline_date_time.src = img_calendar_src;
      deadline_date_time.appendChild(img_deadline_date_time);
      date_obj = new Date(activity.deadlineDateTime);
      let current_date = new Date();
      if (date_obj > current_date) {
          deadline_date_time.style.color = "#57C543";
          date = date_obj.toISOString().split("T")[0];
          time = date_obj.toTimeString().split(" ")[0].split(":").slice(0, 2).join(":");
          deadline_date_time_text.textContent = `open till ${date} ${time}`;
      } else {
          deadline_date_time.style.color = "#FF0000";
          deadline_date_time_text.textContent = "Closed";
      }
      deadline_date_time.appendChild(deadline_date_time_text);

      left_content_text.appendChild(title);
      left_content_text.appendChild(owner);
      left_content_text.appendChild(location);
      left_content_text.appendChild(start_date_time);
      left_content_text.appendChild(end_date_time);
      left_content_text.appendChild(deadline_date_time);

      const participant = document.createElement("div");
      const img_participant = document.createElement("img");
      const participant_text = document.createElement("span");
      participant.className = "activity_participant";
      img_participant.src = img_people_src;
      participant_text.textContent = activity.applyCount + "/" + activity.applyMax;
      if (date_obj <= current_date) {
          participant_text.style.color = "#ACACAC";
      } else if (activity.applyCount < activity.applyMax) {
          participant_text.style.color = "#57C543";
      } else {
          participant_text.style.color = "#FF0000";
      }
      participant.appendChild(img_participant);
      participant.appendChild(participant_text);

      left_group.appendChild(left_content_text);
      left_group.appendChild(participant);

      const left_group_and_tags = document.createElement("div");
      left_group_and_tags.className = "left_group_and_tags";
      const tags = document.createElement("div");
      tags.className = "tags_group";
      activity.tagsList.forEach(tag => {
          const tag_element = document.createElement("span");
          tag_element.className = "activity_tag";
          tag_element.textContent = tag;
          tags.appendChild(tag_element);
      });

      left_group_and_tags.appendChild(left_group);
      left_group_and_tags.appendChild(tags);

      activity_card.appendChild(left_group_and_tags);

      const enter_button = document.createElement("button");
      const img_arrow = document.createElement("img");
      img_arrow.src = img_arrow_src;
      enter_button.className = "enter_button";
      enter_button.appendChild(img_arrow);

      enter_button.onclick = function () {
          window.location.href = `/ViewActivity/Index?activity_id=${activity.activityId}`;
      };

      activity_card.appendChild(enter_button);
      container.appendChild(activity_card);
  });
}

function renderHotTag(container, hot_tags) {
  hot_tags.forEach(hot_tag => {
    const hot_tag_ele = document.createElement("li");
    const link_to_section = document.createElement("a");
    hot_tag_ele.className = "hot-tag-ele";
    link_to_section.textContent = hot_tag.tagName;
    link_to_section.href = `#${hot_tag.tagName}-section`;
    hot_tag_ele.appendChild(link_to_section);
    container.appendChild(hot_tag_ele);
  })
}

function createViewAllBt(container, tag_name) {
  const view_all_bt = document.createElement("button");
  const view_all_img = document.createElement("img");
  view_all_bt.textContent = "View All";
  view_all_bt.className = "activity_card";
  view_all_bt.id = "view-all-bt";
  view_all_img.src = "../img/view-all.png";
  view_all_bt.onclick = function() {
    window.location.href = `/ViewAll/Index?tag_name=${tag_name}`;
  }
  view_all_bt.appendChild(view_all_img);
  container.appendChild(view_all_bt);
}

function renderHotTagSection(container, hot_tags) {
  hot_tags.forEach(hot_tag => {
    const hot_tag_section = document.createElement("section");
    const hot_tag_title = document.createElement("h1");
    const hot_tag_act_div = document.createElement("div");
    const hot_tag_lst = [hot_tag];
    hot_tag_section.id = `${hot_tag.tagName}-section`;
    hot_tag_title.textContent = "# " + hot_tag.tagName;
    hot_tag_act_div.className = "act-div";
    hot_tag_section.appendChild(hot_tag_title);
    hot_tag_section.appendChild(hot_tag_act_div);
    container.appendChild(hot_tag_section);
    renderActivities(hot_tag_act_div, hot_tag_lst, 5);
    if (hot_tag_act_div.children.length > 4) {
      createViewAllBt(hot_tag_act_div, hot_tag.tagName);
    }
  })
}

const realtime_container = document.getElementById("realtime-activity");
const hot_tag_container = document.getElementById("tag-nav");

// ต้องเขียน filter ใหม่นับactก่อนrender 
renderActivities(realtime_container, tags, 5);
if (realtime_container.children.length > 4) {
  createViewAllBt(realtime_container, "Realtime");
}
renderHotTag(hot_tag_container, tags);
renderHotTagSection(document.body, tags);
