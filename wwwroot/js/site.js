﻿document.addEventListener('DOMContentLoaded', () => {
    let previousData = null;

    function getUsernameFromCookie() {
        const name = "UserLoginCookie=";
        const decodedCookie = decodeURIComponent(document.cookie);
        const cookieArray = decodedCookie.split(';');
        for (const c of cookieArray) {
            const trimmedCookie = c.trim();
            if (trimmedCookie.startsWith(name)) return trimmedCookie.substring(name.length);
        }
        return null;
    }

    async function fetchData() {
        try {
            const response = await fetch('/JsonController/GetJson?timestamp=${timestamp}');
            const newData = await response.json();

            if (JSON.stringify(newData) !== JSON.stringify(previousData)) {
                console.log("JSON данные изменились.");
            //    updateMessageHistory(newData);
               // scrollToBottom();
                previousData = newData;
            } else {
                console.log("JSON данные не изменились.");
            }
        } catch (error) {
            console.error("Ошибка при получении данных:", error);
        }
    }

    function updateMessageHistory(newData) {
        const messageHistoryDiv = $('#History');
        messageHistoryDiv.empty();  // Очищаем содержимое элемента

        const username = getUsernameFromCookie();  // Получаем имя пользователя из cookie
        console.log('Текущий пользователь:', username);

        // Проверяем, существует ли пользователь и есть ли для него данные
        if (username && newData[username]) {
            console.log('Найденные данные для пользователя:', newData[username]);

            newData[username].forEach(message => {
                console.log('Обрабатываем сообщение:', message);

                // Проверьте правильный доступ к свойствам JSON
                console.log('Свойства сообщения:', Object.keys(message));
                console.log('Content:', message.content);  // Используем правильный регистр
                console.log('AnswerContent:', message.answerContent);  // Используем правильный регистр

                // Проверьте наличие и корректность данных
                if (message.content !== undefined && message.content !== null) {
                    const messageDiv = $('<div/>').addClass('message-right').text(message.content);
                    messageHistoryDiv.append(messageDiv);
                } else {
                    console.warn('Сообщение не содержит Content:', message);
                }

                if (message.answerContent !== undefined && message.answerContent !== '') {
                    const answerDiv = $('<div/>').addClass('message-left').html(message.answerContent);
                    messageHistoryDiv.append(answerDiv);
                } else {
                    console.warn('Сообщение не содержит AnswerContent:', message);
                }
            });
        } else {
            if (username) {
                console.warn(`Нет данных для пользователя ${username}`);
            } else {
                console.warn('Имя пользователя не найдено в cookie');
            }
        }
    }

    setInterval(fetchData, 4000);
    fetchData();  // Запускаем проверку сразу
});