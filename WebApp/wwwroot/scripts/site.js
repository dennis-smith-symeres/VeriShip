// site.js
let scrollInterval;

function enableAutoScroll() {
    const scrollMargin = 50; // Distance (in pixels) to start scrolling
    const scrollSpeed = 15; // Pixels to scroll per interval (adjust for speed)

    // Clear any existing scrolling interval when active
    function clearScroll() {
        if (scrollInterval) {
            clearInterval(scrollInterval);
            scrollInterval = null;
        }
    }

    document.addEventListener('drag', (event) => {
        clearScroll();

        // Get the mouse position
        const mouseY = event.clientY;
        const viewportHeight = window.innerHeight;

        // If near the top of the viewport, start scrolling up
        if (mouseY < scrollMargin) {
            scrollInterval = setInterval(() => {
                window.scrollBy(0, -scrollSpeed); // Scroll upward
            }, 10);
        }

        // If near the bottom of the viewport, start scrolling down
        else if (mouseY > viewportHeight - scrollMargin) {
            scrollInterval = setInterval(() => {
                window.scrollBy(0, scrollSpeed); // Scroll downward
            }, 10);
        }
    });

    document.addEventListener('dragend', () => {
        clearScroll(); // Stop scrolling when drag ends
    });
}