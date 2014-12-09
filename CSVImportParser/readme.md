This tool utility was writen for simply importing flat data from CSV format into MS SQL database.
Also it can be used for parse CSV data to custom class collection



Consist of dll with form interface.
At the form data loads to data grid view conrol, column which can be compared with custom class fields. It must be necessarily doing before import processing.
After press "OK", for getting imported data you have two ways:
1. get public variable "Data" of form. It represent DataTable type which contains all rows of data grid exclude marked for ignore.
2. get public variable "ListData<T>" of form. It represet Collection of custom class type <T>. In this case - if preprocessing delegate function was setted in public variable "preFormat", this function will be called for each cell of table grid with column name at parameter:

public static void preImportFormat(ref object datavalue, string column)
{
//processing datavalue
//if datavalue is not matched template - set it null. In this case curent row of data grid will be skip from import
}

After proccesing each mapped field of data grid view control will be assigned for class in coollection. One row in table - one custom class element in collection.

For each import ways you may process data grid view control for errors in data, initialized "SetCheck" delegate function. They will be called after click "OK", and before closing form. In parameters transmitted reference to DataGridView control with imported data and reference to DataGridView control for error log. If delegate function return false - will be showing error message.