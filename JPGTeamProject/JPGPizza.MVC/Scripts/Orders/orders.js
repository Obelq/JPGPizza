var shoppingCart = []; 

$(document).ready(() => {
    if (sessionStorage.getItem('shoppingCart') != null) {
        $('.badge').text(JSON.parse(sessionStorage.getItem('shoppingCart')).length);
    }

    $('.shopping-cart').click((ev) => {
        appendToShoppingCart();
    })
    $('#empyShoppingCart').click((ev) => {
        sessionStorage.setItem('shoppingCart', JSON.stringify([]));
        $('.badge').text(JSON.parse(sessionStorage.getItem('shoppingCart')).length);
        appendToShoppingCart();
    })
    $('#logout-btn').click((ev) => {
        sessionStorage.setItem('shoppingCart', JSON.stringify([]));
    });
    $('#finish-Order').click((ev) => {
        finishOrder();
    });

    $('.glyphicon-plus-sign').click((ev) => {
        increaseBtn(ev);
    })

    $('.glyphicon-minus-sign').click((ev) => {
        deceraseBtn(ev);
    })

    addToCart();
});


function addOrderProductToCart(orderProduct) {
    let shoppingCart = sessionStorage.getItem('shoppingCart');

    if (!shoppingCart) {
        sessionStorage.setItem('shoppingCart', JSON.stringify([]));
        shoppingCart = sessionStorage.getItem('shoppingCart');
    }

    shoppingCart = JSON.parse(shoppingCart);
    shoppingCart.push(orderProduct);

    sessionStorage.setItem('shoppingCart', JSON.stringify(shoppingCart));
}

function removeOrderProductByProductId(productId) {
    let shoppingCart = JSON.parse(sessionStorage.getItem('shoppingCart'));

    let orderProductToRemoveIndex;
    for (let i = 0; i < shoppingCart.length; i++) {
        if (shoppingCart[i].productId === productId) {
            orderProductToRemoveIndex = i;
            break;
        }
    }

    shoppingCart.splice(orderProductToRemoveIndex, 1);
    sessionStorage.setItem('shoppingCart', JSON.stringify(shoppingCart));
}

function appendToShoppingCart() {

    let shoppingCart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    let productsInCart = ``;
    if (shoppingCart.length >= 0) {
        let total = 0;
        productsInCart += `<div class="row">
                            <div class="col-sm-3">
                                <span>`+ "Продукт" + `</span>
                            </div>
                            <div class="col-sm-3" style="padding-left: 20px;">`
            + "Количество" +
            `</div>
                             <div class="col-sm-3">`
            + "Цена" +
            `</div>
                        </div>
                        <hr/>`
        for (let orderProduct of shoppingCart) {
            total += orderProduct.price;
            productsInCart +=
                `<div class="row">
                            <div class="col-sm-3">
                                <span>`+ orderProduct.name + `</span>
                            </div>
                            <div class="col-sm-3" style="padding-left: 20px;">`
                + orderProduct.quantity + " бр." +
                `</div>
                                 <div class="col-sm-3">`
                + orderProduct.price.toFixed(2) + " лв." +
                `</div>
                             <div class="col-sm-3">
                                <a class="removeFromCart" data-product-id-value=`+ orderProduct.id + `><span class="glyphicon glyphicon-remove"></span></a>
                            </div>
                        </div>                        
                        <hr/>`;
        }
        productsInCart += `<div class="row">
                                     <span class="col-sm-5">`+ "Общо: " + `</span>
                                     <span class="col-sm-5">`+ total.toFixed(2) + " лв." + `</span>
                                   </div>`

        $('#shopping-cart-list span').empty();
        $('#shopping-cart-list span').append(productsInCart);
        removeFromCart(shoppingCart);
    }
}

function finishOrder() {
    let shoppingCart = sessionStorage.getItem('shoppingCart');         
    $.ajax({
        type: "POST",
        url: "/Orders/ShopingCartList",
        data: shoppingCart,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            sessionStorage.setItem('shoppingCart', JSON.stringify([]));
            document.location = '/manage';
        },
        error: function (msg) {
            appendRegisterPermission();
        }
    });
}

