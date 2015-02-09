CSV Import parser
========

This tool utility was writen for simply importing flat data from CSV format into database.
Also it can be used for parse CSV data to custom class collection

Consist of dll with form interface.
At the form data loads to DataGridView control, column which can be compared with custom class fields. It must be necessarily doing before import data processing.
After press "OK", for getting imported data you have two ways:
1. get public variable `Data` of form. It represent DataTable type which contains all rows of data grid exclude marked for ignore.
2. get public variable `EnumeratedData<T>` of form. It represet IEnumerable of custom class type <T>. In this case - if preprocessing delegate function was setted in public variable "_preFormatFunction", this function will be called for each cell of table grid with column name at parameter:

```c#
public static void preImportFormat(ref object datavalue, string column)
{
//processing datavalue
//if datavalue is not matched template - set it null. In this case curent row of data grid will be skip from import
}
```

After proccesing each mapped field of data grid view control will be assigned for class in coollection. One row in table - one custom class element in collection.

For each import ways you may process data grid view control for errors in data, initialized `_checkErrorsFunction` delegate function. They will be called after click "OK", and before closing form. In parameters transmitted reference to DataGridView control with imported data and reference to DataGridView control for error log. If delegate function return false - will be showing error message.

================
Эта утилита была создана для удобного импорта данных из файлов в формате CSV в базу MS SQL.
Также с помощью нее возможно смпортировать CSV данные в перечисление пользовательских классов.

Утилита состоит из библиотеки с WinForm интерфейсом.
На форме имеется контрол DataGridView, в который загружаются данные из CSV файла. Каждая колонка таблицы DataGridView может быть сопоставлена с полем пользовательского класса (щелчок правой кнопки мыши на заголовки колонки таблицы для вызова меню сопоставления). Это сопоставление необходимо сделать до начала процесса импорта данных.
После нажатия "ОК", у есть два способа получить импортируемые данные:
1. обратиться к переменной формы `Data`, в которой данные представленны типом DataTable с именоваными полями так, как они были сопоставлены. Исключая строки отмеченные "Игнор.".
2. обратиться к переменной формы `EnumeratedData<T>` В ней содержится перечисления пользовательского типа <T>. При получении данных через эту переменную доступна предимпортная обработка данных через функцию-делегат, которая определяется в переменной "_preFormatFunction". Эта функция будет вызвана при обработке каждой строки таблицы с параметром названия сопоставленой колонки таблицы:

```c#
public static void preImportFormat(ref object datavalue, string columnName)
{
//обработка datavalue
//если datavalue не соответствует шаблону - установите его = null. В этом случае вся обрабатываемая строка будет пропущена и не будет импортирована.
}
```

При любом из выбранных способов данных можно задать функицю-делегат `_checkErrorsFunction` для предварительной проверки загруженых в DataGridView данных. Она будет вызвана после нажатия "ОК" и до закрытия формы. В параметрах функции передается ссылка на контрол DataGridView с загруженными данными и ссылка на контрол DataGridView в который будут выводиться ошибки. Если функция проверки ошибок вернет false - будет показаны сообщения об ошибках и форма импорта не будет закрыта.
