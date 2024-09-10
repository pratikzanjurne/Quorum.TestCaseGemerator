let newX = 0, newY = 0, startX = 0, startY = 0;
let rotation = 0;  // For tracking rotation of the line

const shapes = document.querySelectorAll('.shape');
const line = document.getElementById('line');

// Add event listeners to the original shapes in the toolbox
shapes.forEach(shape => {
    shape.addEventListener('mousedown', createCloneAndDrag);
});

line.addEventListener('mousedown', createCloneAndDragForLine);
window.addEventListener('keydown', rotateLine);  // Add event listener for rotating the line with keyboard

// Function to clone the shape and start dragging
function createCloneAndDrag(e) {
    const originalShape = e.target;

    // Clone the shape so the original stays in the toolbox
    const clonedShape = originalShape.cloneNode(true);
    clonedShape.style.position = 'absolute';
    clonedShape.style.zIndex = 1000; // Bring it to the front
    clonedShape.style.left = originalShape.getBoundingClientRect().left + 'px';
    clonedShape.style.top = originalShape.getBoundingClientRect().top + 'px';

    document.body.appendChild(clonedShape);  // Add cloned shape to the body for dragging

    // Call the function to make the cloned shape draggable
    makeShapeDraggable(clonedShape, e);
}

// Function to clone the line and make it draggable
function createCloneAndDragForLine(e) {
    const originalLine = e.target;

    // Clone the line so the original stays in the toolbox
    const clonedLine = originalLine.cloneNode(true);
    clonedLine.style.position = 'absolute';
    clonedLine.style.zIndex = 1000; // Bring it to the front
    clonedLine.style.left = originalLine.getBoundingClientRect().left + 'px';
    clonedLine.style.top = originalLine.getBoundingClientRect().top + 'px';

    document.body.appendChild(clonedLine);  // Add cloned line to the body for dragging

    // Call the function to make the cloned line draggable
    makeShapeDraggable(clonedLine, e);
}

// Function to make a shape draggable
function makeShapeDraggable(shape, e) {
    startX = e.clientX;
    startY = e.clientY;

    // Move the shape as the mouse moves
    function mouseMove(e) {
        newX = startX - e.clientX;
        newY = startY - e.clientY;

        startX = e.clientX;
        startY = e.clientY;

        shape.style.top = (shape.offsetTop - newY) + 'px';
        shape.style.left = (shape.offsetLeft - newX) + 'px';
    }

    function mouseUp() {
        document.removeEventListener('mousemove', mouseMove);
        document.removeEventListener('mouseup', mouseUp);
    }

    document.addEventListener('mousemove', mouseMove);
    document.addEventListener('mouseup', mouseUp);

    // Allow the cloned shape to be dragged again after it's dropped
    shape.addEventListener('mousedown', (e) => {
        makeShapeDraggable(shape, e);  // Reattach draggable functionality
    });
}

// Function to rotate the line using arrow keys (left and right)
function rotateLine(e) {
    const lines = document.querySelectorAll('.line');  // Get all lines (in case there are multiple)

    lines.forEach((lineElement) => {
        if (e.key === 'ArrowLeft') {
            rotation -= 5;  // Rotate left
        } else if (e.key === 'ArrowRight') {
            rotation += 5;  // Rotate right
        }
        lineElement.style.transform = `rotate(${rotation}deg)`;
    });
}

// Function to handle dragging the line
function mouseDownLine(e) {
    const line = e.target;

    line.style.position = 'absolute';
    line.style.zIndex = 1000;  // Bring it to the front

    startX = e.clientX;
    startY = e.clientY;

    function mouseMove(e) {
        newX = startX - e.clientX;
        newY = startY - e.clientY;

        startX = e.clientX;
        startY = e.clientY;

        line.style.top = (line.offsetTop - newY) + 'px';
        line.style.left = (line.offsetLeft - newX) + 'px';
    }

    function mouseUp() {
        document.removeEventListener('mousemove', mouseMove);
        document.removeEventListener('mouseup', mouseUp);
    }

    document.addEventListener('mousemove', mouseMove);
    document.addEventListener('mouseup', mouseUp);
}
