
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


    //$(document).on("click", ".select-catagory", function (e) {
    //    e.preventDefault();

    //    console.log(e.target.href)
    //    fetch(e.target.href)
    //        .then(response => response.text())
    //        .then(data => {
    //            $('.shoppage-product').html(data);
    //        })
    //})


    $(document).on("change", ".searchproductPrice", function (e)
    {
        e.preventDefault();

        let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
        let MinPrice = parseInt(minPrice);

        let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
        let MaxPrice = parseInt(maxPrice);
        let aHref = document.querySelector('.shopping-cart').href;

        console.log(MinPrice);
        console.log(MaxPrice);
        console.log(aHref)

        $.ajax(
            {
                url: aHref,

                data: {
                    minPrice: MinPrice,
                    maxPrice: MaxPrice

                },

                success: function (response) {
                    //$(".viewcompadd").html(null);
                    console.log(response)
                    $(".viewcompadd").html(response);




                },
                error: function (err) {
                    $(".product-details-modal").html(err.responseText);

                }

            });


    })


});