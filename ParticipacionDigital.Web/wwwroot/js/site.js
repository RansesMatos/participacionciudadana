window.renderDashboardCharts = (voteLabels, voteData, timeLabels, timeValues) => {
    // Votes Chart
    const ctxVotes = document.getElementById('votesChart');
    if (ctxVotes) {
        if (window.votesChartInstance) window.votesChartInstance.destroy(); // Destroy previous instance

        window.votesChartInstance = new Chart(ctxVotes, {
            type: 'bar',
            data: {
                labels: voteLabels,
                datasets: [{
                    label: '# de Votos',
                    data: voteData,
                    backgroundColor: 'rgba(54, 162, 235, 0.6)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: { color: '#000' },
                        grid: { color: 'rgba(0,0,0,0.1)' }
                    },
                    x: {
                        ticks: { color: '#000' },
                        grid: { display: false }
                    }
                }
            }
        });
    }

    // Activity Chart
    const ctxActivity = document.getElementById('activityChart');
    if (ctxActivity) {
        if (window.activityChartInstance) window.activityChartInstance.destroy();

        window.activityChartInstance = new Chart(ctxActivity, {
            type: 'line',
            data: {
                labels: timeLabels,
                datasets: [{
                    label: 'Votos Diarios',
                    data: timeValues,
                    fill: true,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgb(75, 192, 192)',
                    borderWidth: 3,
                    tension: 0.1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: { color: '#000' },
                        grid: { color: 'rgba(0,0,0,0.1)' }
                    },
                    x: {
                        ticks: { color: '#000' },
                        grid: { display: false }
                    }
                }
            }
        });
    }
};
