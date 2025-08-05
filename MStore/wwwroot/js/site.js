

function addToCart(productId) {

      fetch(`/Cart/AddToCart?productId=${productId}`, {
        method: 'POST'

      }).then(response => response.json())
        .then(res => {
            if (res.success) {
                document.getElementById("cart-count").textContent = res.count;

                Toastify({
                    text: "Item Added Successfully ✅",
                    duration: 1500,
                    gravity: "top",
                    position: "right",
                    backgroundColor: "#28a745"
                }).showToast();
            } else {
                alert("Failed to add");
            }
        })
        .catch(error => {
            alert("Failed to add to cart");
            console.error("Error:", error);
        });
}

//make event dynamic insated make it on each button need | like onClick = ()

//document.addEventListener("DOMContentLoaded", function ()  {
//    document.querySelectorAll(".add-to-cart-btn").forEach(function(button) {
//        button.addEventListener("click", function () {
//            //dataset contain data-attr make it in html tag
//            var productId = this.dataset.productId;
//            addToCart(productId);          
//        });
//    });
//});

document.addEventListener("click", function (e) {
    const button = e.target.closest(".add-to-cart-btn");
    if (button) {
        const productId = button.dataset.productId;
        addToCart(productId);
    }
});

//prtaice ajax

//const getData = (apiLink) => {

//    return new Promise((resolve, rejecte) => {

//        let myRequest = new XMLHttpRequest();
//        myRequest.onload = function () {
//            if (this.readyState === 4 && this.status === 200) {
//                resolve(JSON.parse(this.responseText));
//            } else {
//                rejecte(Error("No Data found"));
//            }
//        };

//    });
//}

//getData(`/Cart/UpdateQuantity?productId=${productId}&change=${change}`)
//    .then((result) => {
//        console.log(result)
//    }).then((newResultReturnForm1Then) => {
//        console.log(result[0].Name)
//    }).catch();



//funcation fetch make the same thing getData return it then use 
//more easier 