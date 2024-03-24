var interval;

function startTimer(duration) {
    var timer = duration;
    interval = setInterval(function () {
        var hours = Math.floor(timer / 3600);
        var minutes = Math.floor((timer % 3600) / 60);
        var seconds = timer % 60;

        document.getElementById('timer').innerText = hours + ':' + minutes + ':' + seconds + '';

        if (--timer < 0) {
            clearInterval(interval);
        }
    }, 1000);
}

function durationToSeconds(duration) {
    var parts = duration.split(':');
    return parseInt(parts[0]) * 3600 + parseInt(parts[1]) * 60 + parseInt(parts[2]);
}

document.addEventListener('DOMContentLoaded', function () {
    var duration = '@ViewBag.duration';
    var totalSeconds = durationToSeconds(duration);
    startTimer(totalSeconds);
});
