
let fonts = ["cursive", "sans-serif", "serif", "monospace"];

// Function to generate a random string for CAPTCHA
function generateCaptcha() {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var captcha = '';
    for (var i = 0; i < 6; i++) {
        captcha += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return captcha;
}

// Function to display the CAPTCHA image
function displayCaptcha() {
    var captcha = generateCaptcha();
    let html = captcha.split("").map((char) => {
        const rotate = -20 + Math.trunc(Math.random() * 30);
        const font = Math.trunc(Math.random() * fonts.length);
        return `<span style="transform:rotate(${rotate}deg);font-family:${fonts[font]}">${char}</span>`;
    }).join("");
    document.querySelector(".captchaText").innerHTML = html;
}

// Function to refresh the CAPTCHA
function refreshCaptcha() {
    displayCaptcha();
}

// Function to validate the user input against the generated CAPTCHA
function validateCaptcha() {
    var userInput = document.getElementById('captchaInput').value;
    //   var captchaImage = document.getElementById('captchaImage').alt;
    var captchaText = document.querySelector(".captchaText").textContent;

    if (userInput !== captchaText) {
        alert('CAPTCHA is incorrect. Please try again.');
        refreshCaptcha();
        return false;
    }
}

// Initial display of CAPTCHA on page load
displayCaptcha();

document.getElementById("refresh-captcha").addEventListener("click", () => {
    refreshCaptcha();
});

document.querySelector("form").onsubmit = (e) => {

    validateCaptcha();

    if (validateCaptcha() === false) {
        e.preventDefault();
    }
}