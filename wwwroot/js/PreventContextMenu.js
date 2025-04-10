document.addEventListener('keydown', function (e) {
    if (
        (e.ctrlKey && e.shiftKey && (e.key === 'I' || e.key === 'C' || e.key === 'J')) ||
        e.key === 'F12'
    ) {
        e.preventDefault();
    }
});

document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
});