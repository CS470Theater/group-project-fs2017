$(window).on("load", function() {
    
    $(".popupDropdownRoot").on("click", function() {
        popupDropdownShow(this);
    });
    
    $(document).on("click", ".popupDropdownElement", function() {
        popupDropdownChange(this);
    });
    
    $(document).on("click", ".popupDropdownElementStudent", function() {
        
        var dialogBox = $(this).closest(".dialogBox");
        
        if ($(dialogBox).attr("id") == "editStudentBox") {
            editStudentUpdateFields(this);
        }
        
        updateSiblingBookDropdown(this);
        
    });
    
    $(document).on("click", ".popupDropdownElementBook", function() {
        
        var dialogBox = $(this).closest(".dialogBox");
        
        if ($(dialogBox).attr("id") == "editBookBox") {
            editBookUpdateFields(this);
        }
        
        updateSiblingDataEntryDropdown(this);
        
    });
    
    $(document).on("click", function(event) {
        
        // checking if any popup dropdowns need to be closed
        var dropdowns = document.getElementsByClassName("popupDropdownElements");
        for (var i = 0; i < dropdowns.length; i++) {
            if ($(dropdowns[i]).css("height") != "0px") {
                $(dropdowns[i]).animate({ height: "0" }, 200);
            }
        }
        
    });
    
});



function popupDropdownShow(root) {
    
    if ($(root).parent().find(".popupDropdownElement").length > 0) { // only show dropdown if there are dropdown elements
        
        var dropdown = $(root).next();
        
        var h = getHeight(dropdown);
        if (h > 150) h = 150;
        
        $(dropdown).animate({ height: h + "px" }, 200);
        
    }
    
}



function popupDropdownChange(element) {
    
    var root = $(element).parent().parent().siblings()[0];
    console.log(root);
    var rootText = $(root).children()[0];
    $(rootText).html($(element).html());
    $(rootText).attr("id", $(element).attr("id"));
    
}



function updateSiblingBookDropdown(studentElement) {
    
    var dialogBoxContent = $(studentElement).closest(".dialogBoxContent");
    
    var booksPopupDropdown = $(dialogBoxContent).find(".popupDropdownBooks")[0];
    
    if (booksPopupDropdown !== undefined) { // if there exists a book popup dropdown to update
        
        var studentsPopupDropdown = $(dialogBoxContent).find(".popupDropdownStudents")[0];
        var studentsRootText = $(studentsPopupDropdown).find(".popupDropdownRootText")[0];
        var student = getStudentByID($(studentsRootText).attr("id"));
        
        if (student != null) {
            
            resetPopupDropdown(dialogBoxContent, ".popupDropdownBooks", "select book");
            resetPopupDropdown(dialogBoxContent, ".popupDropdownDataEntries", "select data entry");
            
            var booksDropdown = $(booksPopupDropdown).find(".popupDropdownElementsInner")[0];
            var dropdownElement;
            
            for (var i = 0; i < student.books.length; i++) {
                
                dropdownElement = $("<div id=\"" + student.books[i].bookID + "\" class=\"popupDropdownElement popupDropdownElementBook\">" + student.books[i].name + "</div>");
                dropdownElement.appendTo(booksDropdown);
                
            }
            
        }
        
    }
    
}



function getHeight(selector) {
    
    $(selector).css("height", "auto");
    var h = $(selector).height();
    $(selector).css("height", "0");
    
    return h;
    
}












































