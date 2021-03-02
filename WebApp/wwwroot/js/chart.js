google.charts.load('current', { 'packages': ['bar', 'corechart']});
google.charts.setOnLoadCallback(drawChart);


function drawChart() {
    let c_data = JSON.parse(document.getElementById('column_chart').getAttribute('data-graph'));
    let p_data = JSON.parse(document.getElementById('pie_chart').getAttribute('data-graph'));

    let p_dataArray = [
        ['Task', 'Hours per Day'],
    ];
    let c_dataArray = [
        ['Месяц', 'Заказы' , 'Прибыль (тыс. грн)))']
    ];

    for (let item of p_data) {
        p_dataArray.push([item.category, item.quantity])
    };
    for (let item of c_data)
    {
        c_dataArray.push([item.month, item.orders, item.amount])
    };

    let p_dataTable = google.visualization.arrayToDataTable(p_dataArray);
    let c_dataTable = google.visualization.arrayToDataTable(c_dataArray);

    let p_options = {
        title: 'Категории',
        

    };
    let c_options = {
        chart: {
            title: 'Статистика',
            subtitle: 'Заказы и доходы за текущий год',
           
        }
    };

    let p_chart = new google.visualization.PieChart(document.getElementById('pie_chart'));
    let c_chart = new google.charts.Bar(document.getElementById('column_chart'));

    p_chart.draw(p_dataTable, p_options);
    c_chart.draw(c_dataTable, google.charts.Bar.convertOptions(c_options));
}
