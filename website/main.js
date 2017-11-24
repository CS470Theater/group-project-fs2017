$(window).on("load", function() {
    
    $(".loginBoxHeaderElement").on("click", function() {
        
        if (!$(this).hasClass("loginBoxHeaderElementActive")) {
            
            // swapping active state
            $(".loginBoxHeaderElement").removeClass("loginBoxHeaderElementActive");
            $(this).addClass("loginBoxHeaderElementActive");
            
            if ($(this).html() == "login") {
                animateToLoginContent();
            } else {
                animateToRegisterContent();
            }
            
        }
        
    });
    
});

// 333 x 348
// 596 x 371

function animateToLoginContent() {
    
    $("#loginBoxContentRegister").css("display", "none");
    $("#loginBoxContentLogin").css("display", "block");
    $("#loginBox").css("width", "333px");
    $("#loginBox").css("height", "348px");
    
}

function animateToRegisterContent() {
    
    $("#loginBoxContentLogin").css("display", "none");
    $("#loginBoxContentRegister").css("display", "flex");
    $("#loginBox").css("width", "596px");
    $("#loginBox").css("height", "371px");
    
}
































