document.addEventListener('DOMContentLoaded', () => {
    const toggleButton = document.getElementById('notification-toggle');
    const notificationPanel = document.getElementById('notification-panel');
    const notificationBody = document.getElementById('notification-body');
    const notificationCloseButton = document.getElementById('notification-close');
    const userPanel = document.getElementById('user-panel');
    const userCloseButton = document.getElementById('user-close');
    const avatar = document.getElementById('user-avatar');

    toggleButton.addEventListener('click', async (e) => {
        e.stopPropagation();
        notificationPanel.classList.toggle('active');

        if (notificationPanel.classList.contains('active')) {
            try {
                const response = await fetch('/Notification-Partial');
                const html = await response.text();
                notificationBody.innerHTML = html;
            } catch (error) {
                console.error("Failed to load notifications:", error);
            }
        }
    });

    notificationCloseButton.addEventListener('click', (e) => {
        e.stopPropagation();
        notificationPanel.classList.remove('active');
    });

    userCloseButton.addEventListener('click', (e) => {
        e.stopPropagation();
        userPanel.classList.remove('active');
    });

    document.addEventListener('click', (e) => {
        if (!notificationPanel.contains(e.target) && !toggleButton.contains(e.target)) {
            notificationPanel.classList.remove('active');
        }

        if (!userPanel.contains(e.target) && !avatar.contains(e.target)) {
            userPanel.classList.remove('active');
        }
    });

    avatar.addEventListener('click', (e) => {
        e.stopPropagation();
        userPanel.classList.toggle('active');
    });
});
