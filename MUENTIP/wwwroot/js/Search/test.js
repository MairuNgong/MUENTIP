document.getElementById("tag_button").addEventListener("click", function() {
    var searchTag = document.getElementById("search_tag");
    if (searchTag) {
        searchTag.remove(); // ลบ <span> ออกจาก DOM
        tag_button.remove()
    }
});
