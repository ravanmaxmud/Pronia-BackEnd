$(document).on("click", ".show-product-modal", function (e) {
    e.preventDefault();

    var url = e.target.href;
    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.product-details-modal').html(data);
            console.log(data)
        })

    $("#quickModal").modal("show");
})
