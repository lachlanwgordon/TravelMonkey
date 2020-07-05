function setDestination(url, destinationTitle) {
    var image = document.getElementById("carousel");
    image.src = url;
    var title = document.getElementById("destination-title");
    title.textContent = destinationTitle;
};
