
$(document).ready(() => {
    let form = $("#cart-form");
    form.validate().settings.ignore = [];
    let initializeDynamicButtons = null;
    class Cartline {
        constructor(product) {

            this.product = JSON.parse(product);
            this.quantity = 1;
          
           
        }
      
    }
    class Cart {
        constructor(cartelement)
        {
            this.cartlines = new Map();
            this.cartElement = cartelement;
            this.amount = 0;
            
        };

        addCartLine(selectedProduct,cartline)
        {
            

            if (this.cartlines.has(selectedProduct))
            {
                this.cartlines.get(selectedProduct).quantity++;
                return;
            };
            this.cartlines.set(selectedProduct, cartline);
        };

        addProduct(product)
        {

            this.cartlines.get(product).quantity++;
        }
        
        removeProduct(product)
        {

            this.cartlines.get(product).quantity--;
            if (this.cartlines.get(product).quantity < 1)
            {
                this.cartlines.delete(product);
            }
        }

        save()
        {
            let JsonLines = [...this.cartlines];
            sessionStorage.setItem("cart", JSON.stringify(JsonLines));
        };
        get()
        {
            this.cartlines = new Map(JSON.parse(sessionStorage.getItem("cart")));
            
        };
        refresh() {

            let elem = this.cartElement;
            elem.empty();
            let total = 0;
            let totalprice = new Intl.NumberFormat("uk-Ua",
                {
                    style: "currency", currency: "UAH",
                    minimumFractionDigits: 2
                });
            if (this.cartlines.size > 0)
            {
                this.cartlines.forEach(function (value, key, map) {

                    let prod = JSON.parse(key);

                    elem.append(` <li class="list-group-item">
                            <div class="row cart-row mb-2">
                                <div class="col-2 text-left">x${value.quantity}</div>
                                <div class="col-5 text-center">${prod.name}</div>
                                <div class="col-5 text-right">${totalprice.format(prod.price)}</div>
                            </div>
                            <div class="row cart-row-control">
                                <div class="col-6 text-left p-0">
                                <button type="button" class="btn remove"  data-cartline='${key}'>
                                    <img src="/icons/dash-circle.svg" alt="rem-prod" width=24; height=24;>
                                    </img>
                                </button>
                                </div>
                                <div class="col-6 text-right p-0">
                                <button type="button" class="btn add"  data-cartline='${key}' >
                                    <img src="/icons/plus-circle.svg" alt="add-prod" width=24; height=24;>
                                    </img>
                                </button>
                                </div>
                            </div>
                        </li>`);
                    total = total + prod.price * value.quantity;

                });
                this.amount = total;
                $("#total").text(`Сумма : ${totalprice.format(total)}`);
                initializeDynamicButtons();
                $(".cart-empty").hide();
                $("#cart").show();
                $(".cart-control").show();
                this.save();
            }
            else
            {
                
                $("#cart").hide();
                $("#cart-form").hide();
                $(".cart-empty").show();
                this.save();
            }
            

        }
        clear()
        {
            sessionStorage.clear();
            this.cartlines.clear();
            this.refresh();
        };
    }
    let cartElement = $("#cart-list");
    let cart = new Cart(cartElement);
    $(".btn-primary.product").on("click", function () {


        let selectedProduct = $(this).attr("data-product");

        let newCartline = new Cartline(selectedProduct);

        cart.addCartLine(selectedProduct, newCartline);

        cart.refresh();


    });
    $(".btn-primary.confirm").on("click", function () {
        
        $(".cart-control").hide();
        $("#cart-form").show();
        
    });
    
    $(".btn-primary.submit").on("click", function () {
        if (form.valid())
        {
        let lines = [...cart.cartlines.values()];
        let date = new Date().toUTCString();
        let Jsonlines = JSON.stringify(lines);
        $("#cart-products").val(Jsonlines);
        $("#cart-amount").val(cart.amount);
        $("#cart-date").val(date);
            cart.clear();
        }
    });
    $("#toggler_mb1").on("click", function () {
        
        $("#col-cart").removeClass("d-none");
        $("#toggler_mb2").removeClass("d-none");
        $("#products").addClass("d-none");
        $(this).addClass("d-none");
    });
    $("#toggler_mb2").on("click", function () {
        $("#col-cart").addClass("d-none");
        $("#toggler_mb1").removeClass("d-none");
        $("#products").removeClass("d-none");
        $(this).addClass("d-none");
    });
    
    initializeDynamicButtons = function () {
        $(".btn.add").on("click", function () {


            let prod = $(this).attr("data-cartline");

            cart.addProduct(prod);

            cart.refresh();

        });
        $(".btn.remove").on("click", function () {


            let prod = $(this).attr("data-cartline");

            cart.removeProduct(prod);

            cart.refresh();


        });
    }


    if (sessionStorage.getItem("cart") != null)
    {
        cart.get();
        cart.refresh();
    }
    else
    {
        cart.save();
    }
   
    
   


})



