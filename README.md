# Mission Planner External Position Correction Plugin

## English

### Overview

This plugin for Mission Planner allows for real-time position correction of a drone by using an external position estimation source. It's designed for scenarios where high-precision positioning is crucial, and standard GPS is not sufficient or available. This could include indoor navigation using a motion capture system (like Vicon, OptiTrack), or outdoor scenarios using RTK GPS corrections sent over a secondary channel.

The plugin works by listening for position data from an external source (e.g., a script sending data over a UDP port) and feeding it into Mission Planner. Mission Planner then forwards this data to the ArduPilot flight controller, which uses it to correct its own position estimate.

### Features

*   **Real-time Position Correction:** Continuously updates the vehicle's position based on external data.
*   **Easy Configuration:** Simple interface within Mission Planner to set up the connection to the external data source.
*   **Support for Multiple Data Formats:** Can be adapted to parse various data formats from different external positioning systems.
*   **Lightweight and Efficient:** Designed to have minimal impact on Mission Planner's performance.

### How It Works

The plugin integrates with Mission Planner and utilizes MAVLink messages to communicate with the flight controller. Specifically, it can be configured to send `VISION_POSITION_ESTIMATE` or similar MAVLink messages to ArduPilot. The Extended Kalman Filter (EKF) on the flight controller then fuses this external data with its other sensor readings (IMU, barometer, etc.) to produce a more accurate position estimate.

The external position data is expected to be sent to a specified UDP port on the computer running Mission Planner. The plugin listens on this port, parses the incoming data, and injects it into the MAVLink stream going to the vehicle.

### Installation

1.  **Download the Plugin:** Download the latest release of the plugin from the `Releases` page of your repository.
2.  **Copy to Plugins Directory:** Copy the `.dll` file into the `Plugins` directory of your Mission Planner installation (usually `C:\Program Files (x86)\Mission Planner\plugins`).
3.  **Restart Mission Planner:** Close and reopen Mission Planner. The plugin will be loaded automatically.

### Usage

1.  **Open Mission Planner:** Launch Mission Planner and connect to your vehicle.
2.  **Access the Plugin:** Navigate to the `DATA` screen. In the `Actions` tab on the bottom left, you should see a new button for your plugin.
3.  **Configure the Plugin:**
    *   Enter the UDP port number that your external positioning system is broadcasting data to.
    *   Click "Connect" to start listening for data.
4.  **Verify Operation:** Once connected, you should see the vehicle's position on the map update according to the external data source. You can also check the MAVLink Inspector to see the incoming `VISION_POSITION_ESTIMATE` messages.

### For Developers: Building from Source

If you want to modify or contribute to the plugin, you can build it from the source code.

1.  **Prerequisites:**
    *   Visual Studio with C# support.
    *   A copy of the Mission Planner source code to reference its libraries.
2.  **Building:**
    *   Clone this repository.
    *   Open the solution file (`.sln`) in Visual Studio.
    *   Ensure the references to Mission Planner libraries (like `MissionPlanner.Core`, `MAVLink`, etc.) are correctly pointing to your Mission Planner build or installation directory.
    *   Build the project. The output `.dll` will be located in the `bin/Debug` or `bin/Release` folder.

### Contributing

Contributions are welcome! If you have an idea for an improvement or have found a bug, please open an issue or submit a pull request.

---

## Русский

### Обзор

Этот плагин для Mission Planner предназначен для коррекции позиции дрона в реальном времени с использованием внешнего источника данных о местоположении. Он разработан для сценариев, где требуется высокая точность позиционирования, а стандартного GPS недостаточно или он недоступен. Это может включать в себя навигацию в помещении с использованием системы захвата движения (например, Vicon, OptiTrack) или навигацию на открытом воздухе с использованием RTK-поправок, передаваемых по вторичному каналу.

Плагин работает, принимая данные о местополошении от внешнего источника (например, от скрипта, отправляющего данные через UDP-порт) и передавая их в Mission Planner. Затем Mission Planner пересылает эти данные на полетный контроллер ArduPilot, который использует их для коррекции своей собственной оценки местоположения.

### Возможности

*   **Коррекция позиции в реальном времени:** Постоянно обновляет позицию аппарата на основе внешних данных.
*   **Простая настройка:** Удобный интерфейс в Mission Planner для настройки подключения к внешнему источнику данных.
*   **Поддержка различных форматов данных:** Может быть адаптирован для разбора различных форматов данных от разных систем позиционирования.
*   **Легковесность и эффективность:** Разработан с минимальным влиянием на производительность Mission Planner.

### Как это работает

Плагин интегрируется с Mission Planner и использует сообщения MAVLink для связи с полетным контроллером. В частности, его можно настроить для отправки сообщений `VISION_POSITION_ESTIMATE` или аналогичных в ArduPilot. Расширенный фильтр Калмана (EKF) на полетном контроллере затем объединяет эти внешние данные с показаниями других датчиков (IMU, барометр и т.д.) для получения более точной оценки местоположения.

Предполагается, что внешние данные о местоположении отправляются на указанный UDP-порт на компьютере, где запущен Mission Planner. Плагин прослушивает этот порт, анализирует входящие данные и вставляет их в поток MAVLink, идущий к аппарату.

### Установка

1.  **Скачайте плагин:** Загрузите последнюю версию плагина со страницы `Releases` вашего репозитория.
2.  **Скопируйте в директорию плагинов:** Скопируйте файл `.dll` в директорию `Plugins` вашей установки Mission Planner (обычно это `C:\Program Files (x86)\Mission Planner\plugins`).
3.  **Перезапустите Mission Planner:** Закройте и снова откройте Mission Planner. Плагин будет загружен автоматически.

### Использование

1.  **Откройте Mission Planner:** Запустите Mission Planner и подключитесь к вашему аппарату.
2.  **Откройте плагин:** Перейдите на экран `DATA`. Во вкладке `Actions` слева внизу вы должны увидеть новую кнопку для вашего плагина.
3.  **Настройте плагин:**
    *   Введите номер UDP-порта, на который ваша внешняя система позиционирования передает данные.
    *   Нажмите "Connect" (Подключиться), чтобы начать прослушивание данных.
4.  **Проверьте работу:** После подключения вы должны увидеть, как позиция аппарата на карте обновляется в соответствии с данными от внешнего источника. Вы также можете проверить MAVLink Inspector, чтобы увидеть входящие сообщения `VISION_POSITION_ESTIMATE`.

### Для разработчиков: Сборка из исходного кода

Если вы хотите изменить или внести свой вклад в плагин, вы можете собрать его из исходного кода.

1.  **Необходимые компоненты:**
    *   Visual Studio с поддержкой C#.
    *   Копия исходного кода Mission Planner для подключения его библиотек.
2.  **Сборка:**
    *   Клонируйте этот репозиторий.
    *   Откройте файл решения (`.sln`) в Visual Studio.
    *   Убедитесь, что ссылки на библиотеки Mission Planner (такие как `MissionPlanner.Core`, `MAVLink` и др.) правильно указывают на вашу директорию со сборкой или установкой Mission Planner.
    *   Соберите проект. Выходной файл `.dll` будет находиться в папке `bin/Debug` или `bin/Release`.

### Участие в разработке

Мы приветствуем ваш вклад! Если у вас есть идея по улучшению или вы нашли ошибку, пожалуйста, создайте "issue" или отправьте "pull request".

