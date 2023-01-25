$(document).on("click", ".show-product-modal", function (e) {
    e.preventDefault();

    console.log(e.target)

    var url = e.target.parentElement.href;

    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.product-details-modal').html(data);
            console.log(data)
        })

    $("#quickModal").modal("show");
})



$('.a-tag li a').filter(function () {
    return this.href === location.href;
}).addClass('active');