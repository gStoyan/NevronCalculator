
$(function () {
    $("#addButton").on("click", function () {
        const randomNumber = getRandomInRange(101, 999);
       

        $.ajax({
            url: "/Home/AddNumber", 
            type: "Get",
            contentType: "application/json",
            data: { randomNumber: randomNumber }, 
            success: function (response) {
                const newSpan = $("<span>")
                    .addClass("displayed-number")
                    .text(randomNumber);
                $("#numbers").append(newSpan).append(" ");
                $("#count").text(response.count); 
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    });

    $("#clearButton").on("click", function () {
        $.ajax({
            url: "/Home/Clear",
            type: "Get",
            success: function (response) {
                $("#numbers").empty();
                $("#count").text(""); 
                $("#sum").text("");
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    });

    $("#sumButton").on("click", function () {
        const selectors = document.querySelectorAll('#numbers > span');
        const numbers = [...selectors].map(span => parseInt(span.innerText.replace(/"/g, "")));
        console.log(numbers)
        $.ajax({
            url: "/Home/SumNumbers",
            type: "Get",
            dataType: "json",
            contentType: "application/json",
            data: {numbers: numbers}
,
            traditional: true,
            success: function (response) {
                $("#sum").text(response.sum);
            },
            error: function (error) {
                console.error("Error:", error);
            }
        });
    });
});


function getRandomInRange(minVal, maxVal) {
    var randVal = minVal + (Math.random() * (maxVal - minVal));
    return Math.round(randVal);
}
