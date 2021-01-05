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

    $('#DeleteUserModal').on('show.bs.modal', function (e) {
        var deletePath = $(e.relatedTarget).data('user-id');
        $(this).find('a').attr('href', "UserHome/DeleteUser/" + deletePath);
    });

    $('#UpdatePasswordModal').on('show.bs.modal', function (e) {
        var userId = $(e.relatedTarget).data('user-id');
        $(this).find('input[name="id"]').val(userId);
    });

    $('#DisableUserModal').on('show.bs.modal', function (e) {
        var userId = $(e.relatedTarget).data('user-id');
        $(this).find('input[name="id"]').val(userId);
    });

    $('#EnableUserModal').on('show.bs.modal', function (e) {
        var userId = $(e.relatedTarget).data('user-id');
        $(this).find('input[name="id"]').val(userId);
    });
});

