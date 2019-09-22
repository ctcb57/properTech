﻿

function getUserInput() {
    var userInput = document.getElementsByName("userMessage")[0].value;
    document.getElementById("userResponse").innerHTML = userInput;
    if (userInput.includes("hello") || userInput.includes("hi")) {
        document.getElementById("chadResponse").innerHTML = "Hi! My name is Chad. How may I assist you today?"
    }
    else if (userInput.includes("lightbulb") && (userInput.includes("replace") || userInput.includes("change"))) {
        document.getElementById("chadResponse").innerHTML = "<span>I know of a great video about that. Would you like to watch it? <a href = 'https://www.youtube.com/watch?v=KQJALywkB4U'  target= '_blank'>click here to watch it!</a></span>";
    }
    else if (userInput.includes("lightbulb") && (userInput.includes("buy") || userInput.includes("purchase"))) {
        document.getElementById("chadResponse").innerHTML = "<span>Sure thing! Amazon has great deals on lightbulbs. Would you like to check it out? <a href = 'https://www.amazon.com/s?k=lightbulb&ref=nb_sb_noss_1'  target= '_blank'>click here to check it out!</a></span>";
    }
    else if (userInput.includes("no") || userInput.includes("nope") || userInput.includes("no thank you")) {
        document.getElementById("chadResponse").innerHTML = "No problem. Is there anything else I can help you with?"
    }
    else if (userInput.includes(("packers") || userInput.includes("packer")) && (userInput.includes("fan") || userInput.includes("like"))) {
        document.getElementById("chadResponse").innerHTML = "I bleed Green and Gold!"
    }
    else if (userInput.includes("bears") && (userInput.includes("fan") || userInput.includes("like"))) {
        document.getElementById("chadResponse").innerHTML = "As a professional chatbot, I try my hardest not to be offensive. I guess that means I have THAT in common with the Bears."
    }
    else if (userInput.includes("maintenance") && userInput.includes("request") && (!userInput.includes("status") && !userInput.includes("update"))) {
        document.getElementById("chadResponse").innerHTML = "<span>Aboslutely! You can submit a request for maintenance with this link: <a href = 'http://localhost:55337/maintenancerequests/create'  target= '_blank'>click here to check it out!</a></span>";
    }
    else if (userInput.includes("maintenance") && (userInput.includes("status") || userInput.includes("update"))) {
        document.getElementById("chadResponse").innerHTML = "<span>The current status of your maintenance request can be found here: <a href = 'http://localhost:55337/Residents/PendingRequests'  target= '_blank'>click here to check it out!</a></span>";;
    }
    else
    {
        document.getElementById("chadResponse").innerHTML = "I am sorry, I don't quite understand. Try asking me something else.";;
    }
}