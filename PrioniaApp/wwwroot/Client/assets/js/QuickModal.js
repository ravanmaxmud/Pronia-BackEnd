
$(document).ready(function () {



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




    let btns = document.querySelectorAll(".add-product-to-basket-btn")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    }))


    $(document).on("click", ".add-product-to-basket-modal", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    })



    $(document).on("click", ".remove-product-to-basket-btn", function (e) {
        e.preventDefault();

        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    })

    $(document).on("click", ".plus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.cart-block').html(data);
                    })
            })
    })






    $(document).on("click", ".minus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.cart-block').html(data);

                    })
            })
    })


});