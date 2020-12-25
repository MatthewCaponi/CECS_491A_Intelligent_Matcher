$(document).ready(function () {

    $(".dynamic-row").on("mouseover", function () {
        $(this).addClass("row-highlight");
        var userId = $(this).attr('id');
        $("#three-dot-button-" + userId).show();
        
    });

    $(".three-dot-button-box").on("mouseover", function() {
        var userId = $(this).attr('id');
    });


    $(".dynamic-row").on("mouseout", function () {
        $(this).removeClass("row-highlight");
        var userId = $(this).attr('id');
        $("#three-dot-button-" + userId).hide();
     
    });

    $(".three-dot-button-box").on("click", function () {
        var clicked = $(this);
        clicked.css("background-color", "grey");    
        
    });

    $(".three-dot-button-box").on("mouseout", function () {
        $(this).css("background-color", "transparent");
    });
});

