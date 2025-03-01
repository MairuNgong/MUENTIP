document.addEventListener('DOMContentLoaded', function () {
    const addTagBtn = document.getElementById('add-tag-btn');
    const interestList = document.getElementById('interest-list');
    const tagDropdown = document.getElementById('tag-dropdown');

    // เพิ่ม tag จาก dropdown ไปยังรายการที่เลือก
    addTagBtn.addEventListener('click', function () {
        const selectedOption = tagDropdown.selectedOptions[0];
        if (selectedOption) {
            const newTagName = selectedOption.value;
            const newTagItem = document.createElement('li');
            newTagItem.classList.add('selected-tag');
            newTagItem.innerHTML = `
                <span>${newTagName}</span>
                <button type="button" class="remove-tag-btn" data-tag="${newTagName}">x</button>
            `;
            interestList.appendChild(newTagItem);
        }
    });

    // ลบ tag ที่เลือกออกจากรายการ
    interestList.addEventListener('click', function (e) {
        if (e.target.classList.contains('remove-tag-btn')) {
            const tagToRemove = e.target.getAttribute('data-tag');
            const tagItem = e.target.closest('li');
            tagItem.remove();
        }
    });


    const elements = document.querySelectorAll("p, span, div, h1, h2, h3, h4, h5, h6, input, textarea");

    elements.forEach(el => {
        if (el.tagName === "INPUT" || el.tagName === "TEXTAREA") {
            el.style.fontFamily = '"Noto Sans Thai", serif';
        } else if (/[ก-๙]/.test(el.textContent)) { 
            el.style.fontFamily = '"Noto Sans Thai", serif';
        }
    });

});
