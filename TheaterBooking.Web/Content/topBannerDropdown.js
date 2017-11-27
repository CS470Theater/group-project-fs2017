$(function () {

    $("#userDropdownWrapper").on("click", function () {
        dropdownToggle("#userDropdown");
    });

    $(document).on("click", function (event) {

        if ($("#userDropdown").attr("class") == "opened") {
            dropdownToggle("#userDropdown");
        }

    });

});

function dropdownToggle(selector) {

    $(selector).attr("class", "opening");

    if ($(selector).css("height") == "0px") {
        $(selector).animate({ height: getHeight(selector) + "px" }, 200, function () {
            $(selector).attr("class", "opened");
        });
    } else {
        $(selector).animate({ height: "0" }, 200, function () {
            $(selector).attr("class", "closed");
        });
    }

}

function getHeight(selector) {

    $(selector).css("height", "auto");
    var h = $(selector).height();
    $(selector).css("height", "0");

    return h;

}
