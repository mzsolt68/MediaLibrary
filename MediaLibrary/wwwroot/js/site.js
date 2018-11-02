// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#AddPerformer").on("click", function () {
    var id = $("#SelectedPerformer").val();
    if (id < 1) {
        alert("Válasszon előadót!");
    } else {
        $.post("/Songs/AddPerformer/", { id: id }, function (data) {
            $("#songperformers").append(data);
        });    
    }
})
$('#songperformers').on("click", ".DeletePerformer", function () {
    var id = $(this).closest('div').attr('id');
    $(this).closest('.PerformerRow').remove();
})