$(window).on("load", function() {
    
    $(".popupDropdownRoot").on("click", function() {
        popupDropdownShow(this);
    });
    
    $("#movieInfoTrailerButton").on("click", function() {
        showYoutubeVideo();
    });
    
    $("#youtubeVideoX").on("click", function() {
        hideYoutubeVideo();
    });
    
    $(document).on("keyup", function(event) {
        // user hits the escape key while viewing the trailer
        if (event.which == 27) {
            if ($("#youtubeVideoWrapper").css("display") == "flex" && $("#youtubeVideoWrapper").css("opacity") == "1") {
                hideYoutubeVideo();
            }
        }
    });
    
    $(document).on("click", ".popupDropdownElement", function() {
        popupDropdownChange(this);
    });
    
    $(document).on("click", function(event) {
        
        // checking if any popup dropdowns need to be closed
        var dropdowns = document.getElementsByClassName("popupDropdown");
        var dropdownElements = document.getElementsByClassName("popupDropdownElements");
        for (var i = 0; i < dropdowns.length; i++) {
            if ($(dropdowns[i]).attr("id") == "opened") {
                $(dropdownElements[i]).animate({ height: "0" }, 200);
                $(dropdowns[i]).attr("id", "closed");
            }
        }
        
    });
    
});



function showYoutubeVideo() {
    
    $("#youtubeVideoWrapper").css("display", "flex");
    $("#youtubeVideoWrapper").css("opacity");
    $("#youtubeVideoWrapper").css("opacity", "1");
    
    setTimeout(function() {
        
        $("#youtubeVideo").css("display", "block");
        $("#youtubeVideo").css("top");
        $("#youtubeVideo").css("top", "0");
        $("#youtubeVideo").css("opacity");
        $("#youtubeVideo").css("opacity", "1");
        
    }, 250);
    
}



function hideYoutubeVideo() {
    
    $("#youtubeVideo").css("top");
    $("#youtubeVideo").css("top", "-50px");
    $("#youtubeVideo").css("opacity");
    $("#youtubeVideo").css("opacity", "0");
    
    setTimeout(function() {
        
        setTimeout(function() {
            $("#youtubeVideo").css("display", "none");
        }, 100);
        
        $("#youtubeVideoWrapper").css("opacity");
        $("#youtubeVideoWrapper").css("opacity", "0");
        
        setTimeout(function() {
            $("#youtubeVideoWrapper").css("display", "none");
        }, 350);
        
    }, 250);
    
    // refreshing (stopping) youtube video when it is hidden
    var iframe = document.getElementById("youtubeVideo");
    iframe.src = iframe.src;
    
}



function popupDropdownShow(root) {
    
    if ($(root).parent().find(".popupDropdownElement").length > 0) { // only show dropdown if there are dropdown elements
        
        var popupDropdown = $(root).parent();
        $(popupDropdown).attr("id", "opening");
        
        var dropdown = $(root).next();
        
        var h = getHeight(dropdown);
        if (h > 150) h = 150;
        
        $(dropdown).animate({ height: h + "px" }, 200, function() {
            $(popupDropdown).attr("id", "opened");
        });
        
    }
    
}



function popupDropdownChange(element) {
    
    var root = $(element).parent().parent().siblings()[0];
    var popupDropdown = $(root).parent();
    
    if ($(popupDropdown).hasClass("popupDropdownDays")) {
        updateTimesDropdown($(element).html());
    }
    
    var rootText = $(root).children()[0];
    $(rootText).html($(element).html());
    
}



function getHeight(selector) {
    
    $(selector).css("height", "auto");
    var h = $(selector).height();
    $(selector).css("height", "0");
    
    return h;
    
}




