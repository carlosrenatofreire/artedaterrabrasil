// DarkMode Toggle
const mode = document.getElementById("mode-toggle");

// On page load, set the theme based on local storage
const savedTheme = localStorage.getItem("theme");
if (savedTheme) {
    document.documentElement.setAttribute("data-bs-theme", savedTheme);
    updateIcon(savedTheme); // Update the icon on page load
} else {
    // If no theme is saved, default to 'light'
    document.documentElement.setAttribute("data-bs-theme", "light");
    updateIcon("light"); // Set default icon
}

// Add event listener to toggle the theme and save it to local storage
mode.addEventListener("click", () => {
    // Get the html element
    const html = document.documentElement;
    // Toggle the theme between 'dark' and 'light'
    const currentTheme = html.getAttribute("data-bs-theme");
    const newTheme = currentTheme === "dark" ? "light" : "dark";

    html.setAttribute("data-bs-theme", newTheme);

    // Save the new theme in local storage
    localStorage.setItem("theme", newTheme);

    // Update the icon
    updateIcon(newTheme);
});

// Function to update the mode icon based on the theme
function updateIcon(theme) {
    if (theme === "dark") {
        mode.classList.remove("fa-sun");
        mode.classList.add("fa-moon");
    } else {
        mode.classList.remove("fa-moon");
        mode.classList.add("fa-sun");
    }
}