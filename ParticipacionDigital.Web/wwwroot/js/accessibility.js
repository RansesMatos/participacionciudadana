window.accessibility = {
    currentZoom: 1.0,

    increaseFontSize: function () {
        this.currentZoom += 0.1;
        document.documentElement.style.fontSize = `${this.currentZoom}em`;
    },

    decreaseFontSize: function () {
        if (this.currentZoom > 0.5) {
            this.currentZoom -= 0.1;
            document.documentElement.style.fontSize = `${this.currentZoom}em`;
        }
    },

    toggleHighContrast: function () {
        document.body.classList.toggle('high-contrast');
    },

    toggleLiteMode: function () {
        document.body.classList.toggle('lite-mode');
    },

    readContent: function (selector) {
        // Stop any current reading
        window.speechSynthesis.cancel();

        const element = document.querySelector(selector);
        if (!element) return;

        const text = element.innerText;
        const utterance = new SpeechSynthesisUtterance(text);
        utterance.lang = 'es-ES'; // Spanish
        window.speechSynthesis.speak(utterance);
    },

    stopReading: function () {
        window.speechSynthesis.cancel();
    }
};