function appendRegisterPermission() {
    let permission = `<div>Трябва да сте регистиран потребител</div>`
    $('#shopping-cart-list').append(permission);
}

function removeFromCart(shoppingCart) {
    $('.removeFromCart').click((ev) => {

        let evTarget = $(ev.currentTarget);
        let productId = ev.currentTarget.getAttribute('data-product-id-value');
        let index = getIndexById(shoppingCart, productId);
        if (index != null) {
            shoppingCart.splice(index, 1);
            sessionStorage.setItem('shoppingCart', JSON.stringify(shoppingCart));
        }
        appendToShoppingCart();
        $('.badge').text(JSON.parse(sessionStorage.getItem('shoppingCart')).length);
    });
}

function getIndexById(shoppingCart, productId) {
    for (var i = 0; i < shoppingCart.length; i++) {
        if (shoppingCart[i].id === productId) {
            return i;
        }
    }
    return null;
}

function deceraseBtn(ev) {    
        let evTarget = $(ev.currentTarget);
        let productPrice = ev.currentTarget.getAttribute('data-product-price-value');
        let quantity = parseFloat($('.quantity-value').text());
        if (quantity > 1) {
            quantity--;
            $('.quantity-value').text(quantity)
            let newPrice = productPrice * quantity;
            $('.product-order-price').text(newPrice.toFixed(2) + " лв.");
        }
}

function increaseBtn(ev) {
    
        let price = $('.product-order-price');
        let evTarget = $(ev.currentTarget);
        let productPrice = ev.currentTarget.getAttribute('data-product-price-value');
        let quantity = parseFloat($('.quantity-value').text());
        quantity++;
               
        $('.quantity-value').text(quantity);
        let newPrice = productPrice * quantity;
        $('.product-order-price').text(newPrice.toFixed(2) + " лв.");

}

function addToCart() {
    $('#add-to-cart').click((ev) => {
        ev.preventDefault();
        let evTarget = $(ev.currentTarget);
        let productId = ev.currentTarget.getAttribute('data-product-id-value');
        let quantity = parseFloat($('.quantity-value').text());
        let productName = ev.currentTarget.getAttribute('data-product-name-value');
        let productPrice = ev.currentTarget.getAttribute('data-product-price-value');
        let newPrice = productPrice * quantity;
        let orderProduct = {
            id: productId,
            name: productName,
            price: newPrice,
            quantity: quantity
        };
        document.location = '/orders/carryout';
        addOrderProductToCart(orderProduct);
    });
}

function addOrderProductToCart(orderProduct) {
    let shoppingCart = sessionStorage.getItem('shoppingCart');

    if (!shoppingCart) {
        sessionStorage.setItem('shoppingCart', JSON.stringify([]));
        shoppingCart = sessionStorage.getItem('shoppingCart');
    }

    shoppingCart = JSON.parse(shoppingCart);

    let orderProductFromCart = getOrderProductById(orderProduct.id, shoppingCart);

    if (orderProductFromCart === null) {
        shoppingCart.push(orderProduct);
    } else {
        orderProductFromCart.quantity += orderProduct.quantity;
    }

    sessionStorage.setItem('shoppingCart', JSON.stringify(shoppingCart));
    if (shoppingCart.length >= 1) $('.badge').text(JSON.parse(sessionStorage.getItem('shoppingCart')).length);
}

function getOrderProductById(id, orderProducts) {
    for (let orderProduct of orderProducts) {
        if (orderProduct.id === id) {
            return orderProduct;
        }
    }

    return null;
}

function removeOrderProductByProductId(productId) {
    let shoppingCart = JSON.parse(sessionStorage.getItem('shoppingCart'));

    let orderProductToRemoveIndex;
    for (let i = 0; i < shoppingCart.length; i++) {
        if (shoppingCart[i].productId === productId) {
            orderProductToRemoveIndex = i;
            break;
        }
    }

    shoppingCart.splice(orderProductToRemoveIndex, 1);
    sessionStorage.setItem('shoppingCart', JSON.stringify(shoppingCart));
}


