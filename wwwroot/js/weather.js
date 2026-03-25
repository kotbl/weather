$(function () {
    loadWeather();

    $('#retry-btn').on('click', function () {
        loadWeather();
    });
});

function loadWeather() {
    $('#loading').removeClass('d-none');
    $('#error').addClass('d-none');
    $('#weather-content').addClass('d-none');

    $.ajax({
        url: '/api/weather',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            renderWeather(data);
            $('#loading').addClass('d-none');
            $('#weather-content').removeClass('d-none');
        },
        error: function (xhr) {
            var msg = 'Не удалось загрузить данные о погоде';
            if (xhr.responseJSON && xhr.responseJSON.error) {
                msg = xhr.responseJSON.error;
            }
            $('#error-message').text(msg);
            $('#loading').addClass('d-none');
            $('#error').removeClass('d-none');
        }
    });
}

function renderWeather(data) {
    // Местное время
    $('#local-time').text(data.location.localtime);

    // Текущая погода
    var c = data.current;
    $('#current-icon').attr('src', 'https:' + c.condition.icon.replace('64x64', '128x128'));
    $('#current-temp').text(Math.round(c.temp_c) + '°C');
    $('#current-condition').text(c.condition.text);
    $('#current-feelslike').text('Ощущается как ' + Math.round(c.feelslike_c) + '°C');
    $('#current-humidity').text('Влажность: ' + c.humidity + '%');
    $('#current-wind').text('Ветер: ' + Math.round(c.wind_kph) + ' км/ч ' + c.wind_dir);
    $('#current-pressure').text('Давление: ' + Math.round(c.pressure_mb) + ' мбар');
    $('#current-uv').text('УФ-индекс: ' + c.uv);

    // Почасовой прогноз
    renderHourly(data);

    // Прогноз на 3 дня
    renderForecast(data.forecast.forecastday);
}

function renderHourly(data) {
    var container = $('#hourly-container');
    container.empty();

    var days = data.forecast.forecastday;
    var now = new Date(data.location.localtime.replace(' ', 'T'));
    var currentHour = now.getHours();

    // Оставшиеся часы сегодня
    if (days.length > 0) {
        var today = days[0].hour;
        for (var i = 0; i < today.length; i++) {
            var h = today[i];
            var hourTime = new Date(h.time.replace(' ', 'T'));
            if (hourTime.getHours() >= currentHour) {
                container.append(createHourCard(h));
            }
        }
    }

    // Все часы завтра
    if (days.length > 1) {
        var tomorrow = days[1].hour;
        for (var j = 0; j < tomorrow.length; j++) {
            container.append(createHourCard(tomorrow[j]));
        }
    }
}

function createHourCard(h) {
    var clone = $($('#hour-template').html());
    clone.find('[data-field="date"]').text(h.time.substring(5, 10));
    clone.find('[data-field="time"]').text(h.time.substring(11, 16));
    clone.find('[data-field="icon"]').attr('src', 'https:' + h.condition.icon);
    clone.find('[data-field="temp"]').text(Math.round(h.temp_c) + '°');
    clone.find('[data-field="humidity"]').text(h.humidity + '%');
    return clone;
}

function renderForecast(days) {
    var container = $('#forecast-container');
    container.empty();

    var dayNames = ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'];

    for (var i = 0; i < days.length; i++) {
        var d = days[i];
        var date = new Date(d.date + 'T00:00:00');
        var dayName = dayNames[date.getDay()];
        var dateStr = date.toLocaleDateString('ru-RU', { day: 'numeric', month: 'short' });

        var clone = $($('#forecast-template').html());
        clone.find('[data-field="day-name"]').text(dayName + ', ' + dateStr);
        clone.find('[data-field="icon"]').attr('src', 'https:' + d.day.condition.icon.replace('64x64', '128x128'));
        clone.find('[data-field="condition"]').text(d.day.condition.text);
        clone.find('[data-field="max-temp"]').text(Math.round(d.day.maxtemp_c) + '°');
        clone.find('[data-field="min-temp"]').text(Math.round(d.day.mintemp_c) + '°');
        clone.find('[data-field="details"]').text(
            'Влажность: ' + d.day.avghumidity + '% · ' +
            'Ветер: ' + Math.round(d.day.maxwind_kph) + ' км/ч · ' +
            'Дождь: ' + d.day.daily_chance_of_rain + '%'
        );
        container.append(clone);
    }
}
