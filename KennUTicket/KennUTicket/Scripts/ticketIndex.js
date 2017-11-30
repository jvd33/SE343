function filter(col, dir, state) {
    var params = [$('#searchField').val(), $('#search').val()]
    console.log(params)
    if (!(params[0] && params[1])) {

    }
    var searchField = params[0];
    var search = params[1];
    var searchParams = {};
    searchParams[searchField] = search;
    searchParams['strategy'] = 'search';

    filterParams = {}
    filterParams[col] = "Selected";
    filterParams['strategy'] = 'filter_' + dir;

    data = JSON.stringify({ searchModel: searchParams, filterModel: filterParams });
    $.ajax({
        url: '/Tickets/Index',
        data: data,
        method: 'POST',
        datatype: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (result, status, xhr) {
            document.open();
            document.write(result);
            document.close();
        },
        error: function (xhr, status, error) {

        }
    });
};